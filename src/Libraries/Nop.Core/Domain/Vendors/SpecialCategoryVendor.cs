using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Son categorias destacadas que se relacionan con el vendedor como:
    /// - Referencias de motocicletas especiales
    /// - Servicios especializados
    /// - Recepción de cotizaciones
    /// </summary>
    public partial class SpecialCategoryVendor : BaseEntity
    {
        public int CategoryId { get; set; }

        public int VendorId { get; set; }

        public int SpecialTypeId { get; set; }

        public SpecialCategoryVendorType SpecialType
        {
            get { return (SpecialCategoryVendorType)this.SpecialTypeId; }

            set { this.SpecialTypeId = (int)value; }
        }

        public virtual Vendor Vendor { get; set; }

        public virtual Category Category { get; set; }

    }
}
