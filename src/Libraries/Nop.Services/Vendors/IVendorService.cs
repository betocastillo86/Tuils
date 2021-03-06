using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using System;
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
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? showOnHomePage = null, bool? withPlan = null, VendorType? vendorType = null, string email = null,VendorOrderBy order = VendorOrderBy.Name);

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
        /// Si el cliente no tiene asociado ninguno se tiene la opci�n de crear uno por defecto basado en los datos del cliente
        /// </summary>
        /// <param name="createIfNotExists">True: Crea el vendor si el cliente no tiene ninguno asociado</param>
        /// <param name="customerId">Id de cliente que sirve como filtro</param>
        /// <returns>Vendor asociado al cliente ya sea creado o no</returns>
        Vendor GetVendorByCustomerId(int customerId, bool createIfNotExists = false);

        /// <summary>
        /// Permite cargar el archivo de un vendor
        /// </summary>
        /// <param name="dataFile">datos del archivo</param>
        /// <param name="extension">extensi�n del archivo</param>
        /// <param name="isMainPicture">True: es la imagen principal False: es el fondo</param>
        bool UpdatePicture(int vendorId, byte[] dataFile, string extension, bool isMainPicture);

        /// <summary>
        /// Actualiza la posici�n vertical en porcentaje de fondo del vendedor en el sitio
        /// </summary>
        /// <param name="vendorId">Id del vendedor</param>
        /// <param name="position">posici�n vertical del fondo</param>
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
        IList<IReview> GetReviewsByVendorId(int vendorId);

        bool InsertUpdateVendorSpecialCategories(int vendorId, IList<SpecialCategoryVendor> specialCategories);

        /// <summary>
        /// Actualiza los valores de AvgRating y NumRating del vendor dependiendo de los reviews recibidos
        /// </summary>
        /// <param name="vendorId">Vendedor a ser actualziado</param>
        void UpdateRatingsTotal(int vendorId);

        /// <summary>
        /// Actualiza el plan del vendor de acuerdo a la orden que se est� comprando
        /// Unicamente recibe la orden ya que con ese dato se puede saber qui�n es el vendor
        /// </summary>
        /// <param name="order">Orden de la que se desea agregar al plan</param>
        Vendor AddPlanToVendor(Order order);


        #region Vendor reviews

        /// <summary>
        /// Gets all vendor reviews
        /// </summary>
        /// <param name="customerId">Customer identifier; 0 to load all records</param>
        /// <param name="approved">A value indicating whether to content is approved; null to load all records</param> 
        /// <param name="fromUtc">Item creation from; null to load all records</param>
        /// <param name="toUtc">Item item creation to; null to load all records</param>
        /// <param name="message">Search title or review text; null to load all records</param>
        /// <param name="orderItemId">Filtra por orden a las calificaciones de un producto</param>
        /// <returns>Reviews</returns>
        IList<VendorReview> GetAllVendorReviews(int? customerId = null, bool? approved = null,
            DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = null, int? vendorId = null);

        /// <summary>
        /// Gets product review
        /// </summary>
        /// <param name="vendorReviewId">Product review identifier</param>
        /// <returns>Product review</returns>
        VendorReview GetVendorReviewById(int vendorReviewId);

        /// <summary>
        /// Deletes a product review
        /// </summary>
        /// <param name="vendorReview">Product review</param>
        void DeleteVendorReview(VendorReview vendorReview);


        /// <summary>
        /// Valida si el usuario ya hizo review a un vendor
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        bool CustomerHasVendorReview(int customerId, int vendorId);
        #endregion

        
    }
}