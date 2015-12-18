

using Nop.Core.Domain.Common;
using Nop.Core.Domain.Media;
using System.Collections.Generic;

namespace Nop.Services.Common
{
    /// <summary>
    /// Address service interface
    /// </summary>
    public partial interface IAddressService
    {
        /// <summary>
        /// Deletes an address
        /// </summary>
        /// <param name="address">Address</param>
        void DeleteAddress(Address address);

        /// <summary>
        /// Gets total number of addresses by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <returns>Number of addresses</returns>
        int GetAddressTotalByCountryId(int countryId);

        /// <summary>
        /// Gets total number of addresses by state/province identifier
        /// </summary>
        /// <param name="stateProvinceId">State/province identifier</param>
        /// <returns>Number of addresses</returns>
        int GetAddressTotalByStateProvinceId(int stateProvinceId);

        /// <summary>
        /// Gets an address by address identifier
        /// </summary>
        /// <param name="addressId">Address identifier</param>
        /// <returns>Address</returns>
        Address GetAddressById(int addressId);

        /// <summary>
        /// Inserts an address
        /// </summary>
        /// <param name="address">Address</param>
        void InsertAddress(Address address);

        /// <summary>
        /// Updates the address
        /// </summary>
        /// <param name="address">Address</param>
        bool UpdateAddress(Address address);

        /// <summary>
        /// Gets a value indicating whether address is valid (can be saved)
        /// </summary>
        /// <param name="address">Address to validate</param>
        /// <returns>Result</returns>
        bool IsAddressValid(Address address);


        IList<Address> GetAddressesByVendorId(int vendorId);

        /// <summary>
        /// Filtra todas las sedes de los vendors de acuerdo a los filtros entregados
        /// </summary>
        /// <param name="stateProvinceId">filtro obligatorio en el que se buscan las sedes de una ciudad especifica</param>
        /// <param name="vendorId">Id del vendor especifico del que se quieren traer las direcciones</param>
        /// <param name="subVendorType">sub tipo de vendedor por el que se quieren traer las direcciones</param>
        /// <param name="categoryId">Categorias especiales de los vendors existentes por la que se quiere filtrar</param>
        /// <returns>lista de sedes</returns>
        IList<Address> SearchVendorsAddresses(int stateProvinceId, int? vendorId = null, int? subVendorType= null, int? categoryId= null);

        IList<Picture> GetPicturesByAddressId(int addressId);

        AddressPicture InsertPicture(int addressId, byte[] file, string extension, string seoName = null);

        /// <summary>
        /// Valida si una imagen está asociada a la dirección
        /// </summary>
        /// <param name="addressId">direccion</param>
        /// <param name="pictureId">id de la imagen a evaluar</param>
        /// <returns>True: La imagen estpá asociada False: La imagen no está asociada</returns>
        bool AddressHasPicture(int addressId, int pictureId);
    }
}