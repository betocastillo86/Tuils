using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class SearchVendorsFilter
    {
        public int? CategoryId { get; set; }

        public int? VendorId { get; set; }

        public int? SubTypeId { get; set; }

        public int StateProvinceId { get; set; }
    }
}