﻿using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.ExternalPayU
{
    public class PayUExternalSettings : ISettings
    {
        public string MerchantId { get; set; }

        public string AccountId { get; set; }

        public string ApiKey { get; set; }

        public string ResponseUrl { get; set; }

        public string ConfirmationUrl { get; set; }

        public string UrlPayment { get; set; }
    }
}
