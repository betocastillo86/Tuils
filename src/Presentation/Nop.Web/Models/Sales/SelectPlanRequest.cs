using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class SelectPlanRequest
    {
        /// <summary>
        /// Fuerza ser mostrado la seleccion del plan. Normalmente es para hacer un upgrade del plan
        /// </summary>
        public bool? force { get; set; }

        /// <summary>
        /// Limite de productos que tiene seleccionado en el plan actual
        /// </summary>
        public int? limit { get; set; }
    }
}