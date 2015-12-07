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


        /// <summary>
        /// Puede venir preseleccionado un plan, posiblemente cuando una de las transacciones falla
        /// </summary>
        public int? plan { get; set; }

        /// <summary>
        /// True, es un reintento de un pago fallido
        /// </summary>
        public bool retry { get; set; }
    }
}