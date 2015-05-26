using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public partial class CategoryMap : NopEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Category");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
            this.Property(c => c.MetaKeywords).HasMaxLength(400);
            this.Property(c => c.MetaTitle).HasMaxLength(400);
            this.Property(c => c.PriceRanges).HasMaxLength(400);
            this.Property(c => c.PageSizeOptions).HasMaxLength(200);
            this.Property(c => c.ChildrenCategoriesStr).IsMaxLength();

            this.Ignore(c => c.SubCategories);

            this.HasMany(c => c.SpecialCategoriesByProduct)
                .WithRequired(cs => cs.Category)
                .WillCascadeOnDelete(false);

            this.HasMany(c => c.Manufacturers)
                .WithRequired(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .WillCascadeOnDelete(false);

        }
    }
}