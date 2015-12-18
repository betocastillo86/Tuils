using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Catalog
{
    public class SearchVendorsModel : BaseNopModel
    {
        public SelectList StateProvinces { get; set; }
    }
}