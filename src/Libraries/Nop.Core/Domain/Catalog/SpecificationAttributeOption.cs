using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a specification attribute option
    /// </summary>
    public partial class SpecificationAttributeOption : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        private ICollection<ProductSpecificationAttribute> _productSpecificationAttributes;
        private ICollection<CategorySpecificationAttribute> _categorySpecificationAttributes;
        

        /// <summary>
        /// Gets or sets the specification attribute identifier
        /// </summary>
        public int SpecificationAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
        
        /// <summary>
        /// Gets or sets the specification attribute
        /// </summary>
        public virtual SpecificationAttribute SpecificationAttribute { get; set; }

        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>
        public virtual ICollection<ProductSpecificationAttribute> ProductSpecificationAttributes
        {
            get { return _productSpecificationAttributes ?? (_productSpecificationAttributes = new List<ProductSpecificationAttribute>()); }
            protected set { _productSpecificationAttributes = value; }
        }


        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>
        public virtual ICollection<CategorySpecificationAttribute> CategorySpecificationAttributes
        {
            get { return _categorySpecificationAttributes ?? (_categorySpecificationAttributes = new List<CategorySpecificationAttribute>()); }
            protected set { _categorySpecificationAttributes = value; }
        }
    }
}
