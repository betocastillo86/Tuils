using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public class ManufacturerCategoryMap : NopEntityTypeConfiguration<ManufacturerCategory>
    {
        public ManufacturerCategoryMap()
        {
            ToTable("Manufacturer_Category_Mapping");
        }
    }
}
