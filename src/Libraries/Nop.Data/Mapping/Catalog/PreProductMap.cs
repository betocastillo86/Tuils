using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public class PreProductMap : NopEntityTypeConfiguration<Preproduct>
    {
        public PreProductMap()
        {
            this.ToTable("PreProduct");
            this.HasKey(p => p.Id);
            this.Property(p => p.JsonObject).IsRequired();
            this.Property(p => p.ProductName).HasMaxLength(400);
            
            
            this.Ignore(p => p.ProductType);
        }
    }
}
