using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Plugin.Payments.PayUExternal.Models
{
    /// <summary>
    /// Contenido de la respuesta que se le da al usuario desde pagina de pagos
    /// </summary>
    public class PaymentResponseRequest
    {
        public int merchantId { get; set; }

        public TransactionState transactionState { get; set; }

        public decimal risk { get; set; }

        public string polResponseCode { get; set; }

        public int referenceCode { get; set; }

        public string reference_pol { get; set; }

        public string signature { get; set; }

        public string polPaymentMethod { get; set; }

        public PaymentMethodType polPaymentMethodType { get; set; }

        public int installmentsNumber { get; set; }

        public decimal TX_VALUE { get; set; }

        public decimal TX_TAX { get; set; }

        public string buyerEmail { get; set; }

        public string processingDate { get; set; }

        public string currency { get; set; }

        public string cus { get; set; }

        public string pseBank { get; set; }

        public string lng { get; set; }

        public string description { get; set; }

        public string lapResponseCode { get; set; }

        public string lapPaymentMethod { get; set; }

        public string lapPaymentMethodType { get; set; }

        public int lapTransactionState { get; set; }

        public string message { get; set; }

        /// <summary>
        /// Id del producto que se desea destacar en el caso de producto simple
        /// </summary>
        public string extra1 { get; set; }

        public string extra2 { get; set; }

        public string extra3 { get; set; }

        public string authorizationCode { get; set; }


        public string merchant_address { get; set; }

        public string merchant_name { get; set; }

        public string merchant_url { get; set; }

        public string orderLanguage { get; set; }

        public int pseCycle { get; set; }

        public string pseReference1 { get; set; }

        public string pseReference2 { get; set; }

        public string pseReference3 { get; set; }

        public string telephone { get; set; }

        public string transactionId { get; set; }


        public string trazabilityCode { get; set; }

        public decimal TX_ADMINISTRATIVE_FEE { get; set; }

        public decimal TX_TAX_ADMINISTRATIVE_FEE { get; set; }

        public decimal TX_TAX_ADMINISTRATIVE_FEE_RETURN_BASE { get; set; }

    }
}