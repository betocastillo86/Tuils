using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Vendors
{
    public class SpecialCategoryVendorMap : NopEntityTypeConfiguration<SpecialCategoryVendor>
    {
        public SpecialCategoryVendorMap()
        {
            this.ToTable("SpecialCategoryVendor");
            this.Ignore(c => c.SpecialType);
        }
    }
}
