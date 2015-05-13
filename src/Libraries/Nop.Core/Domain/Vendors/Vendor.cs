using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Seo;
using System.Collections.Generic;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Represents a vendor
    /// </summary>
    public partial class Vendor : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        private ICollection<SpecialCategoryVendor> _specialCategoryVendors;
        private ICollection<Address> _addresses;
        
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }


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
        /// Tipo de vendedor que realiza la transacción 
        /// </summary>
        public int VendorTypeId { get; set; }


        public bool? EnableCreditCardPayment { get; set; }

        public bool? EnableShipping { get; set; }

        public int? PictureId { get; set; }

        public int? BackgroundPictureId { get; set; }

        public double? AvgRating { get; set; }

        public int? BackgroundPosition { get; set; }


        public VendorType VendorType
        {
            get { return (VendorType)VendorTypeId; }
        }


        public virtual Picture Picture { get; set; }

        public virtual Picture BackgroundPicture { get; set; }

        /// <summary>
        /// Numero de veces que ha sido calificado un producto vendido por el usuario
        /// </summary>
        public int NumRatings { get; set; }

        /// <summary>
        /// Direcciones del vendedor
        /// </summary>
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            protected set { _addresses = value; }
        }
        
        /// <summary>
        /// Listado de categorias especiales de un producto
        /// </summary>
        public virtual ICollection<SpecialCategoryVendor> SpecialCategories
        {
            get { return _specialCategoryVendors ?? (_specialCategoryVendors = new List<SpecialCategoryVendor>()); }
            protected set { _specialCategoryVendors = value; }
        }


    }
}


