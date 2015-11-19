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

        public bool ShowGoToMyProductsButton { get; set; }

        public bool ShowFinishPublishingButton { get; set; }

        /// <summary>
        /// Oculta el botón en los cassos que sea un proceso de publicación y no tenga cupo para destacar
        /// </summary>
        public bool HideFeatureButton { get { return HasReachedLimitOfFeature && ShowFinishPublishingButton; } }

        public bool HasReachedLimitOfProducts { get; set; }

        public bool HasReachedLimitOfFeature { get; set; }

        public int NumLimitOfProducts { get; set; }
    }
}