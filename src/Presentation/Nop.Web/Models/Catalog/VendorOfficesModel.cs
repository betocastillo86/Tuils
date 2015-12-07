using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class VendorOfficesModel
    {
        public List<AddressModel> Offices { get; set; }

        public VendorModel Vendor { get; set; }

        public bool AllowEdit { get; set; }
    }
}