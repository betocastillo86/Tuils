using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Web.Extensions.Api;
using Nop.Services.Common;
using Nop.Services.Logging;
using Nop.Core;
using Nop.Services.Media;
using Nop.Core.Domain.Media;
using Nop.Services.Localization;
using System.Threading.Tasks;
using Nop.Web.Infrastructure;
using Nop.Core.Domain.Common;
using Nop.Services.Vendors;
using Nop.Utilities;
using Nop.Services.Seo;
using Nop.Web.Framework.Mvc.Api;
using Nop.Core.Caching;
using Nop.Web.Infrastructure.Cache;

namespace Nop.Web.Controllers.Api
{
    [Route("api/addresses")]
    public class AddressesController : ApiController
    {
        #region Fields
        private readonly IAddressService _addressService;
        private readonly IWorkContext _workContext;
        private readonly ILogger _logger;
        private readonly IPictureService _pictureService;
        private readonly MediaSettings _mediaSettings;
        private readonly TuilsSettings _tuilsSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IVendorService _vendorService;
        private readonly ICacheManager _cacheManager;
        #endregion

        #region ctor
        public AddressesController(IAddressService addressService,
            IWorkContext workContext,
            ILogger logger,
            IPictureService pictureService,
            MediaSettings mediaSettings,
            ILocalizationService localizationService,
            TuilsSettings tuilsSettings,
            IVendorService vendorService,
            ICacheManager cacheManager)
        {
            this._addressService = addressService;
            this._logger = logger;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._mediaSettings = mediaSettings;
            this._localizationService = localizationService;
            this._tuilsSettings = tuilsSettings;
            this._vendorService = vendorService;
            this._cacheManager = cacheManager;
        }
        #endregion

        #region Methods

        [Route("api/addresses/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var model = _addressService.GetAddressById(id).ToModel();
                if (model.Id > 0)
                    return Ok(model);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
                return InternalServerError();
            }
        }

        #region Insert
        [AuthorizeApi]
        [Route("api/addresses")]
        [HttpPost]
        public IHttpActionResult Insert(AddressModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.VendorId == _workContext.CurrentVendor.Id)
                {
                    var address = model.ToEntity();
                    try
                    {
                        address.Active = true;
                        _addressService.InsertAddress(address);
                        model.Id = address.Id;
                        return Ok(model);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString(), e);
                        return InternalServerError(new NopException("No fue posible insertar la dirección, intenta de nuevo"));
                    }
                }
                else
                {
                    return BadRequest("No está autorizado para actualizar este dato");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Update
        [AuthorizeApi]
        [Route("api/addresses")]
        [HttpPut]
        public IHttpActionResult Update(AddressModel model)
        {
            if (ModelState.IsValid && model.Id > 0)
            {
                var address = _addressService.GetAddressById(model.Id);

                if (address != null &&
                    address.VendorId.HasValue &&
                    address.VendorId.Value == _workContext.CurrentVendor.Id)
                {
                    try
                    {

                        address.Id = model.Id;
                        address.Address1 = model.Address;
                        address.Email = model.Email;
                        address.PhoneNumber = model.PhoneNumber;
                        address.StateProvinceId = model.StateProvinceId;
                        address.Schedule = model.Schedule;
                        address.VendorId = model.VendorId;
                        address.FirstName = model.Name;
                        address.Latitude = model.Latitude;
                        address.Longitude = model.Longitude;
                        _addressService.UpdateAddress(address);
                        return Ok(model);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString(), e);
                        return InternalServerError(new NopException("No fue posible insertar la dirección, intenta de nuevo"));
                    }
                }
                else
                {
                    return BadRequest("No está autorizado para actualizar este dato");
                }
            }
            else
            {
                return BadRequest();
            }

        }

        #endregion

        #region Save Pictures

        [AuthorizeApi]
        [Route("api/addresses/{id}/pictures/{pictureId?}")]
        [HttpPost]
        public async Task<IHttpActionResult> SavePicture(int id, int? pictureId = null)
        {
            //Valida que venga un archivo
            if (id > 0 && 
                Request.Content.IsMimeMultipartContent() &&
                _workContext.CurrentVendor != null)
            {
                //Consulta la dirección y valida que el usuario auteticado pertenezca a esa sede
                var address = _addressService.GetAddressById(id);

                if (address != null &&
                   address.VendorId.HasValue &&
                    address.VendorId == _workContext.CurrentVendor.Id)
                {
                    try
                    {
                        var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());

                        System.Web.Mvc.FormCollection formData = provider.FormData;

                        IList<HttpContent> fileContentList = provider.Files;

                        var fileDataList = provider.GetFiles();

                        var files = await fileDataList;
                        var fileToUpload = files.FirstOrDefault();

                        //Valida que el tamaño del archivo sea valido
                        if (fileToUpload.Size < _tuilsSettings.maxFileUploadSize)
                        {
                            string extension = Files.GetExtensionByContentType(fileToUpload.ContentType);

                            string fileName = string.Empty;

                            //Si viene id de la foto actualiza los datos, sino inserta una nueva
                            if (pictureId.HasValue)
                            {
                                //Valida que esa direccion tenga asociada la imagen que se intenta modificar
                                if(_addressService.AddressHasPicture(id, pictureId.Value))
                                {
                                    _pictureService.UpdatePicture(pictureId.Value, fileToUpload.Data, _pictureService.GetContentTypeFromExtension(extension), address.Vendor.GetSeName(), true);
                                    return Ok(new { id = pictureId });
                                }
                                else
                                    return InternalServerError();
                            }
                            else
                            {
                                //Inserta la imagen y la asocia
                                pictureId = _addressService.InsertPicture(id, fileToUpload.Data, extension).PictureId;
                                if(pictureId.Value > 0)
                                    return Ok(new { id = pictureId });
                                else
                                    return InternalServerError();
                            }
                        }
                        else
                        {
                            return InternalServerError(new Nop.Core.NopException("El tamaño exede el permitido {0}", fileToUpload.Size));
                        }
                    }
                    catch (Exception e)
                    {
                        return InternalServerError(e);
                    }
                }
                else
                {
                    return Unauthorized();
                }

            }
            else
            {
                return NotFound();
            }

        }

        //[AuthorizeApiVendorAttribute]
        [AuthorizeApiAttribute]
        [Route("api/addresses/{id}/pictures/{pictureId}")]
        [HttpDelete]
        public IHttpActionResult DeletePicture(int id, int pictureId)
        {
            var address = _addressService.GetAddressById(id);

            //Si la dirección no existe o no tiene imagenes
            if (address == null || address.AddressPictures.Count == 0)
                return NotFound();

            //la sede debe pertenecer al usuario en sesión
            if (address.VendorId == _workContext.CurrentVendor.Id)
            {
                var picture = address.AddressPictures.FirstOrDefault(p => p.PictureId == pictureId);
                if(picture != null)
                {
                    _pictureService.DeletePicture(picture.Picture);
                    return Ok(id);
                }
                else
                    return NotFound();
            }
            else
                return Unauthorized();
        }
        #endregion

        #region Delete

        [AuthorizeApi]
        [Route("api/addresses/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (ModelState.IsValid && id > 0)
            {
                var address = _addressService.GetAddressById(id);

                if (address != null &&
                    address.VendorId.HasValue &&
                    address.VendorId.Value == _workContext.CurrentVendor.Id)
                {
                    try
                    {
                        _addressService.DeleteAddress(address);
                        return Ok(true);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString(), e);
                        return InternalServerError(new NopException("No fue posible insertar la dirección, intenta de nuevo"));
                    }
                }
                else
                {
                    return BadRequest("No está autorizado para actualizar este dato");
                }
            }
            else
            {
                return BadRequest();
            }

        }
        #endregion

        #region Filter Addresses
        [Route("api/vendors/addresses")]
        public IHttpActionResult Get([FromUri] SearchVendorsFilter filter)
        {
            var key = string.Format(ModelCacheEventConsumer.VENDOR_ADDRESS_SEARCH_KEY, filter.StateProvinceId, filter.SubTypeId, filter.CategoryId, filter.VendorId);



            var model = _cacheManager.Get(key, () => {
                //Realiza el filtro de las direcciones de acuerdo a los datos del filtro
                var addresses = _addressService.SearchVendorsAddresses(filter.StateProvinceId, filter.VendorId, filter.SubTypeId, filter.CategoryId);
                var modelList = new List<SearchVendorsAddress>();

                foreach (var item in addresses)
                {
                    if (!item.Longitude.HasValue || !item.Latitude.HasValue)
                        continue;

                    modelList.Add(new SearchVendorsAddress()
                    {
                        id = item.Id,
                        //name = item.FirstName,
                        address = item.Address1,
                        //phone = item.PhoneNumber,
                        lat = item.Latitude.Value,
                        lon = item.Longitude.Value,
                        vId = item.Vendor.Id,
                        vType = item.Vendor.VendorSubTypeId,
                        seName = item.Vendor.GetSeName(),
                        vName = item.Vendor.Name/*,
                        img = _pictureService.GetPictureUrl(item.Vendor.Picture,)*/
                    });
                }

                return modelList;
            });
            
            return Ok(model);
        }
        #endregion




        #region Get Pictures

        [HttpGet]
        [Route("api/addresses/{id}/pictures")]
        public IHttpActionResult GetPictures(int id)
        {
            if (id > 0)
            {
                int size = _mediaSettings.OfficeThumbPictureSizeOnControlPanel;
                return Ok(_addressService.GetPicturesByAddressId(id).ToModels(string.Empty, size, _pictureService, _localizationService));
            }
            else
                return NotFound();          
        }
        #endregion

        #endregion

    }
}