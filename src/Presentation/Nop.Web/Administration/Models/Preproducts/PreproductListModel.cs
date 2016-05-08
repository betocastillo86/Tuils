using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Models.Preproducts
{
    public class PreproductListModel
    {
        [NopResourceDisplayName("Admin.Catalog.Preproducts.List.SearchCustomerName")]
        [AllowHtml]
        public string SearchCustomerName { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Preproducts.List.SearchCustomerEmail")]
        [AllowHtml]
        public string SearchCustomerEmail { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Preproducts.List.SearchProductName")]
        [AllowHtml]
        public string SearchProductName { get; set; }
    }
}