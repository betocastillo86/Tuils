using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class ManufacturerHomePageModel
    {
        public bool Enable { get; set; }

        public List<ManufacturerModel> Manufacturers { get; set; }

        /// <summary>
        /// Ya que hay algunas categorías que funcionan como marcas se van a mezclar
        /// </summary>
        public List<CategoryModel> Categories { get; set; }
    }
}