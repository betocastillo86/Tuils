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
using Nop.Core.Domain.Orders;
using Nop.Services.Helpers;
using System.Transactions;
using Nop.Services.Vendors;


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
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IVendorService _vendorService;
        
        public PayUExternalController(PayUExternalSettings settings,
            ISettingService settingService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IOrderService orderService,
            IProductService productService,
            IPriceFormatter priceFormater,
            PlanSettings planSettings,
            ILogger logger,
            IDateTimeHelper datetimeHelper,
            IOrderProcessingService orderProcessingService,
            IVendorService vendorService)
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
            this._orderProcessingService = orderProcessingService;
            this._vendorService = vendorService;
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
            _logger.Information(command, "PaymentResponseRequest");
            
            
            if (!IsValidSignatureResponse(command))
                return ErrorResponse(command, PaymentResponseErrorCode.InvalidSignature.ToString());
            
            var order = _orderService.GetOrderById(command.referenceCode);
            //Valida que la orden exista, que sea del usuario que se encuentra autenticado y que tenga items
            if (order == null || order.CustomerId != _workContext.CurrentCustomer.Id || order.OrderItems.Count == 0)
                return ErrorResponse(command, PaymentResponseErrorCode.InvalidOrderNumber.ToString());

            var selectedPlan = order.OrderItems.First().Product;

            //Si el producto que viene seleccionado no es un plan o un destacado para un producto muestra el error
            int categoryId = selectedPlan.ProductCategories.First().CategoryId;
            if(categoryId != _planSettings.CategoryProductPlansId && categoryId != _planSettings.CategoryStorePlansId)
                return ErrorResponse(command, PaymentResponseErrorCode.NoPlanSelected.ToString());

            //Si el plan es para destacar el producto y no viene extra1 que refiere el codigo del producto destacado es error
            int selectedProductId = 0;
            if((categoryId == _planSettings.CategoryProductPlansId  && string.IsNullOrEmpty(command.extra1))
                & (!int.TryParse(command.extra1, out selectedProductId) || selectedProductId == 0))
                return ErrorResponse(command, PaymentResponseErrorCode.NoProductSelected.ToString());


            //Guarda la nota de que ya el usuario paso por la respuesta
            order.OrderNotes.Add(new OrderNote() { 
                CreatedOnUtc = DateTime.UtcNow,
                Note = command.ToStringObject("Response->")
            });

            _orderService.UpdateOrder(order);

            var model = new PaymentResponseModel();
            model.SelectedPlanName = selectedPlan.Name;
            model.ReferenceCode = command.referenceCode.ToString();
            model.ReferencePayUCode = command.reference_pol;
            model.State = _localizationService.GetResource(string.Format("Plugins.PayUExternal.TransactionState.{0}", command.transactionState));
            model.TransactionValue = _priceFormater.FormatPrice(order.OrderTotal);
            model.Currency = command.currency;
            model.TransactionDate = Convert.ToDateTime(command.processingDate).ToShortDateString();
            model.OrderId = order.Id;
            model.TransactionRejected = command.transactionState == TransactionState.Declined || command.transactionState == TransactionState.Error || command.transactionState == TransactionState.Expired;


            //Carga los valores del producto
            //Para los casos en que se adquiere un plan de tienda sin vender directamente un producto el id del producto puede ser 0
            if (selectedProductId > 0)
            {
                var product = _productService.GetProductById(selectedProductId);
                model.ProductName = product.Name;
                model.ProductId = product.Id;    
            }
            

            //Si el plan seleccionado es especial para destacar un producto, carga la información del mismo en el modelo
            if (categoryId == _planSettings.CategoryProductPlansId)
            {
                model.IsFeaturedProduct = true;
            }
            else
            {
                //Como la actualizacion del plan solo se activa en la confirmación, se envian dos llaves minimas de seguridad
                //Además debe de venir de una publicación de un producto, sino solo muestra la confirmación de la venta
                if (!model.TransactionRejected && selectedProductId > 0)
                {
                    model.RedirectToFeatureProduct = true;
                    model.RedirectToFeaturedKey = _orderProcessingService.GetPaymentPlanValidationKeys(order, selectedProductId);

                    
                    //return RedirectToAction("SelectFeaturedAttributesByPlan", "Sales",
                    //    new
                    //    {
                    //        orderId = order.Id,
                    //        id = selectedProductId,
                    //        sign = _orderProcessingService.GetPaymentPlanValidationKeys(order.Id, selectedProductId)
                    //    });
                }
                
            }
            
            return View("~/Plugins/Payments.PayUExternal/Views/PayUExternal/PaymentResponse.cshtml", model);
            
        }

        [ChildActionOnly]
        public ActionResult ErrorResponse(PaymentResponseRequest command, string errorName)
        {

            var model = new PaymentResponseErrorModel();
            model.ErrorMessage = string.Format(_localizationService.GetResource("Plugins.PayUExternal.ErrorResponse.External"), command.referenceCode, errorName);

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
        private bool IsValidSignatureResponse(PaymentResponseRequest command)
        {
            //Round half to even
            //bool even = ((int)(command.TX_VALUE * 10) % 2) == 0;
            decimal newValue = Math.Round(command.TX_VALUE, 1, MidpointRounding.ToEven);
            
            //TX_VALUE siempre a un decimal con el método de redondeo "Round half to even"
            var signatureToValidate = Nop.Utilities.Cryptography.MD5(string.Format("{0}~{1}~{2}~{3}~{4}~{5}", _settings.ApiKey, _settings.MerchantId, command.referenceCode, newValue.ToString(new System.Globalization.CultureInfo("en-US")), command.currency, Convert.ToInt32(command.transactionState)));
            return signatureToValidate.Equals(command.signature);
        }




       

        #endregion

        #region PaymentConfirmation
        /// <summary>
        /// Procesa la confirmación de la orden y la marca como paga y queda lista para ser publicada
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PaymentConfirmation(PaymentConfirmationRequest command)
        {

            //Almacena la información de la petición
            _logger.Information(command, "PaymentConfirmationRequest");


            if (!IsValidSignatureConfirmation(command))
                return ErrorConfirmation(command, PaymentConfirmationErrorCode.InvalidSignature, null);

            var order = _orderService.GetOrderById(command.reference_sale);
            //Valida que la orden exista, que sea del usuario que se encuentra autenticado y que tenga items
            if (order == null || order.OrderItems.Count == 0)
                return ErrorConfirmation(command, PaymentConfirmationErrorCode.InvalidOrderNumber, order);

            var selectedPlan = order.OrderItems.First().Product;

            //Si el producto que viene seleccionado no es un plan o un destacado para un producto muestra el error
            int categoryId = selectedPlan.ProductCategories.First().CategoryId;
            
            if (categoryId != _planSettings.CategoryProductPlansId && categoryId != _planSettings.CategoryStorePlansId)
                return ErrorConfirmation(command, PaymentConfirmationErrorCode.NoPlanSelected, order);

            //Si el plan es para destacar el producto y no viene extra1 que refiere el codigo del producto destacado es error
            int selectedProductId = 0;
            if ((categoryId == _planSettings.CategoryProductPlansId && string.IsNullOrEmpty(command.extra1))
                & (!int.TryParse(command.extra1, out selectedProductId) || selectedProductId == 0))
                return ErrorConfirmation(command, PaymentConfirmationErrorCode.NoProductSelected, order);


           
            //Inicia la transacción ReadUncommited ya que no es necesario que aisle las tablas por el tipo de operación que se está haciendo
            using (var scope = new System.Transactions.TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {

                //Guarda la nota de que ya el usuario paso por la confirmacion
                order.OrderNotes.Add(new OrderNote()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    Note = command.ToStringObject(string.Format("Confirmation{0}->", command.state_pol))
                });

                _orderService.UpdateOrder(order);


                switch (command.state_pol)
                {
                    case TransactionState.Approved:
                        //Valida si puede marcar la orden y la marca como paga
                        if (_orderProcessingService.CanMarkOrderAsPaid(order))
                        {
                            order.AuthorizationTransactionId = command.transaction_id;
                            //_orderService.UpdateOrder(order);

                            _orderProcessingService.MarkOrderAsPaid(order);


                            var isStorePlan = categoryId == _planSettings.CategoryStorePlansId;
                            
                            //Si se ha seleccionado un plan especifico para un producto, se destaca
                            if (!isStorePlan)
                            {
                                //Despues de realizar los cambios actualiza los datos del plan
                                _productService.AddPlanToProduct(selectedProductId, order);
                            }
                            else 
                            { 
                                //Si el plan es a nivel de tienda actualiza las vigencias
                                var vendor = _vendorService.AddPlanToVendor(order);

                                //Actualiza las fechas de la orden con los datos del plan
                                order.PlanExpirationOnUtc = vendor.PlanExpiredOnUtc;
                                order.PlanStartOnUtc = DateTime.UtcNow;
                                
                                //PARA DOWNGRADE LLAMAR EL METODO DE ABAJO
                                _productService.ValidateProductLimitsByVendorPlan(vendor);
                            }

                            scope.Complete();
                        }
                        else
                        {
                            //Realiza dispose manual ya que no puede realizar el commit de los cambios
                            //Transaction.Current.Rollback();
                            scope.Dispose();
                            return ErrorConfirmation(command, PaymentConfirmationErrorCode.OrderAlreadyPaid, order);
                        }
                        break;
                    //Si hay errror realiza la cancelación de la orden y el usuario debe reiniciar la transacción
                    case TransactionState.Declined:
                    case TransactionState.Expired:
                    case TransactionState.Error:
                        if (_orderProcessingService.CanVoid(order))
                        {
                            _orderProcessingService.Void(order);
                            scope.Complete();
                        }
                        else
                        {
                            scope.Dispose();
                            return ErrorConfirmation(command, PaymentConfirmationErrorCode.OrderAlreadyPaid, order);
                        }
                        break;
                    case TransactionState.Pending:
                    default:
                        break;
                }


            }

            Response.StatusCode = 200;
            return Json(new { Resultado = "Operación existosa" }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public ActionResult ErrorConfirmation(PaymentConfirmationRequest command, PaymentConfirmationErrorCode errorName, Order order)
        {

            int errorCode = 0;

            switch (errorName)
            {
                case PaymentConfirmationErrorCode.InvalidSignature:
                case PaymentConfirmationErrorCode.NoPlanSelected:
                case PaymentConfirmationErrorCode.NoProductSelected:
                case PaymentConfirmationErrorCode.InvalidOrderNumber:
                    errorCode = 400;
                    break;
                default:
                    errorCode = 400;
                    break;
            }

            if (order != null)
            {
                //Guarda la nota de error en la orden
                order.OrderNotes.Add(new OrderNote()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    Note = command.ToStringObject("ConfirmationError->" + errorName.ToString())
                });

                _orderService.UpdateOrder(order);
            }


            Response.StatusCode = errorCode;

            var model = new PaymentResponseErrorModel();
            model.ErrorMessage = string.Format(_localizationService.GetResource("Plugins.PayUExternal.ErrorResponse.External") ?? string.Empty, command.reference_pol, errorName);
            model.ErrorCode = errorName;
            //Guarda la información de la transaccion
            _logger.Warning(string.Format("No fue posible procesar la petición ErrorConfirmation, codigo de error {0}. Transaccion PayU {1}", errorName, command.reference_pol));

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Realiza la validacion de la firma que viene en el request de la confirmación
        /// La validación de la firma te permite comprobar la integridad de los datos, deberás generar la firma con los datos que encontrarás en la página de respuesta y compararla con la información del parámetro signature.
        //  Si el segundo decimal del parámetro value es cero, ejemplo: 150.00
        //  El nuevo valor new_value para generar la firma debe ir con sólo un decimal así: 150.0.
        //  Si el segundo decimal del parámetro value es diferente a cero, ejemplo: 150.26
        //  El nuevo valor new_value para generar la firma debe ir con los dos decimales así: 150.26.
        //  Los parámetros para generar la firma merchant_id, reference_sale, value, currency y state_pol debes obtenerlos de la página de confirmación, no de tu base de datos.
        //  Debes tener almacenada tu ApiKey de forma segura.
        //  Esquema de la firma:
        //          "ApiKey~merchant_id~reference_sale~new_value~currency~state_pol"             
        /// </summary>
        /// <param name="command"></param>
        /// <returns>true: firma valida false: firma invalida</returns>
        private bool IsValidSignatureConfirmation(PaymentConfirmationRequest command)
        {
            //Realiza el redondeo, validando si tiene más de dos decimales o no
            bool twoDecimals = ((decimal)(command.ValueDecimal * 10) - (command.ValueDecimal * 10)) > 0;
            decimal newValue = twoDecimals ? command.ValueDecimal : Math.Round(command.ValueDecimal, 1, MidpointRounding.ToEven);

            //TX_VALUE siempre a un decimal con el método de redondeo "Round half to even"
            var signatureToValidate = Nop.Utilities.Cryptography.MD5(string.Format("{0}~{1}~{2}~{3}~{4}~{5}", _settings.ApiKey, _settings.MerchantId, command.reference_sale, newValue.ToString(new System.Globalization.CultureInfo("en-US")), command.currency, Convert.ToInt32(command.state_pol)));
            return signatureToValidate.Equals(command.sign);
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