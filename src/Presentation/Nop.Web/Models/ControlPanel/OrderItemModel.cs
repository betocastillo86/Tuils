using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class OrderItemModel : BaseNopEntityModel
    {
        public string Price { get; set; }
        
        public double? Rating { get; set; }

        public bool RatingApproved { get; set; }

        public bool ShowRating { get; set; }

        public string CreatedOn { get; set; }

        public string PlanStartOnUtc { get; set; }

        public string PlanExpirationOnUtc { get; set; }

        public string PaymentStatus { get; set; }

        public string OrderStatus { get; set; }

        public CustomerInfoModel Customer { get; set; }

        public ProductOverviewModel Product { get; set; }

        //public VendorModel Vendor { get; set; }

    }
}