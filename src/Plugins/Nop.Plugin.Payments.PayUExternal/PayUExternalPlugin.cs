using Nop.Core.Plugins;
using Nop.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.ExternalPayU
{
    public class PayUExternalPlugin : BasePlugin, IPaymentMethod
    {
        public readonly PayUExternalSettings _settings;
        public PayUExternalPlugin(PayUExternalSettings settings)
        {
            this._settings = settings;
        }

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            //Unicamente cambia el estado del pago
            var result = new ProcessPaymentResult();
            result.NewPaymentStatus = Core.Domain.Payments.PaymentStatus.Pending;

            //Se agregan las llaves complementarias
            result.AdditionalKeys.Add("MerchantId", _settings.MerchantId);
            result.AdditionalKeys.Add("AccountId", _settings.AccountId);
            //result.AdditionalKeys.Add("Signature", GenerateSignature(referenceCode, amount, currency));
            result.AdditionalKeys.Add("ResponseUrl", _settings.ResponseUrl);
            result.AdditionalKeys.Add("ConfirmationUrl", _settings.ConfirmationUrl);
            result.AdditionalKeys.Add("UrlPayment", _settings.UrlPayment);

            return result;
        }


        /// <summary>
        /// El signature es una forma única de validar los pagos realizados a través de la plataforma, 
        /// garantizando su autenticidad. Consiste en una cadena de caracteres a la cual se le aplica algoritmo MD5 o SHA para encriptarla. 
        /// La cadena está compuesta de la siguiente forma:
        /// “ApiKey~merchantId~referenceCode~amount~currency”
        /// </summary>
        /// <param name="referenceCode">Codigo de referencia unico para la transaccion</param>
        /// <returns></returns>
        private string GenerateSignature(string referenceCode, string amount, string currency)
        {
            return Nop.Utilities.Cryptography.MD5(string.Join("~", _settings.ApiKey, _settings.MerchantId, referenceCode, amount, currency)).ToString();
        }

        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            //Asigna las llaves que serán usadas para crear el signature
            string referenceCode = (postProcessPaymentRequest.Order.Id + 10000).ToString();
            string amount = postProcessPaymentRequest.Order.OrderTotal.ToString("0");
            string currency = postProcessPaymentRequest.Order.CustomerCurrencyCode;
            postProcessPaymentRequest.Signature = GenerateSignature(referenceCode, amount, currency);
            postProcessPaymentRequest.ReferenceCode = referenceCode;
        }

        public bool HidePaymentMethod(IList<Core.Domain.Orders.ShoppingCartItem> cart)
        {
            return false;
        }

        public decimal GetAdditionalHandlingFee(IList<Core.Domain.Orders.ShoppingCartItem> cart)
        {
            return 0;
        }

        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            throw new NotImplementedException();
        }

        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public bool CanRePostProcessPayment(Core.Domain.Orders.Order order)
        {
            throw new NotImplementedException();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "PayUExternal";
            routeValues = new System.Web.Routing.RouteValueDictionary { { "Namespaces", "Nop.Plugin.Payments.PayUExternal.Controllers" }, { "area", null } };
        }

        public void GetPaymentInfoRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public Type GetControllerType()
        {
            throw new NotImplementedException();
        }

        public bool SupportCapture
        {
            get { return false; }
        }

        public bool SupportPartiallyRefund
        {
            get { return false; }
        }

        public bool SupportRefund
        {
            get { return false; }
        }

        public bool SupportVoid
        {
            get { return false; }
        }

        public RecurringPaymentType RecurringPaymentType
        {
            get { return Services.Payments.RecurringPaymentType.NotSupported; }
        }

        public PaymentMethodType PaymentMethodType
        {
            get { return Services.Payments.PaymentMethodType.Redirection; }
        }

        public bool SkipPaymentInfo
        {
            get { return true; }
        }
    }
}
