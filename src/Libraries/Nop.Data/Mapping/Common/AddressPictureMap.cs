using Nop.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Common
{
    public partial class AddressPictureMap : NopEntityTypeConfiguration<AddressPicture>
    {
        public AddressPictureMap()
        {
            this.ToTable("Address_Picture_Mapping");
            this.HasKey(pp => pp.Id);

            this.HasRequired(pp => pp.Picture)
                .WithMany(p => p.VendorPictures)
                .HasForeignKey(pp => pp.PictureId);


            this.HasRequired(pp => pp.Address)
                .WithMany(p => p.AddressPictures)
                .HasForeignKey(pp => pp.AddressId);
        }
    }
}
