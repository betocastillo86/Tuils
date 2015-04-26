using Nop.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Imagenes asociadas al vendedor
    /// </summary>
    public class VendorPicture : BaseEntity
    {
        /// <summary>
        /// Vendedor
        /// </summary>
        public int VendorId { get; set; }

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
        public virtual Vendor Vendor { get; set; }
    
    }
}
