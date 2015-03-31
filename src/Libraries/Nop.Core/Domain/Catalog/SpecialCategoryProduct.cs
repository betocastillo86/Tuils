using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Son categorias especiales que permiten funcionamientos diferentes al de filtro 
    /// Por ejemplo: Referencias de motocicletas en las que un producto es relevante
    /// ---La referencia de la moto es una categoria
    /// </summary>
    public partial class SpecialCategoryProduct : BaseEntity
    {
        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public int SpecialTypeId { get; set; }

        public SpecialCategoryProductType SpecialType
        {
            get { return (SpecialCategoryProductType)this.SpecialTypeId; }

            set { this.SpecialTypeId = (int)value; }
        }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }

    }
}
