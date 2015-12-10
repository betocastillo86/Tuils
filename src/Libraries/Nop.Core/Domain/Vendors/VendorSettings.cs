using Nop.Core.Configuration;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Vendor settings
    /// </summary>
    public class VendorSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the default value to use for Vendor page size options (for new vendors)
        /// </summary>
        public string DefaultVendorPageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets the value indicating how many vendors to display in vendors block
        /// </summary>
        public int VendorsBlockItemsToDisplay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display vendor name on the product details page
        /// </summary>
        public bool ShowVendorOnProductDetailsPage { get; set; }

        /// <summary>
        /// Tamaño por defecto de los resultados por pagina de los vendedores
        /// </summary>
        public int DefaultPageSize { get; set; }

        /// <summary>
        /// Tamaño por defecto de los resultados por pagina de los vendedores
        /// </summary>
        public int DefaultReviewsPageSize { get; set; }

        /// <summary>
        /// Dias para programar primer correo de actualizacion de tienda
        /// </summary>
        public int DaysUpdateShopFirstEmail { get; set; }

        /// <summary>
        /// Dias para programar segundo correo de actualizacion de tienda
        /// </summary>
        public int DaysUpdateShopSecondEmail { get; set; }

        /// <summary>
        /// Ancho minimo para cargar una imagen de cover
        /// </summary>
        public int MinWidthCover { get; set; }

        /// <summary>
        /// Alto  minimo para cargar una imagen de cover
        /// </summary>
        public int MinHeightCover { get; set; }

        public int DefaultPicture1 { get; set; }
        public int DefaultPicture2 { get; set; }
        public int DefaultPicture3 { get; set; }
        public int DefaultPicture4 { get; set; }
        public int DefaultPicture5 { get; set; }
        public int DefaultPicture6 { get; set; }
        public int DefaultPicture7 { get; set; }
        public int DefaultPicture8 { get; set; }

        public int GetRandomCover()
        {
            int i = new System.Random().Next(1,8);
            return (int) this.GetType().GetProperty("DefaultPicture" +i).GetValue(this);
        }

    }
}
