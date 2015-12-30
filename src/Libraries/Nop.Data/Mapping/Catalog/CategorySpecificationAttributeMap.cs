using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public class CategorySpecificationAttributeMap: NopEntityTypeConfiguration<CategorySpecificationAttribute>
    {
        public CategorySpecificationAttributeMap()
        {
            this.ToTable("Category_SpecificationAttribute_Mapping");
            this.HasKey(psa => psa.Id);

            this.Property(psa => psa.CustomValue).HasMaxLength(4000);

            this.Ignore(psa => psa.AttributeType);

            this.HasRequired(psa => psa.SpecificationAttributeOption)
                .WithMany(sao => sao.CategorySpecificationAttributes)
                .HasForeignKey(psa => psa.SpecificationAttributeOptionId);


            this.HasRequired(csa => csa.Category)
                .WithMany(c => c.CategorySpecificationAttributes)
                .HasForeignKey(csa => csa.CategoryId);
        }
    }
}
