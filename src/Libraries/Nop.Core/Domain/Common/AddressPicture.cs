using Nop.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Common
{
    /// <summary>
    /// Imagenes asociadas al vendedor
    /// </summary>
    public class AddressPicture : BaseEntity
    {
        /// <summary>
        /// Direccion
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Imagen que lo relaciona
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Orden en el que se muestra
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Imagen
        /// </summary>
        public virtual Picture Picture { get; set; }

        /// <summary>
        /// Vendedor
        /// </summary>
        public virtual Address Address { get; set; }

    }
}
