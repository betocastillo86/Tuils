using Nop.Core.Domain.Vendors;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class ConfirmationWithoutPlanModel : BaseNopModel
    {
        public ConfirmationWithoutPlanModel()
        {
            ProductDetails = new ProductOverviewModel();
        }
        
        public ProductOverviewModel ProductDetails { get; set; }

        public int LimitDaysOfProductPublished { get; set; }

        public VendorType VendorType { get; set; }

    }
}