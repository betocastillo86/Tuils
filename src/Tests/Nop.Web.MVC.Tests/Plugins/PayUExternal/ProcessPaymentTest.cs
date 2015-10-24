using Nop.Core;
using Nop.Core.Domain.Catalog;
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
            _planSettings = MockRepository.GenerateMock<PlanSettings>();
            _dateTimeHelper = MockRepository.GenerateMock<IDateTimeHelper>();
            _orderProcessingService = MockRepository.GenerateMock<IOrderProcessingService>();

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



        /// <summary>
        /// Envia cualquier llave y debe arrojar error de esta validacion
        /// </summary>
        [Test]
        public void payu_confirmation_invalid_key()
        {
            var request = new PaymentConfirmationRequest();
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
            var request = new PaymentConfirmationRequest();
            request.value = "18000.00";
            request.reference_sale = -20;
            request.currency = "COP";
            request.reference_pol = "123";
            request.state_pol = TransactionState.Approved;

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

            
            var response = (JsonResult)controller.PaymentConfirmation(request);
            Assert.AreEqual((((PaymentResponseErrorModel)response.Data).ErrorCode), PaymentConfirmationErrorCode.InvalidOrderNumber);
        }

    }
}
