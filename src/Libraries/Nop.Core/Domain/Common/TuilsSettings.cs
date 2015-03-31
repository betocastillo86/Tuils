using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Common
{
    public class TuilsSettings : ISettings
    {
        /// <summary>
        /// Categoria principal para los productos
        /// </summary>
        public int productBaseTypes_product { get; set; }

        /// <summary>
        /// Categoria principal para los Servicios
        /// </summary>
        public int productBaseTypes_service { get; set; }

        /// <summary>
        /// Categoria principal para las Motos 
        /// </summary>
        public int productBaseTypes_bike { get; set; }

    }
}
