using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Contiene la relacion entre la tabla de Marcas y Categorias
    /// </summary>
    public class ManufacturerCategory:  BaseEntity
    {
        public int ManufacturerId { get; set; }

        public int CategoryId { get; set; }

        /// <summary>
        /// Marca desctacada dentro de la categoria
        /// </summary>
        public bool IsFeaturedManufacturer { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Category Category { get; set; }
    }
}
