
using Nop.Core.Configuration;

namespace Nop.Core.Domain.Media
{
    public class MediaSettings : ISettings
    {
        public int AvatarPictureSize { get; set; }
        public int ProductThumbPictureSize { get; set; }
        public int ProductDetailsPictureSize { get; set; }
        public int ProductThumbPictureSizeOnProductDetailsPage { get; set; }
        /// <summary>
        /// A picture size of child associated product on the grouped product details page
        /// </summary>
        public int ProductThumbPerRowOnProductDetailsPage { get; set; }
        public int AssociatedProductPictureSize { get; set; }
        public int CategoryThumbPictureSize { get; set; }
        public int ManufacturerThumbPictureSize { get; set; }
        public int CartThumbPictureSize { get; set; }
        public int MiniCartThumbPictureSize { get; set; }
        public int AutoCompleteSearchThumbPictureSize { get; set; }

        public bool DefaultPictureZoomEnabled { get; set; }

        public int MaximumImageSize { get; set; }

        /// <summary>
        /// Geta or sets a default quality used for image generation
        /// </summary>
        public int DefaultImageQuality { get; set; }
        
        /// <summary>
        /// Geta or sets a vaue indicating whether single (/content/images/thumbs/) or multiple (/content/images/thumbs/001/ and /content/images/thumbs/002/) directories will used for picture thumbs
        /// </summary>
        public bool MultipleThumbDirectories { get; set; }

        public int VendorMainThumbPictureSize { get; set; }

        public int VendorBackgroundThumbPictureSize { get; set; }

        public int OfficeThumbPictureSizeOnControlPanel { get; set; }
        
        /// <summary>
        /// Tamaño maximo al que se deben redimensionar las imagenes que se van a subir. NO las que se muestran, solo las que se suben
        /// </summary>
        public int ProductImageMaxSizeResize { get; set; }

        /// <summary>
        /// Tamaño maximo al que se deben redimensionar las imagenes del cover que se van a subir. NO las que se muestran, solo las que se suben
        /// </summary>
        public int CoverImageMaxSizeResize { get; set; }

        /// <summary>
        /// Tamaño maximo al que se deben redimensionar las imagenes del LOGO que se van a subir. NO las que se muestran, solo las que se suben
        /// </summary>
        public int LogoImageMaxSizeResize { get; set; }
        
    }
}