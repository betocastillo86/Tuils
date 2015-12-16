﻿using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Core.Domain.Vendors;

namespace Nop.Web.Models.Catalog
{
    public partial class VendorNavigationModel : BaseNopModel
    {
        public VendorNavigationModel()
        {
            this.Vendors = new List<VendorBriefInfoModel>();
        }

        public IList<VendorBriefInfoModel> Vendors { get; set; }

        public int TotalVendors { get; set; }
    }

    public partial class VendorBriefInfoModel : BaseNopEntityModel
    {
        public string Name { get; set; }

        public string SeName { get; set; }

        public bool VendorShippingEnabled { get; set; }

        public bool CreditCardEnabled { get; set; }

        public string PhoneNumber { get; set; }

        public VendorType VendorType { get; set; }

        public VendorSubType VendorSubType { get; set; }
    }
}