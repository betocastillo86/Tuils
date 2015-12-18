using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class VendorModel : BaseApiModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public bool EnableShipping { get; set; }

        public bool EnableCreditCardPayment { get; set; }

        public int? BackgroundPosition { get; set; }

        public string  PictureUrl { get; set; }

        public string SeName { get; set; }
    }
}