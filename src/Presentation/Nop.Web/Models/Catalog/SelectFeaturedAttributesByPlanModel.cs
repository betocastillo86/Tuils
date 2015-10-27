using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class SelectFeaturedAttributesByPlanModel
    {
        public ProductOverviewModel ProductDetails { get; set; }

        public int NumProductsOnHomeByPlan { get; set; }
        public int NumProductsOnHomeLeft { get; set; }
        public bool SelectOnHome { get; set; }

        public int NumProductsOnSlidersByPlan { get; set; }
        public int NumProductsOnSlidersLeft { get; set; }
        public bool SelectOnSliders { get; set; }

        public int NumProductsOnSocialNetworksByPlan { get; set; }
        public int NumProductsOnSocialNetworksLeft { get; set; }
        public bool SelectOnSocialNetworks { get; set; }
    }
}