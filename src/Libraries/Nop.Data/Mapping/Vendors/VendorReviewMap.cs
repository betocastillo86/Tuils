using Nop.Core.Domain.Vendors;

namespace Nop.Data.Mapping.Vendors
{
    public partial class VendorReviewMap : NopEntityTypeConfiguration<VendorReview>
    {
        public VendorReviewMap()
        {
            this.ToTable("VendorReview");
            this.HasKey(pr => pr.Id);

            this.HasRequired(pr => pr.Vendor)
                .WithMany(p => p.VendorReviews)
                .HasForeignKey(pr => pr.VendorId);

            this.HasRequired(pr => pr.Customer)
                .WithMany()
                .HasForeignKey(pr => pr.CustomerId);

        }
    }
}