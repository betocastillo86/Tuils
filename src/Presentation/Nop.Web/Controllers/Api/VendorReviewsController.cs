using Nop.Core;
using Nop.Services.Vendors;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Core.Domain.Customers;
using Nop.Services.Localization;
using Nop.Web.Framework.Mvc.Api;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Services.Logging;
using Nop.Web.Extensions.Api;
using Nop.Services.Messages;

namespace Nop.Web.Controllers.Api
{
    public class VendorReviewsController : ApiController
    {

        private readonly IVendorService _vendorService;
        private readonly IWorkContext _workContext;
        private readonly CatalogSettings _catalogSettings;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IWorkflowMessageService _workflowMessageService;
        

        public VendorReviewsController (IVendorService vendorService,
            IWorkContext workContext,
            CatalogSettings catalogSettings,
            ILocalizationService localizationService,
            ICustomerActivityService customerActivityService,
            IWorkflowMessageService workflowMessageService)
	    {
            this._vendorService = vendorService;
            this._workContext = workContext;
            this._catalogSettings = catalogSettings;
            this._localizationService = localizationService;
            this._customerActivityService = customerActivityService;
            this._workflowMessageService = workflowMessageService;
	    }


        [HttpPost]
        [AuthorizeApi]
        [Route("api/vendors/{vendorId}/reviews")]
        public IHttpActionResult AddVendorReview(int vendorId, VendorReviewModel model)
        {
            var vendor = _vendorService.GetVendorById(vendorId);
            if (vendor == null || vendor.Deleted)
                return BadRequest("El venedor no existe");

            if (_workContext.CurrentCustomer.IsGuest() && !_catalogSettings.AllowAnonymousUsersToReviewProduct)
            {
                ModelState.AddModelError("sessionVendor", _localizationService.GetResource("Reviews.OnlyRegisteredUsersCanWriteReviews"));
            }

            if (_vendorService.CustomerHasVendorReview(_workContext.CurrentCustomer.Id, vendorId))
            {
                ModelState.AddModelError("hasVendorReview", "Ya dejaste previamente una reseña de este negocio");
            }

            //Valida que el usuario autenticado tenga reviews pendientes
            if (ModelState.IsValid && !_vendorService.CustomerHasVendorReview(_workContext.CurrentCustomer.Id, vendorId))
            {
                //save review
                int rating = model.Rating;
                if (rating < 1 || rating > 5)
                    rating = _catalogSettings.DefaultProductRatingValue;
                bool isApproved = !_catalogSettings.ProductReviewsMustBeApproved;

                var vendorReview = new VendorReview
                {
                    VendorId = vendor.Id,
                    CustomerId = _workContext.CurrentCustomer.Id,
                    Title = model.Title,
                    ReviewText = model.ReviewText,
                    Rating = rating,
                    HelpfulYesTotal = 0,
                    HelpfulNoTotal = 0,
                    IsApproved = isApproved,
                    CreatedOnUtc = DateTime.UtcNow
                };

                vendor.VendorReviews.Add(vendorReview);
                _vendorService.UpdateVendor(vendor);

                //notify store owner
                if (_catalogSettings.NotifyStoreOwnerAboutNewProductReviews)
                    _workflowMessageService.SendVendorReviewNotificationMessage(vendorReview, 2);

                //activity log
                _customerActivityService.InsertActivity("PublicStore.AddVendorReview", _localizationService.GetResource("ActivityLog.PublicStore.AddVendorReview"), vendor.Name);

                model.Id = vendorReview.Id;

                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState.ToErrorString());
            }
        }
    }
}
