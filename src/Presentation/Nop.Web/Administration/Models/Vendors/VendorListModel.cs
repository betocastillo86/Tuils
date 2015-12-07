using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Core.Domain.Vendors;

namespace Nop.Admin.Models.Vendors
{
    public partial class VendorListModel : BaseNopModel
    {
        [NopResourceDisplayName("Admin.Vendors.List.SearchName")]
        [AllowHtml]
        public string SearchName { get; set; }

        [NopResourceDisplayName("Admin.Vendors.List.ShowOnHome")]
        public bool ShowOnHome { get; set; }

        [NopResourceDisplayName("Admin.Vendors.List.WithPlan")]
        public bool WithPlan { get; set; }
        
        [NopResourceDisplayName("Admin.Vendors.List.VendorPlan")]
        public int VendorType { get; set; }


    }
}