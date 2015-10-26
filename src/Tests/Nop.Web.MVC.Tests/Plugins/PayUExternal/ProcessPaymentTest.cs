using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.ExternalPayU;
using Nop.Plugin.Payments.PayUExternal.Controllers;
using Nop.Plugin.Payments.PayUExternal.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Nop.Web.MVC.Tests.Plugins.PayUExternal
{

    [TestFixture]
    public class ProcessPaymentTest
    {

        private ISettingService _settingService;
        private ILocalizationService _localizationService;
        private IWorkContext _workContext;
        private IOrderService _orderService;
        private IProductService _productService;
        private PayUExternalSettings _settings;
        private IPriceFormatter _priceFormater;
        private ILogger _logger;
        private PlanSettings _planSettings;
        private IDateTimeHelper _dateTimeHelper;
        private IOrderProcessingService _orderProcessingService;
        private PayUExternalController controller;

        [SetUp]
        public void SetUp()
        {
            _settingService = MockRepository.GenerateMock<ISettingService>();
            _localizationService = MockRepository.GenerateMock<ILocalizationService>();
            _workContext = MockRepository.GenerateMock<IWorkContext>();
            _orderService = MockRepository.GenerateMock<IOrderService>();
            _settings = MockRepository.GenerateMock<PayUExternalSettings>();
            _priceFormater = MockRepository.GenerateMock<IPriceFormatter>();
            _logger = MockRepository.GenerateMock<ILogger>();
            //_planSettings = MockRepository.GenerateMock<PlanSettings>();
            _planSettings = new PlanSettings();
            _dateTimeHelper = MockRepository.GenerateMock<IDateTimeHelper>();
            _orderProcessingService = MockRepository.GenerateMock<IOrderProcessingService>();
            _productService = MockRepository.GenerateMock<IProductService>();

            var ctx = new Nop.Core.Fakes.FakeHttpContext("~/", "GET", null, null, null, null, null, null);

            var context = new Moq.Mock<HttpContextBase>();
            var request = new Moq.Mock<HttpRequestBase>();
            var response = new Moq.Mock<HttpResponseBase>(Moq.MockBehavior.Loose);

            context.Setup(c => c.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            

            controller = new PayUExternalController(_settings, _settingService, _localizationService, _workContext, _orderService, _productService, _priceFormater, _planSettings, _logger, _dateTimeHelper, _orderProcessingService);
            var routeData = new RouteData();
            controller.ControllerContext = // set mocked context
                 new ControllerContext(ctx, routeData, controller);

        }

        private PaymentConfirmationRequest GetValidPaymentConfirmationRequest(out Order order, int idPlanItem = -1, int idProductSelected = -1)
        {

            order = new Order();
            order.OrderGuid = Guid.NewGuid();
            order.StoreId = 1;
            order.CustomerId = 1;
            order.OrderStatus = OrderStatus.Pending;
            order.ShippingStatus = Core.Domain.Shipping.ShippingStatus.ShippingNotRequired;
            order.PaymentStatus = Core.Domain.Payments.PaymentStatus.Pending;
            order.CurrencyRate = 1;
            order.CustomerTaxDisplayType = Core.Domain.Tax.TaxDisplayType.IncludingTax;
            order.OrderSubtotalInclTax = 18000;
            order.OrderSubtotalExclTax = 18000;
            order.OrderTotal = 18000;
            order.CustomerLanguageId = 1;
            order.CreatedOnUtc = DateTime.UtcNow;

            order.OrderItems.Add(new OrderItem() 
            { 
                ProductId = idPlanItem, 
                Quantity = 1,   
                UnitPriceExclTax = 18000,
                UnitPriceInclTax = 18000,
                PriceInclTax = 18000,
                PriceExclTax = 18000
            });


            _orderService.InsertOrder(order);
            
            var request = new PaymentConfirmationRequest();
            request.value = "18000.00";
            request.reference_sale = order.Id;
            request.currency = "COP";
            request.reference_pol = "123";
            request.state_pol = TransactionState.Approved;
            request.extra1 = idProductSelected.ToString();

            //Crea la llave que debe concordar y la asigna
            bool twoDecimals = ((decimal)(request.ValueDecimal * 10) - (request.ValueDecimal * 10)) > 0;
            decimal newValue = twoDecimals ? request.ValueDecimal : Math.Round(request.ValueDecimal, 1, MidpointRounding.ToEven);
            request.sign = Nop.Utilities.Cryptography.MD5(
                string.Format("{0}~{1}~{2}~{3}~{4}~{5}",
                _settings.ApiKey,
                _settings.MerchantId,
                request.reference_sale,
                newValue.ToString(new System.Globalization.CultureInfo("en-US")),
                request.currency,
                Convert.ToInt32(request.state_pol)));

            return request;
        }

        /// <summary>
        /// Envia cualquier llave y debe arrojar error de esta validacion
        /// </summary>
        [Test]
        public void payu_confirmation_invalid_key()
        {
            Order order;
            var request = GetValidPaymentConfirmationRequest(out order);
            request.sign = "ab0df2154a073b35788124d77b07eb6e1";
            request.value = "18000.00";
            request.reference_sale = 1130;
            request.currency = "COP";
            request.reference_pol = "123";
            request.state_pol = TransactionState.Approved;
            var response = (JsonResult)controller.PaymentConfirmation(request);
            Assert.AreEqual((((PaymentResponseErrorModel)response.Data).ErrorCode), PaymentConfirmationErrorCode.InvalidSignature);
        }

        /// <summary>
        /// Debe concordar la llave enviada con la validacion
        /// </summary>
        [Test]
        public void payu_confirmation_valid_key()
        {
            Order order;
            var request = GetValidPaymentConfirmationRequest(out order);
            request.reference_sale = -20;
            
            var response = (JsonResult)controller.PaymentConfirmation(request);
            Assert.AreEqual((((PaymentResponseErrorModel)response.Data).ErrorCode), PaymentConfirmationErrorCode.InvalidOrderNumber);
        }

        /// <summary>
        /// Valida que la orden que está intentando correr no exista
        /// </summary>
        [Test]
        public void payu_order_doesnot_exist()
        {
            Order order;
            var request = GetValidPaymentConfirmationRequest(out order);
            request.reference_sale = -20;

            var response = (JsonResult)controller.PaymentConfirmation(request);
            Assert.AreEqual((((PaymentResponseErrorModel)response.Data).ErrorCode), PaymentConfirmationErrorCode.InvalidOrderNumber);
        }

        /// <summary>
        /// Valida que la orden tenga un plan seleccionado valido
        /// </summary>
        [Test]
        public void payu_selected_plan_user_isvalid()
        {
            var product = _productService.SearchProducts(categoryIds:new List<int>(){_planSettings.CategoryProductPlansId} ).FirstOrDefault();
            Order order;
            var request = GetValidPaymentConfirmationRequest(out order, product.Id, 0);
            var response = (JsonResult)controller.PaymentConfirmation(request);
            Assert.AreEqual((((PaymentResponseErrorModel)response.Data).ErrorCode), PaymentConfirmationErrorCode.NoProductSelected);
        }




    }
}
