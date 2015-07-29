using Nop.Core.Domain.Vendors;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class SpecialCategoryVendorModel : BaseNopEntityModel
    {

        public int CategoryId { get; set; }

        public int VendorId { get; set; }

        public int SpecialTypeId { get; set; }

        public SpecialCategoryVendorType SpecialType
        {
            get { return (SpecialCategoryVendorType)this.SpecialTypeId; }

            set { this.SpecialTypeId = (int)value; }
        }

        public string VendorName { get; set; }

        public string CategoryName { get; set; }

        public string CategorySeName { get; set; }
    }
}