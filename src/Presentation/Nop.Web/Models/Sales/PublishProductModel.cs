using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class PublishProductModel : BaseNopModel
    {
        public List<CategoryBaseModel> BikeBrands { get; set; }
    }
}