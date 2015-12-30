using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Catalog
{
    public class VersusModel : BaseNopModel
    {
        public VersusModel()
        {
            Categories = new List<CategoryVersusModel>();
        }

        public IList<CategoryVersusModel> Categories { get; set; }

        /// <summary>
        /// Atributo para el año
        /// </summary>
        public int SpecificationAttributeYearId { get; set; }

        // <summary>
        // Listado de categorias que se pueden seleccionar en el combo
        // </summary>
        public List<CategoryBaseModel> CategoriesToSelect { get; set; }

        #region Propiedades filtradas previamente
        public int Year1 { get; set; }

        public int Year2 { get; set; }

        public string Slug1 { get; set; }

        public string Slug2 { get; set; }

        public int? ProductId1 { get; set; }

        public int? ProductId2 { get; set; }
        #endregion
        

        public class CategoryVersusModel : CategoryModel
        {
            public CategoryVersusModel()
            {
            }
            /// <summary>
            /// Principal producto que se desea comparar
            /// </summary>
            public ProductOverviewModel MainProduct { get; set; }

            public int Year { get; set; }
        }

        /// <summary>
        /// Cuando viene filtrado algún producto en especifico
        /// vienen los ids seleccionados
        /// </summary>
        public class VersusCommand
        {
            public int? pId1 { get; set; }

            public int? pId2 { get; set; }
        }

        
    }

    


}