using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Estructura que representa la organizacion de categorias del home
    /// </summary>
    public class CategoryOrganizationHomeMenu
    {
        public int CategoryId { get; set; }

        public int ColumnId { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// Diccionario de id de categoria y orden en el que se va mostrar
        /// </summary>
        public List<CategoryOrganizationHomeMenu> ChildrenCategories { get; set; }
    }
}
