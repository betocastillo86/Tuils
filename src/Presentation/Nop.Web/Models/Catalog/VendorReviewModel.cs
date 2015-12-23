using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Catalog
{
    public partial class VendorReviewOverviewModel : BaseNopModel
    {
        public int VendorId { get; set; }

        public int RatingSum { get; set; }

        public int BestRating { get; set; }

        public int WorstRating { get; set; }

        public int TotalReviews { get; set; }

        public bool ShowSnippets { get; set; }

        public bool AllowCustomerReviews { get; set; }

        public string VendorSeName { get; set; }

        public bool IsVendorDetail { get; set; }
    }

    [Validator(typeof(VendorReviewsValidator))]
    public partial class VendorReviewsModel : BaseNopModel
    {
        public VendorReviewsModel()
        {
            Items = new List<VendorReviewModel>();
            AddVendorReview = new AddVendorReviewModel();
        }
        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public string VendorSeName { get; set; }

        public string VendorUrl { get; set; }

        public IList<VendorReviewModel> Items { get; set; }
        public AddVendorReviewModel AddVendorReview { get; set; }
    }

    public partial class VendorReviewModel : BaseNopEntityModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public bool AllowViewingProfiles { get; set; }

        public string Title { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }

        public VendorReviewHelpfulnessModel Helpfulness { get; set; }

        public string WrittenOnStr { get; set; }
    }


    public partial class VendorReviewHelpfulnessModel : BaseNopModel
    {
        public int VendorReviewId { get; set; }

        public int HelpfulYesTotal { get; set; }

        public int HelpfulNoTotal { get; set; }
    }

    public partial class AddVendorReviewModel : BaseNopModel
    {
        [AllowHtml]
        [System.ComponentModel.DataAnnotations.Required]
        [NopResourceDisplayName("Reviews.Fields.Title")]
        public string Title { get; set; }

        [AllowHtml]
        [System.ComponentModel.DataAnnotations.Required]
        [NopResourceDisplayName("Reviews.Fields.ReviewText")]
        public string ReviewText { get; set; }

        [NopResourceDisplayName("Reviews.Fields.Rating")]
        public int Rating { get; set; }

        public bool DisplayCaptcha { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool CanCurrentCustomerLeaveReview { get; set; }
        public bool SuccessfullyAdded { get; set; }
        public string Result { get; set; }
    }
}