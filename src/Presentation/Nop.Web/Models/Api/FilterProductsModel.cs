using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class FilterProductsModel
    {
        public bool? OnHome { get; set; }

        public bool? OnSliders { get; set; }

        /// <summary>
        /// En redes sociales
        /// </summary>
        public bool? OnSN { get; set; }
    }
}