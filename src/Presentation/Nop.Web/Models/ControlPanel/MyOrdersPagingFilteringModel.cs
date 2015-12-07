using Nop.Web.Framework.UI.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class MyOrdersPagingFilteringModel : BasePageableModel
    {
        public string Filter { get; set; }

        /// <summary>
        /// Solo productos publicados
        /// </summary>
        public bool? p { get; set; }

        /// <summary>
        /// Valor de la consulta por texto
        /// </summary>
        public string q { get; set; }
        
        /// <summary>
        /// Filtro por tipo de producto
        /// </summary>
        public int? pt { get; set; }
    }
}