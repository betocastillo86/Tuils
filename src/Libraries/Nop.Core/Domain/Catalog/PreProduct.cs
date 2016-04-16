using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Tabla que contiene los datos de una publicación que no ha sido terminada
    /// </summary>
    public class Preproduct : BaseEntity
    {   
        public int CustomerId { get; set; }

        public string JsonObject { get; set; }

        public string ProductName { get; set; }

        public int ProductTypeId { get; set; }

        public DateTime CreatedOnUtc{ get; set; }

        public Nullable<DateTime> UpdatedOnUtc { get; set; }

        public string UserAgent { get; set; }

        public ProductType ProductType 
        { 
            get {
                return (Catalog.ProductType)this.ProductTypeId;
            }
            set {
                this.ProductTypeId = Convert.ToInt32(value);
            }
        }

        public virtual Customer Customer { get; set; }
    }
}
