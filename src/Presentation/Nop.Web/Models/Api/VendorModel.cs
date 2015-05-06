using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class VendorModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool EnableShipping { get; set; }

        public bool EnableCreditCardPayment { get; set; }

        public int? BackgroundPosition { get; set; }
    }
}