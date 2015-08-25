using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using System.Collections.Generic;

namespace Nop.Services.Vendors
{
    /// <summary>
    /// Vendor service interface
    /// </summary>
    public partial interface IVendorService
    {
        /// <summary>
        /// Gets a vendor by vendor identifier
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <returns>Vendor</returns>
        Vendor GetVendorById(int vendorId, bool includeImages = false);

        /// <summary>
        /// Delete a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        void DeleteVendor(Vendor vendor);

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <param name="name">Vendor name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        IPagedList<Vendor> GetAllVendors(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Inserts a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        void InsertVendor(Vendor vendor, bool loadDefaultCover = true, bool loadDefaultLogo = true);

        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        bool UpdateVendor(Vendor vendor);

        /// <summary>
        /// Actualiza unicamente los campos del header
        /// </summary>
        /// <param name="vendor">Nuevos datos del header</param>
        /// <returns></returns>
        bool UpdateVendorHeader(Vendor vendor);

        /// <summary>
        /// Retorna el vendor asociado a un cliente. 
        /// Si el cliente no tiene asociado ninguno se tiene la opción de crear uno por defecto basado en los datos del cliente
        /// </summary>
        /// <param name="createIfNotExists">True: Crea el vendor si el cliente no tiene ninguno asociado</param>
        /// <param name="customerId">Id de cliente que sirve como filtro</param>
        /// <returns>Vendor asociado al cliente ya sea creado o no</returns>
        Vendor GetVendorByCustomerId(int customerId, bool createIfNotExists = false);

        /// <summary>
        /// Permite cargar el archivo de un vendor
        /// </summary>
        /// <param name="dataFile">datos del archivo</param>
        /// <param name="extension">extensión del archivo</param>
        /// <param name="isMainPicture">True: es la imagen principal False: es el fondo</param>
        bool UpdatePicture(int vendorId, byte[] dataFile, string extension, bool isMainPicture);

        /// <summary>
        /// Actualiza la posición vertical en porcentaje de fondo del vendedor en el sitio
        /// </summary>
        /// <param name="vendorId">Id del vendedor</param>
        /// <param name="position">posición vertical del fondo</param>
        /// <returns></returns>
        bool UpdateBackgroundPosition(int vendorId, int position);

        /// <summary>
        /// Retorna las categorias especiales de un vendedor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        IList<SpecialCategoryVendor> GetSpecialCategoriesByVendorId(int vendorId);


        /// <summary>
        /// Retorna los reviews hechos a los productos del vendedor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        IList<ProductReview> GetReviewsByVendorId(int vendorId);

        bool InsertUpdateVendorSpecialCategories(int vendorId, IList<SpecialCategoryVendor> specialCategories);

        /// <summary>
        /// Actualiza los valores de AvgRating y NumRating del vendor dependiendo de los reviews recibidos
        /// </summary>
        /// <param name="vendorId">Vendedor a ser actualziado</param>
        void UpdateRatings(int vendorId);
    }
}