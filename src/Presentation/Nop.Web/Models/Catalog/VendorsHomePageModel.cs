using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class VendorsHomePageModel : BaseNopModel
    {
        public List<VendorModel> Vendors { get; set; }
    }
}