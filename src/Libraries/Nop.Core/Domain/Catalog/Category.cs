using System;
using System.Collections.Generic;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Vendors;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public partial class Category : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported, IStoreMappingSupported
    {
        private ICollection<Discount> _appliedDiscounts;
        private ICollection<SpecialCategoryProduct> _specialCategoriesByProduct;
        private ICollection<SpecialCategoryVendor> _specialCategoriesByVendor;
        private ICollection<Category> _subCategories;
        private ICollection<ManufacturerCategory> _manufacturers;
        

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value of used category template identifier
        /// </summary>
        public int CategoryTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier
        /// </summary>
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers can select the page size
        /// </summary>
        public bool AllowCustomersToSelectPageSize { get; set; }

        /// <summary>
        /// Gets or sets the available customer selectable page size options
        /// </summary>
        public string PageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets the available price ranges
        /// </summary>
        public string PriceRanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the category on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include this category in the top menu
        /// </summary>
        public bool IncludeInTopMenu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this category has discounts applied
        /// <remarks>The same as if we run category.AppliedDiscounts.Count > 0
        /// We use this property for performance optimization:
        /// if this property is set to false, then we do not need to load Applied Discounts navifation property
        /// </remarks>
        /// </summary>
        public bool HasDiscountsApplied { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// False: Permite publicar productos de este tipo de categoría
        /// True: No permite publicar produtos de este tipo de categorua
        /// </summary>
        public bool NotAllowedToPublishProduct { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }


        /// <summary>
        /// Categorias hijas separadas por comas
        /// </summary>
        public string ChildrenCategoriesStr { get; set; }

        /// <summary>
        /// SpecificacionAttributeOption Relacionado a la categoría
        /// </summary>
        public Nullable<int> SpecificationAttributeOptionId { get; set; }

        public virtual SpecificationAttributeOption SpecificationAttributeOption { get; set; }

        /// <summary>
        /// Subcategorias con las que cuenta la principal
        /// </summary>
        public virtual ICollection<Category> SubCategories {
            get { return _subCategories ?? new List<Category>(); }
            set { _subCategories = value; }
        }

        /// <summary>
        /// Marcas que aplican para la categoria
        /// </summary>
        public virtual ICollection<ManufacturerCategory> Manufacturers
        {
            get { return _manufacturers ?? new List<ManufacturerCategory>(); }
            set { _manufacturers = value; }
        }

        /// <summary>
        /// Gets or sets the collection of applied discounts
        /// </summary>
        public virtual ICollection<Discount> AppliedDiscounts
        {
            get { return _appliedDiscounts ?? (_appliedDiscounts = new List<Discount>()); }
            protected set { _appliedDiscounts = value; }
        }
        
        public virtual ICollection<SpecialCategoryProduct> SpecialCategoriesByProduct {
            get { return _specialCategoriesByProduct ?? (_specialCategoriesByProduct = new List<SpecialCategoryProduct>()); }
            protected set { _specialCategoriesByProduct = value; }
        }


        public virtual ICollection<SpecialCategoryVendor> SpecialCategoriesByVendor
        {
            get { return _specialCategoriesByVendor ?? (_specialCategoriesByVendor = new List<SpecialCategoryVendor>()); }
            protected set { _specialCategoriesByVendor = value; }
        }



        
    }
}
