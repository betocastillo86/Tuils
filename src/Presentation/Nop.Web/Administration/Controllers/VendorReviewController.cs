using Nop.Admin.Controllers;
using Nop.Admin.Models.Vendors;
using Nop.Core.Domain.Vendors;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework;

namespace Nop.Admin.Controllers
{
    public class VendorReviewController : BaseAdminController
    {
         #region Fields

        private readonly IVendorService _vendorService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IOrderService _orderService;
       

        #endregion Fields

        #region Constructors

        public VendorReviewController(IVendorService vendorService, IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService, IPermissionService permissionService,
            IOrderService orderService)
        {
            this._vendorService = vendorService;
            this._dateTimeHelper = dateTimeHelper;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._orderService = orderService;
            this._vendorService = vendorService;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareProductReviewModel(VendorReviewModel model,
            VendorReview vendorReview, bool excludeProperties, bool formatReviewText)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (vendorReview == null)
                throw new ArgumentNullException("productReview");

            model.Id = vendorReview.Id;
            model.VendorId = vendorReview.VendorId;
            model.VendorName = vendorReview.Vendor.Name;
            model.CustomerId = vendorReview.CustomerId;
            var customer = vendorReview.Customer;
            model.CustomerInfo = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
            model.Rating = vendorReview.Rating;
            model.CreatedOn = _dateTimeHelper.ConvertToUserTime(vendorReview.CreatedOnUtc, DateTimeKind.Utc);
            if (!excludeProperties)
            {
                model.Title = vendorReview.Title;
                if (formatReviewText)
                    model.ReviewText = Core.Html.HtmlHelper.FormatText(vendorReview.ReviewText, false, true, false, false, false, false);
                else
                    model.ReviewText = vendorReview.ReviewText;
                model.IsApproved = vendorReview.IsApproved;
            }
        }

        #endregion

        #region Methods

        //list
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProductReviews))
                return AccessDeniedView();

            var model = new VendorReviewListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, VendorReviewListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProductReviews))
                return AccessDeniedView();

            DateTime? createdOnFromValue = (model.CreatedOnFrom == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.CreatedOnFrom.Value, _dateTimeHelper.CurrentTimeZone);

            DateTime? createdToFromValue = (model.CreatedOnTo == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.CreatedOnTo.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

            var productReviews = _vendorService.GetAllVendorReviews(0, null, 
                createdOnFromValue, createdToFromValue, model.SearchText);
            var gridModel = new DataSourceResult
            {
                Data = productReviews.PagedForCommand(command).Select(x =>
                {
                    var m = new VendorReviewModel();
                    PrepareProductReviewModel(m, x, false, true);
                    return m;
                }),
                Total = productReviews.Count,
            };

            return Json(gridModel);
        }

        //edit
        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProductReviews))
                return AccessDeniedView();

            var vendorReview = _vendorService.GetVendorReviewById(id);
            if (vendorReview == null)
                //No product review found with the specified id
                return RedirectToAction("List");

            var model = new VendorReviewModel();
            PrepareProductReviewModel(model, vendorReview, false, false);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(VendorReviewModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProductReviews))
                return AccessDeniedView();

            var vendorReview = _vendorService.GetVendorReviewById(model.Id);
            if (vendorReview == null)
                //No product review found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                vendorReview.Title = model.Title;
                vendorReview.ReviewText = model.ReviewText;
                vendorReview.IsApproved = model.IsApproved;
                _vendorService.UpdateVendor(vendorReview.Vendor);
                
                //update product totals
                //_vendorService.UpdateVendorReviewTotals(vendorReview.Vendor);

                //Marca como hecha la calificacion
                _vendorService.UpdateRatingsTotal(vendorReview.VendorId);
                    

                SuccessNotification(_localizationService.GetResource("Admin.Catalog.ProductReviews.Updated"));
                return continueEditing ? RedirectToAction("Edit", vendorReview.Id) : RedirectToAction("List");
            }


            //If we got this far, something failed, redisplay form
            PrepareProductReviewModel(model, vendorReview, true, false);
            return View(model);
        }
        
        //delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProductReviews))
                return AccessDeniedView();

            var vendorReview = _vendorService.GetVendorReviewById(id);
            if (vendorReview == null)
                //No product review found with the specified id
                return RedirectToAction("List");

            var vendor = vendorReview.Vendor;
            _vendorService.DeleteVendorReview(vendorReview);
            //update product totals
            _vendorService.UpdateRatingsTotal(vendor.Id);

            SuccessNotification(_localizationService.GetResource("Admin.Catalog.ProductReviews.Deleted"));
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult ApproveSelected(ICollection<int> selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProductReviews))
                return AccessDeniedView();

            if (selectedIds != null)
            {
                foreach (var id in selectedIds)
                {
                    var vendorReview = _vendorService.GetVendorReviewById(id);
                    if (vendorReview != null)
                    {
                        vendorReview.IsApproved = true;
                        _vendorService.UpdateVendor(vendorReview.Vendor);
                        //update product totals
                        //_vendorService.UpdateVendorReviewTotals(vendorReview.Vendor);
                    }
                }
            }

            return Json(new { Result = true });
        }

        [HttpPost]
        public ActionResult DisapproveSelected(ICollection<int> selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProductReviews))
                return AccessDeniedView();

            if (selectedIds != null)
            {
                foreach (var id in selectedIds)
                {
                    var productReview = _vendorService.GetVendorReviewById(id);
                    if (productReview != null)
                    {
                        productReview.IsApproved = false;
                        _vendorService.UpdateVendor(productReview.Vendor);
                        //update product totals
                        //_vendorService.UpdateProductReviewTotals(productReview.Product);
                    }
                }
            }

            return Json(new { Result = true });
        }

        #endregion
    }
}