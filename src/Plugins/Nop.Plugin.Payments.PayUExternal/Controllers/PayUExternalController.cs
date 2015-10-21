using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;

using Nop.Web.Framework.Controllers;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain;
using Nop.Core.Domain.Catalog;
using Nop.Web;
using Nop.Web.Infrastructure.Cache;
using Nop.Services.Stores;
using Nop.Plugin.Payments.PayUExternal.Models;
using Nop.Plugin.Payments.ExternalPayU;
using Nop.Services.Logging;


namespace Nop.Plugin.Payments.PayUExternal.Controllers
{
    public class PayUExternalController : BasePaymentController
    {
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly PayUExternalSettings _settings;
        private readonly IPriceFormatter _priceFormater;
        private readonly ILogger _logger;
        private readonly PlanSettings _planSettings;
        private readonly IDateTimeHelper _dateTimeHelper;
        
        public PayUExternalController(PayUExternalSettings settings,
            ISettingService settingService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IOrderService orderService,
            IProductService productService,
            IPriceFormatter priceFormater,
            PlanSettings planSettings,
            ILogger logger,
            IDateTimeHelper datetimeHelper)
        {
            this._settings = settings;
            this._settingService = settingService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._orderService = orderService;
            this._productService = productService;
            this._priceFormater = priceFormater;
            this._planSettings = planSettings;
            this._logger = logger;
            this._dateTimeHelper = datetimeHelper;
        }

        #region Configure
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel();
            model.AccountId = _settings.AccountId;
            model.ApiKey = _settings.ApiKey;
            model.ConfirmationUrl = _settings.ConfirmationUrl;
            model.MerchantId = _settings.MerchantId;
            model.ResponseUrl = _settings.ResponseUrl;
            model.UrlPayment = _settings.UrlPayment;
            return View("~/Plugins/Payments.PayUExternal/Views/PayUExternal/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();
            else
            {
                _settings.AccountId = model.AccountId;
                _settings.ApiKey = model.ApiKey;
                _settings.ConfirmationUrl = model.ConfirmationUrl;
                _settings.MerchantId = model.MerchantId;
                _settings.ResponseUrl = model.ResponseUrl;
                _settings.UrlPayment = model.UrlPayment;
                _settingService.SaveSetting(_settings);
                SuccessNotification(_localizationService.GetResource("Admin.Configuration.Updated"));
            }

            return Configure();
        }
        #endregion


        #region PaymentResponse
        [ChildActionOnly]
        public ActionResult PaymentResponse(PaymentResponseRequest command)
        {

            //Almacena la información de la petición
            _logger.Information(command);
            
            
            if (!IsValidSignature(command))
                return ErrorResponse(command, PaymentResponseErrorCodes.InvalidSignature.ToString());
            
            var order = _orderService.GetOrderById(command.referenceCode);
            //Valida que la orden exista, que sea del usuario que se encuentra autenticado y que tenga items
            if (order == null || order.CustomerId != _workContext.CurrentCustomer.Id || order.OrderItems.Count == 0)
                return ErrorResponse(command, PaymentResponseErrorCodes.InvalidOrderNumber.ToString());

            var selectedPlan = order.OrderItems.First().Product;

            //Si el producto que viene seleccionado no es un plan o un destacado para un producto muestra el error
            int categoryId = selectedPlan.ProductCategories.First().CategoryId;
            if(categoryId != _planSettings.CategoryProductPlansId && categoryId != _planSettings.CategoryStorePlansId)
                return ErrorResponse(command, PaymentResponseErrorCodes.NoPlanSelected.ToString());

            //Si el plan es para destacar el producto y no viene extra1 que refiere el codigo del producto destacado es error
            int selectedProductId = 0;
            if((categoryId == _planSettings.CategoryProductPlansId  && string.IsNullOrEmpty(command.extra1))
                & (!int.TryParse(command.extra1, out selectedProductId) || selectedProductId == 0))
                return ErrorResponse(command, PaymentResponseErrorCodes.NoProductSelected.ToString());

            var model = new PaymentResponseModel();
            model.SelectedPlanName = selectedPlan.Name;
            model.ReferenceCode = command.referenceCode.ToString();
            model.ReferencePayUCode = command.reference_pol;
            model.State = _localizationService.GetResource(string.Format("Plugins.PayUExternal.TransactionState.{0}", command.transactionState));
            model.TransactionValue = _priceFormater.FormatPrice(order.OrderTotal);
            model.Currency = command.currency;
            model.TransactionDate = Convert.ToDateTime(command.processingDate).ToShortDateString(); 

            //Si el plan seleccionado es especial para destacar un producto, carga la información del mismo en el modelo
            if (categoryId == _planSettings.CategoryProductPlansId)
            { 
                var product = _productService.GetProductById(selectedProductId);
                model.ProductName = product.Name;
                model.IsFeaturedProduct = true;
            }
            
            return View("~/Plugins/Payments.PayUExternal/Views/PayUExternal/PaymentResponse.cshtml", model);
        }

        [ChildActionOnly]
        public ActionResult ErrorResponse(PaymentResponseRequest command, string errorName)
        {

            var model = new PaymentResponseErrorModel();
            model.ErrorMessage = string.Format( _localizationService.GetResource("Plugins.PayUExternal.ErrorResponse.External"), command.referenceCode,  errorName);

            //Guarda la información de la transaccion
            _logger.Warning(string.Format("No fue posible procesar la petición, codigo de error {0}. Transaccion PayU {1}", errorName, command.reference_pol));

            return View("~/Plugins/Payments.PayUExternal/Views/PayUExternal/ErrorResponse.cshtml", model);
        }

        /// <summary>
        /// Realiza la validacion de la firma que viene en el request
        /// La validación de la firma te permite comprobar la integridad de los datos, deberás generar la firma con los datos que encontrarás en la página de respuesta y compararla con la información del parámetro signature.
        //  Para validar la firma en la página de respuesta debes tener en cuenta:
        //  Para obtener el nuevo valor new_value se debe aproximar TX_VALUE siempre a un decimal con el método de redondeo "Round half to even":
        //    * Si el primer decimal es par y el segundo es 5, se redondeará hacia el menor valor.
        //    * Si el primer decimal es impar y el segundo es 5, se redondeará hacia el valor mayor.
        //    * En cualquier otro caso se redondeará al decimal más cercano.
        //  Los parámetros para generar la firma merchantId, referenceCode, TX_VALUE, currency y transactionState debes obtenerlos de la página de respuesta, no de tu base de datos.
        //  Debes tener almacenada tu ApiKey de forma segura.
        //  Esquema de la firma:
        //          "ApiKey~merchantId~referenceCode~new_value~currency~transactionState"             
        /// </summary>
        /// <param name="command"></param>
        /// <returns>true: firma valida false: firma invalida</returns>
        private bool IsValidSignature(PaymentResponseRequest command)
        {
            //Round half to even
            //bool even = ((int)(command.TX_VALUE * 10) % 2) == 0;
            decimal newValue = Math.Round(command.TX_VALUE, 1, MidpointRounding.ToEven);
            
            //TX_VALUE siempre a un decimal con el método de redondeo "Round half to even"
            var signatureToValidate = Nop.Utilities.Cryptography.MD5(string.Format("{0}~{1}~{2}~{3}~{4}~{5}", _settings.ApiKey, _settings.MerchantId, command.referenceCode, newValue.ToString(new System.Globalization.CultureInfo("en-US")), command.currency, Convert.ToInt32(command.transactionState)));
            return signatureToValidate.Equals(command.signature);
        }

        #endregion

        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            throw new NotImplementedException();
        }

        public override Services.Payments.ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            throw new NotImplementedException();
        }
    }
}