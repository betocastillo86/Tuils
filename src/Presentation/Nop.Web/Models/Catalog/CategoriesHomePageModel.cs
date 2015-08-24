using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class CategoriesHomePageModel : BaseNopModel
    {

        /// <summary>
        /// Diccionario con el nombre de la columna y los detalles de las categorias dentro
        /// </summary>
        public List<CategoryHomePageModel> Categories { get; set; }

        /// <summary>
        /// Nujmero de columnas
        /// </summary>
        public int NumColumns { get; set; }
        
        /// <summary>
        /// Estructura de la clase que se muestra en el home
        /// </summary>
        public class CategoryHomePageModel
        {
            public int Column { get; set; }
            
            public int CategoryId { get; set; }

            public string Name { get; set; }

            public string SeName { get; set; }

            public int Order { get; set; }

            public PictureModel PictureModel { get; set; }

            public List<CategoryHomePageModel> ChildrenCategories { get; set; }
        }
    }
}