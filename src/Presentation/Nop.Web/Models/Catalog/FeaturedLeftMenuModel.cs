using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class FeaturedLeftMenuModel : BaseNopModel
    {
        public IList<ProductOverviewModel> Products { get; set; }
    }
}