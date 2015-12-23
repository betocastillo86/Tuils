using Nop.Core.Domain.Vendors;

namespace Nop.Data.Mapping.Vendors
{
    public partial class VendorReviewHelpfulnessMap : NopEntityTypeConfiguration<VendorReviewHelpfulness>
    {
        public VendorReviewHelpfulnessMap()
        {
            this.ToTable("VendorReviewHelpfulness");
            this.HasKey(pr => pr.Id);

            this.HasRequired(prh => prh.VendorReview)
                .WithMany(pr => pr.VendorReviewHelpfulnessEntries)
                .HasForeignKey(prh => prh.VendorReviewId).WillCascadeOnDelete(true);
        }
    }
}