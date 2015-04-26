using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;

namespace Nop.Core.Domain.Media
{
    /// <summary>
    /// Represents a picture
    /// </summary>
    public partial class Picture : BaseEntity
    {
        private ICollection<ProductPicture> _productPictures;
        private ICollection<VendorPicture> _vendorPictures;
        private ICollection<Vendor> _vendorBackgroundPictures;
        

        
        /// <summary>
        /// Gets or sets the picture binary
        /// </summary>
        public byte[] PictureBinary { get; set; }

        /// <summary>
        /// Gets or sets the picture mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the SEO friednly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the picture is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets the product pictures
        /// </summary>
        public virtual ICollection<ProductPicture> ProductPictures
        {
            get { return _productPictures ?? (_productPictures = new List<ProductPicture>()); }
            protected set { _productPictures = value; }
        }


        /// <summary>
        /// Gets or sets the product pictures
        /// </summary>
        public virtual ICollection<VendorPicture> VendorPictures
        {
            get { return _vendorPictures ?? (_vendorPictures = new List<VendorPicture>()); }
            protected set { _vendorPictures = value; }
        }

        /// <summary>
        /// Imagenes de fondo de los vendedores
        /// </summary>
        public virtual ICollection<Vendor> VendorBackgroudPictures
        {
            get { return _vendorBackgroundPictures ?? (_vendorBackgroundPictures = new List<Vendor>()); }
            protected set { _vendorBackgroundPictures = value; }
        }
    }
}
