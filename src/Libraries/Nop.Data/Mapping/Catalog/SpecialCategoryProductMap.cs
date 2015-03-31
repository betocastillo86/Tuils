using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public class SpecialCategoryProductMap : NopEntityTypeConfiguration<SpecialCategoryProduct>
    {
        public SpecialCategoryProductMap()
        {
            this.ToTable("SpecialCategoryProduct");
            this.Ignore(c => c.SpecialType);
        }
    }
}
