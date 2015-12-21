using System;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Services.Directory;
using Nop.Services.Events;
using System.Collections.Generic;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Media;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Services.Logging;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Common
{
    /// <summary>
    /// Address service
    /// </summary>
    public partial class AddressService : IAddressService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : address ID
        /// </remarks>
        private const string ADDRESSES_BY_ID_KEY = "Nop.address.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ADDRESSES_PATTERN_KEY = "Nop.address.";

        #endregion

        #region Fields

        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<SpecialCategoryVendor> _specialCategoryVendorRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<AddressPicture> _addressPictureRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly ICountryService _countryService;
        private readonly IPictureService _pictureService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ILogger _logger;
        
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IEventPublisher _eventPublisher;
        private readonly AddressSettings _addressSettings;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="addressRepository">Address repository</param>
        /// <param name="countryService">Country service</param>
        /// <param name="stateProvinceService">State/province service</param>
        /// <param name="addressAttributeService">Address attribute service</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="addressSettings">Address settings</param>
        public AddressService(ICacheManager cacheManager,
            IRepository<Address> addressRepository,
            ICountryService countryService, 
            IStateProvinceService stateProvinceService,
            IAddressAttributeService addressAttributeService,
            IEventPublisher eventPublisher, 
            AddressSettings addressSettings,
            IRepository<StateProvince> stateProvinceRepository,
            IRepository<AddressPicture> addressPictureRepository,
            IRepository<Picture> pictureRepository,
            IPictureService pictureService,
            ILogger logger,
            IRepository<SpecialCategoryVendor> specialCategoryVendorRepository)
        {
            this._cacheManager = cacheManager;
            this._addressRepository = addressRepository;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._addressAttributeService = addressAttributeService;
            this._eventPublisher = eventPublisher;
            this._addressSettings = addressSettings;
            this._stateProvinceRepository = stateProvinceRepository;
            this._addressPictureRepository = addressPictureRepository;
            this._pictureRepository = pictureRepository;
            this._pictureService = pictureService;
            this._logger = logger;
            this._specialCategoryVendorRepository = specialCategoryVendorRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void DeleteAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            _addressRepository.Delete(address);

            //cache
            _cacheManager.RemoveByPattern(ADDRESSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(address);
        }

        /// <summary>
        /// Gets total number of addresses by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <returns>Number of addresses</returns>
        public virtual int GetAddressTotalByCountryId(int countryId)
        {
            if (countryId == 0)
                return 0;

            var query = from a in _addressRepository.Table
                        where a.CountryId == countryId
                        select a;
            return query.Count();
        }

        /// <summary>
        /// Gets total number of addresses by state/province identifier
        /// </summary>
        /// <param name="stateProvinceId">State/province identifier</param>
        /// <returns>Number of addresses</returns>
        public virtual int GetAddressTotalByStateProvinceId(int stateProvinceId)
        {
            if (stateProvinceId == 0)
                return 0;

            var query = from a in _addressRepository.Table
                        where a.StateProvinceId == stateProvinceId
                        select a;
            return query.Count();
        }

        /// <summary>
        /// Gets an address by address identifier
        /// </summary>
        /// <param name="addressId">Address identifier</param>
        /// <returns>Address</returns>
        public virtual Address GetAddressById(int addressId)
        {
            if (addressId == 0)
                return null;

            string key = string.Format(ADDRESSES_BY_ID_KEY, addressId);
            return _cacheManager.Get(key, () => _addressRepository.GetById(addressId));
        }

        /// <summary>
        /// Inserts an address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void InsertAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            
            address.CreatedOnUtc = DateTime.UtcNow;

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _addressRepository.Insert(address);

            //cache
            _cacheManager.RemoveByPattern(ADDRESSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(address);
        }

        /// <summary>
        /// Updates the address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual bool UpdateAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            int modified = _addressRepository.Update(address) ;

            //cache
            _cacheManager.RemoveByPattern(ADDRESSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(address);

            return modified > 0;
        }

        
        /// <summary>
        /// Gets a value indicating whether address is valid (can be saved)
        /// </summary>
        /// <param name="address">Address to validate</param>
        /// <returns>Result</returns>
        public virtual bool IsAddressValid(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (String.IsNullOrWhiteSpace(address.FirstName))
                return false;

            if (String.IsNullOrWhiteSpace(address.LastName))
                return false;

            if (String.IsNullOrWhiteSpace(address.Email))
                return false;

            if (_addressSettings.CompanyEnabled &&
                _addressSettings.CompanyRequired &&
                String.IsNullOrWhiteSpace(address.Company))
                return false;

            if (_addressSettings.StreetAddressEnabled &&
                _addressSettings.StreetAddressRequired &&
                String.IsNullOrWhiteSpace(address.Address1))
                return false;

            if (_addressSettings.StreetAddress2Enabled &&
                _addressSettings.StreetAddress2Required &&
                String.IsNullOrWhiteSpace(address.Address2))
                return false;

            if (_addressSettings.ZipPostalCodeEnabled &&
                _addressSettings.ZipPostalCodeRequired &&
                String.IsNullOrWhiteSpace(address.ZipPostalCode))
                return false;


            if (_addressSettings.CountryEnabled)
            {
                if (address.CountryId == null || address.CountryId.Value == 0)
                    return false;

                var country = _countryService.GetCountryById(address.CountryId.Value);
                if (country == null)
                    return false;

                if (_addressSettings.StateProvinceEnabled)
                {
                    var states = _stateProvinceService.GetStateProvincesByCountryId(country.Id);
                    if (states.Count > 0)
                    {
                        if (address.StateProvinceId == null || address.StateProvinceId.Value == 0)
                            return false;

                        var state = states.FirstOrDefault(x => x.Id == address.StateProvinceId.Value);
                        if (state == null)
                            return false;
                    }
                }
            }

            if (_addressSettings.CityEnabled &&
                _addressSettings.CityRequired &&
                String.IsNullOrWhiteSpace(address.City))
                return false;

            if (_addressSettings.PhoneEnabled &&
                _addressSettings.PhoneRequired &&
                String.IsNullOrWhiteSpace(address.PhoneNumber))
                return false;

            if (_addressSettings.FaxEnabled &&
                _addressSettings.FaxRequired &&
                String.IsNullOrWhiteSpace(address.FaxNumber))
                return false;

            var attributes = _addressAttributeService.GetAllAddressAttributes();
            if (attributes.Any(x => x.IsRequired))
                return false;

            return true;
        }

        /// <summary>
        /// Consulta las direcciones de un vendedor especifico
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public IList<Address> GetAddressesByVendorId(int vendorId)
        {
            if (vendorId <= 0)
                return new List<Address>();

            var query = from a in _addressRepository.Table
                        where a.VendorId == vendorId
                        join v in _stateProvinceRepository.Table on a.StateProvinceId equals v.Id
                        select a;


            return query.ToList();
        }
        
        #endregion


        #region Pictures
        /// <summary>
        /// Retorna todas las fotos de una dirección
        /// </summary>
        /// <param name="addressId">id de la direccion</param>
        /// <returns></returns>
        public IList<Picture> GetPicturesByAddressId(int addressId)
        {
            if(addressId == 0)
                return new List<Picture>();
            var query = from a in _addressPictureRepository.Table
                        where a.AddressId == addressId
                        join p in _pictureRepository.Table on a.PictureId equals p.Id
                        select p;

            return query.ToList();
        }

        /// <summary>
        /// Asocia una nueva imagen a una dirección
        /// </summary>
        /// <param name="addressId">id de la direccion</param>
        /// <param name="file">contenido del archivo</param>
        /// <param name="extension">extension del archivo</param>
        /// <param name="seoName">Nombre con el que se va guargar la imagen. Es opcional, sino se tiene toma el nombre del vendor</param>
        /// <returns>Retorna la relación entre la dirección y la imagen</returns>
        public AddressPicture InsertPicture(int addressId, byte[] file, string extension, string seoName = null)
        {
            
            try
            {
               
                string mimeType = _pictureService.GetContentTypeFromExtension(extension);
               //Si el seo viene nulo lo iguala al seoName del vendor 
                if (seoName == null)
                {
                    var address = GetAddressById(addressId);
                    seoName = address.Vendor.GetSeName();
                }

                //inserta en la tabla de imagenes
                var picture = _pictureService.InsertPicture(file, mimeType, seoName, true);

                //inserta la relacion
                var addressPicture = new AddressPicture()
                {
                    AddressId = addressId,
                    PictureId = picture.Id,
                    DisplayOrder = 0
                };
                _addressPictureRepository.Insert(addressPicture);

                return addressPicture;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
                return new AddressPicture();
            }
        }

        /// <summary>
        /// Valida si una imagen está asociada a la dirección
        /// </summary>
        /// <param name="addressId">direccion</param>
        /// <param name="pictureId">id de la imagen a evaluar</param>
        /// <returns>True: La imagen estpá asociada False: La imagen no está asociada</returns>
        public bool AddressHasPicture(int addressId, int pictureId) {
            return _addressPictureRepository.Table.FirstOrDefault(a => a.PictureId == pictureId && a.AddressId == addressId) != null;
        }

        #endregion



        #region Search
        /// <summary>
        /// Filtra todas las sedes de los vendors de acuerdo a los filtros entregados
        /// </summary>
        /// <param name="stateProvinceId">filtro obligatorio en el que se buscan las sedes de una ciudad especifica</param>
        /// <param name="vendorId">Id del vendor especifico del que se quieren traer las direcciones</param>
        /// <param name="subVendorType">sub tipo de vendedor por el que se quieren traer las direcciones</param>
        /// <param name="categoryId">Categorias especiales de los vendors existentes por la que se quiere filtrar</param>
        /// <returns>lista de sedes</returns>
        public IList<Address> SearchVendorsAddresses(int stateProvinceId, int? vendorId = null, int? subVendorType = null, int? categoryId = null)
        {
            var query = _addressRepository.Table.Where(a => 
                a.Active &&
                !a.Deleted &&
                a.StateProvinceId == stateProvinceId);

            //El filtro del vendedor anula los demás filtros
            if (vendorId.HasValue)
            {
                query = query.Where(a => a.VendorId == vendorId.Value);
            }
            else
            { 
                //Si no viene filtrado por vendor, valida que la dirección sea de un vendor
                query = query.Where(a => a.VendorId.HasValue);

                if (subVendorType.HasValue)
                    query = query.Where(a => a.Vendor.VendorSubTypeId == subVendorType.Value);

                if (categoryId.HasValue)
                {
                    //Consulta los vendors que venden esa categoria en especifico
                    var vendorsWithCategories = _specialCategoryVendorRepository.Table.Where(v => v.CategoryId == categoryId)
                        .Select(v => v.VendorId);

                    query = query.Where(a => vendorsWithCategories.Contains(a.VendorId.Value));
                }
            }

            return query.ToList();
        }
        #endregion

        
    }
}