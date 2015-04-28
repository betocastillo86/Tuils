using Nop.Core;
using Nop.Core.Domain.Vendors;

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
        void InsertVendor(Vendor vendor);

        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        void UpdateVendor(Vendor vendor);

        /// <summary>
        /// Retorna el vendor asociado a un cliente. 
        /// Si el cliente no tiene asociado ninguno se tiene la opción de crear uno por defecto basado en los datos del cliente
        /// </summary>
        /// <param name="createIfNotExists">True: Crea el vendor si el cliente no tiene ninguno asociado</param>
        /// <param name="customerId">Id de cliente que sirve como filtro</param>
        /// <returns>Vendor asociado al cliente ya sea creado o no</returns>
        Vendor GetVendorByCustomerId(int customerId, bool createIfNotExists = false);
    }
}