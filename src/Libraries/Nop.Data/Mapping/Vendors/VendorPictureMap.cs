using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Vendors
{
    public partial class VendorPictureMap : NopEntityTypeConfiguration<VendorPicture>
    {
        public VendorPictureMap()
        {
            this.ToTable("Vendor_Picture_Mapping");
            this.HasKey(pp => pp.Id);

            this.HasRequired(pp => pp.Picture)
                .WithMany(p => p.VendorPictures)
                .HasForeignKey(pp => pp.PictureId);


            this.HasRequired(pp => pp.Vendor)
                .WithMany(p => p.VendorPictures)
                .HasForeignKey(pp => pp.VendorId);
        }
    }
}
