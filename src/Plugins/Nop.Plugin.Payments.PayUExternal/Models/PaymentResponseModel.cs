using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.PayUExternal.Models
{
    public class PaymentResponseModel
    {
        public int OrderId { get; set; }
        
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
        /// Si la transaccion fue rechazada o no
        /// </summary>
        public bool TransactionRejected { get; set; }

        /// <summary>
        /// Nombre del producto que se desea destacar
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// True: Debe mostrar un link que lleve al usuario a destacar su producto entre las caracteristicas posibles existentes
        /// </summary>
        public bool RedirectToFeatureProduct { get; set; }


        /// <summary>
        /// Llave con la que deben redireccionar para destacar el producto
        /// </summary>
        public string RedirectToFeaturedKey { get; set; }
        
        /// <summary>
        /// Nombre del producto que se desea destacar
        /// </summary>
        public string ProductName { get; set; }

    }
}
