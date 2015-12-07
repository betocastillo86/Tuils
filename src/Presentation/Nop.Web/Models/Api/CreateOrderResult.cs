using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class CreateOrderResult
    {
        public string Signature { get; set; }

        public string ReferenceCode { get; set; }

        public string Amount { get; set; }

        public string AccountId { get; set; }

        public string MerchantId { get; set; }

        public string Currency { get; set; }

        public string ResponseUrl { get; set; }

        public string ConfirmationUrl { get; set; }

        public string UrlPayment { get; set; }
    }
}