using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Nop.Web.Validators.Vendors
{
    public class VendorReviewsValidator : BaseNopValidator<VendorReviewsModel>
    {
        public VendorReviewsValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.AddVendorReview.Title).NotEmpty().WithMessage(localizationService.GetResource("Reviews.Fields.Title.Required")).When(x => x.AddVendorReview != null);
            RuleFor(x => x.AddVendorReview.Title).Length(1, 200).WithMessage(string.Format(localizationService.GetResource("Reviews.Fields.Title.MaxLengthValidation"), 200)).When(x => x.AddVendorReview != null && !string.IsNullOrEmpty(x.AddVendorReview.Title));
            RuleFor(x => x.AddVendorReview.ReviewText).NotEmpty().WithMessage(localizationService.GetResource("Reviews.Fields.ReviewText.Required")).When(x => x.AddVendorReview != null);
        }
    }
}