using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.PayUExternal.Models
{
    public class PaymentResponseModel
    {
        /// <summary>
        /// Nombre del plan seleccionado por el usuario
        /// </summary>
        public string SelectedPlanName { get; set; }

        public string ReferenceCode { get; set; }

        public string ReferencePayUCode { get; set; }
        
        public string State { get; set; }

        public string TransactionValue { get; set; }

        public string Currency { get; set; }

        public string TransactionDate { get; set; }

        /// <summary>
        /// True: si es un plan que destaca directamente un producto
        /// False: Si es un plan para las tiendas
        /// </summary>
        public bool IsFeaturedProduct { get; set; }

        /// <summary>
        /// Nombre del producto que se desea destacar
        /// </summary>
        public string ProductName { get; set; }
    }
}
