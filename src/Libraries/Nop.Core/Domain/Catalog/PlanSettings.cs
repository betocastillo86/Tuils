using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public class PlanSettings : ISettings
    {
        public int CategoryProductPlansId { get; set; }

        public int CategoryStorePlansId { get; set; }

        /// <summary>
        /// Marca cual es el plan gratis de productos
        /// </summary>
        public int PlanProductsFree { get; set; }

        /// <summary>
        /// Marca cual es el plan gratis para tiendas
        /// </summary>
        public int PlanStoresFree { get; set; }

        public int SpecificationAttributeIdPictures { get; set; }

        public int SpecificationAttributeIdDisplayOrder { get; set; }

        public int SpecificationAttributeIdSliders { get; set; }

        public int SpecificationAttributeIdHomePage { get; set; }

        public int SpecificationAttributeIdSocialNetworks { get; set; }

        public bool RunLikeTest { get; set; }


        #region Tiendas
        public int SpecificationAttributePlanDays { get; set; }
        public int SpecificationAttributeIdOwnStore { get; set; }
        public int SpecificationAttributeIdFeaturedManufacturers { get; set; }
        public int SpecificationAttributeIdProductsFeaturedOnSliders { get; set; }
        public int SpecificationAttributeIdProductsOnHomePage { get; set; }
        public int SpecificationAttributeIdProductsOnSocialNetworks { get; set; }
        public int SpecificationAttributeIdHelpWithStore { get; set; }
        
        public int SpecificationAttributeIdLimitProducts { get; set; }
        #endregion
        

        #region Opciones banda rotativa

        /// <summary>
        /// Destacado en las bandas de las categorias
        /// </summary>
        public int OptionAttributeFeaturedCategories { get; set; }

        /// <summary>
        /// Destacado en las bandas de las marcas
        /// </summary>
        public int OptionAttributeFeaturedManufacturers { get; set; }

        /// <summary>
        /// Destacado en las bandas de los relacionados
        /// </summary>
        public int OptionAttributeFeaturedRelated { get; set; }

        /// <summary>
        /// Destacado en la izquierda
        /// </summary>
        public int OptionAttributeFeaturedLeft { get; set; }
        #endregion

       
    }
}
