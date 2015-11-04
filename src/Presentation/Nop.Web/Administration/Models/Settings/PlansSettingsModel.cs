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
            AvailableStoresPlans = new List<SelectListItem>();
            AvailableProductsPlans = new List<SelectListItem>();
            AvailableSpecificationAttributeOptionsSliders = new List<SelectListItem>();
        }
        
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.CategoryProductPlans")]
        public int CategoryProductPlansId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.CategoryStorePlans")]
        public int CategoryStorePlansId { get; set; }


        /// <summary>
        /// Marca cual es el plan gratis de productos
        /// </summary>
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.PlanProductsFree")]
        public int PlanProductsFree { get; set; }

        /// <summary>
        /// Marca cual es el plan gratis para tiendas
        /// </summary>
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.PlanStoresFree")]
        public int PlanStoresFree { get; set; }


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


        #region tiendas
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributePlanDays")]
        public int SpecificationAttributePlanDays { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdOwnStore")]
        public int SpecificationAttributeIdOwnStore { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdFeaturedManufacturers")]
        public int SpecificationAttributeIdFeaturedManufacturers { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdProductsFeaturedOnSliders")]
        public int SpecificationAttributeIdProductsFeaturedOnSliders { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdProductsOnHomePage")]
        public int SpecificationAttributeIdProductsOnHomePage { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdProductsOnSocialNetworks")]
        public int SpecificationAttributeIdProductsOnSocialNetworks { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdHelpWithStore")]
        public int SpecificationAttributeIdHelpWithStore { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.SpecificationAttributeIdLimitProducts")]
        public int SpecificationAttributeIdLimitProducts { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.RunLikeTest")]
        public bool RunLikeTest { get; set; }
        #endregion


        


        #region Opciones banda rotativa
        
        /// <summary>
        /// Destacado en las bandas de las categorias
        /// </summary>
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedCategories")]
        public int OptionAttributeFeaturedCategories { get; set; }

        /// <summary>
        /// Destacado en las bandas de las marcas
        /// </summary>
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedManufacturers")]
        public int OptionAttributeFeaturedManufacturers { get; set; }

        /// <summary>
        /// Destacado en las bandas de los relacionados
        /// </summary>
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedRelated")]
        public int OptionAttributeFeaturedRelated { get; set; }

        /// <summary>
        /// Destacado en la izquierda
        /// </summary>
        [NopResourceDisplayName("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedLeft")]
        public int OptionAttributeFeaturedLeft { get; set; }
        #endregion


        public IList<SelectListItem> AvailableCategories { get; set; }


        public IList<SelectListItem> AvailableProductsPlans { get; set; }

        public IList<SelectListItem> AvailableStoresPlans { get; set; }

        public IList<SelectListItem> AvailableSpecificationAttributes { get; set; }

        public IList<SelectListItem> AvailableSpecificationAttributeOptions { get; set; }

        
        public IList<SelectListItem> AvailableSpecificationAttributeOptionsSliders { get; set; }


    }
}