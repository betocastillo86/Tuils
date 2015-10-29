using System;
using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;
using Nop.Core;


namespace Nop.Web.Models.Catalog
{
    public partial class ProductOverviewModel : BaseNopCampaignEntityModel, IComparableModel, IWishableModel
    {

        public ProductOverviewModel()
        {
            ProductPrice = new ProductPriceModel();
            DefaultPictureModel = new PictureModel();
            SpecificationAttributeModels = new List<ProductSpecificationModel>();
            ReviewOverviewModel = new ProductReviewOverviewModel();
            Manufacturers = new List<ManufacturerBriefInfoModel>();
        }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string SeName { get; set; }

        public bool Sold { get; set; }

        //price
        public ProductPriceModel ProductPrice { get; set; }
        //picture
        public PictureModel DefaultPictureModel { get; set; }
        //specification attributes
        public IList<ProductSpecificationModel> SpecificationAttributeModels { get; set; }

        public IList<ManufacturerBriefInfoModel> Manufacturers { get; set; }
        //price
        public ProductReviewOverviewModel ReviewOverviewModel { get; set; }

        public bool DisableWishlistButton { get; set; }

        public int Visits { get; set; }

        public int TotalSales { get; set; }

        public int UnansweredQuestions { get; set; }

        public int ApprovedTotalReviews { get; set; }

        public DateTime AvailableStartDate { get; set; }

        public DateTime AvailableEndDate { get; set; }

        public bool Published { get; set; }

        public bool CompareProductsEnabled { get; set; }

        public bool FeaturedBySpecialCategory { get; set; }

        public int NumClicksForMoreInfo { get; set; }

        public bool HasPlanSelected { get; set; }


		#region Nested Classes

        public partial class ProductPriceModel : BaseNopModel
        {
            public string OldPrice { get; set; }
            public string Price {get;set;}

            public bool DisableBuyButton { get; set; }
            public bool DisableWishlistButton { get; set; }

            public bool AvailableForPreOrder { get; set; }
            public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

            public bool IsRental { get; set; }

            public bool ForceRedirectionAfterAddingToCart { get; set; }

            /// <summary>
            /// A value indicating whether we should display tax/shipping info (used in Germany)
            /// </summary>
            public bool DisplayTaxShippingInfo { get; set; }
        }

		#endregion

        public string StateProvinceName { get; set; }
    }
}