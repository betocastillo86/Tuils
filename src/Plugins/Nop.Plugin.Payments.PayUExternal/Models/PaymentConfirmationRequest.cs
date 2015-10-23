using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.PayUExternal.Models
{
    /// <summary>
    /// Clase que contiene las variables que se envian en un llamado POST de PayU
    /// http://developers.payulatam.com/es/web_checkout/integration.html
    /// </summary>
    public class PaymentConfirmationRequest
    {
        public int merchant_id { get; set; }

        public TransactionState state_pol { get; set; }

        public decimal risk { get; set; }

        public string response_code_pol { get; set; }

        public int reference_sale { get; set; }

        public string reference_pol { get; set; }

        public string sign { get; set; }

        public string extra1 { get; set; }

        public string extra2 { get; set; }

        public int payment_method { get; set; }

        public PaymentMethodType payment_method_type { get; set; }

        public int installments_number { get; set; }

        public string value {get; set; }

        public decimal ValueDecimal { 
            get { 
                return Convert.ToDecimal(this.value, new System.Globalization.CultureInfo("en-US")); 
            } 
        }

        public decimal tax { get; set; }

        public int additional_value { get; set; }

        public string transaction_date { get; set; }

        public string currency { get; set; }

        public string email_buyer { get; set; }

        public string cus { get; set; }

        public string pse_bank { get; set; }

        public bool test { get; set; }

        public string description { get; set; }

        public string billing_address { get; set; }

        public string shipping_address { get; set; }

        public string phone { get; set; }

        public string office_phone { get; set; }

        public string account_number_ach { get; set; }

        public string account_type_ach { get; set; }

        public decimal administrative_fee { get; set; }

        public decimal administrative_fee_base { get; set; }

        public decimal administrative_fee_tax { get; set; }

        public string airline_code { get; set; }

        public int attempts { get; set; }

        public string authorization_code { get; set; }

        public string bank_id { get; set; }

        public string billing_city { get; set; }

        public string billing_country { get; set; }

        public decimal commision_pol { get; set; }

        public string commision_pol_currency { get; set; }

        public int customer_number { get; set; }

        public string date { get; set; }

        public string error_code_bank { get; set; }

        public string error_message_bank { get; set; }

        public decimal exchange_rate { get; set; }

        public string ip { get; set; }

        public string nickname_buyer { get; set; }

        public string nickname_seller { get; set; }

        public PaymentMethodType payment_method_id { get; set; }

        public string payment_request_state { get; set; }

        public string pseReference1 { get; set; }

        public string pseReference2 { get; set; }

        public string pseReference3 { get; set; }

        public string response_message_pol { get; set; }

        public string shipping_city { get; set; }

        public string shipping_country { get; set; }

        public string transaction_bank_id { get; set; }

        public string transaction_id { get; set; }

        public string payment_method_name { get; set; }

    }
}
