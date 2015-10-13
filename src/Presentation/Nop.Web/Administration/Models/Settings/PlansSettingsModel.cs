using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.Collections.Generic;

namespace Nop.Admin.Models.Settings
{
    public partial class PlansSettingsModel : BaseNopModel
    {
        public PlansSettingsModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableSpecificationAttributeOptions = new List<SelectListItem>();
            AvailableSpecificationAttributes = new List<SelectListItem>();
        }
        
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.CategoryProductPlans")]
        public int CategoryProductPlansId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.CategoryStorePlans")]
        public int CategoryStorePlansId { get; set; }


        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdLimitDays")]
        public int SpecificationAttributeIdLimitDays { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdPictures")]
        public int SpecificationAttributeIdPictures { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdDisplayOrder")]
        public int SpecificationAttributeIdDisplayOrder { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdSliders")]
        public int SpecificationAttributeIdSliders { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdHomePage")]
        public int SpecificationAttributeIdHomePage { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdSocialNetworks")]
        public int SpecificationAttributeIdSocialNetworks { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableSpecificationAttributes { get; set; }

        public IList<SelectListItem> AvailableSpecificationAttributeOptions { get; set; }

    }
}