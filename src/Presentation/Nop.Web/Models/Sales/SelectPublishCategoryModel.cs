using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class SelectPublishCategoryModel
    {
        /// <summary>
        /// Valida si ha alcanzado el limite de publicaciones
        /// </summary>
        public bool HasReachedLimitOfProducts { get; set; }

        public int NumLimitOfProducts { get; set; }
        public bool CanSelectService { get; set; }
    }
}