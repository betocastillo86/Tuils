using Nop.Services.Common;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Web.Extensions.Api;
using Nop.Core;
using Nop.Services.Vendors;
using Nop.Services.Logging;
using System.Threading.Tasks;
using Nop.Web.Infrastructure;
using Nop.Core.Domain.Common;
using Nop.Utilities;
using Nop.Core.Domain.Vendors;
using Nop.Web.Framework.Mvc.Api;

namespace Nop.Web.Controllers.Api
{
    [Route("api/vendors")]
    public class VendorsController : ApiController
    {
        #region Fields
        private readonly IAddressService _addressService;
        private readonly IWorkContext _workContext;
        private readonly IVendorService _vendorService;
        private readonly ILogger _logger;
        private readonly TuilsSettings _tuilsSettings;
        private readonly VendorSettings _vendorSettings;
        #endregion

        #region ctor
        public VendorsController(IAddressService addressService,
            IWorkContext workContext,
            IVendorService vendorService,
            ILogger logger,
            TuilsSettings tuilsSettings,
            VendorSettings vendorSettings)
        {
            this._addressService = addressService;
            this._workContext = workContext;
            this._vendorService = vendorService;
            this._logger = logger;
            this._tuilsSettings = tuilsSettings;
            this._vendorSettings = vendorSettings;
        }
        #endregion


        [Route("api/vendors/{id}/offices")]
        public IHttpActionResult GetOffices(int id)
        {
            return Ok(_addressService.GetAddressesByVendorId(id).ToModels());
        }
#region Header
          [AuthorizeApi]
        [HttpPut]
        [Route("api/vendors/header")]
        public IHttpActionResult UpdateVendorHeader(VendorModel model)
        {
            if (model.Id > 0 && model.Id == _workContext.CurrentVendor.Id)
            {

                if (model.IsHeaderValid())
                {
                    try
                    {
                        return Ok(_vendorService.UpdateVendorHeader(model.ToEntity()));
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString(), e);
                        return InternalServerError();
                    }
                }
                else
                    return BadRequest();
            }
            else
                return NotFound();
        }

        [AuthorizeApi]
        [HttpPut]
        [Route("api/vendors/{id}/header/background")]
        public IHttpActionResult UpdateBackgroundPosition(int id, VendorModel model)
        {
            if (id > 0 && id == _workContext.CurrentVendor.Id)
            {
                if (model.BackgroundPosition.HasValue && model.BackgroundPosition.Value >= 0)
                {
                    try
                    {
                        return Ok(_vendorService.UpdateBackgroundPosition(id, model.BackgroundPosition.Value));
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString(), e);
                        return InternalServerError();
                    }
                }
                else
                    return BadRequest();
            }
            else
                return NotFound();
        }

	#endregion
       
        #region Files

        [AuthorizeApi]
        [Route("api/vendors/{id}/{type:regex(backgroundPicture|picture)}", Order = 2)]
        [HttpPost]
        public async Task<IHttpActionResult> SaveBackground(int id, string type)
        {
            if (id > 0 &&
                _workContext.CurrentVendor != null &&
                _workContext.CurrentVendor.Id == id &&
                Request.Content.IsMimeMultipartContent())
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
                        
                        if (_vendorService.UpdatePicture(id, fileToUpload.Data, extension, type.Equals("picture")))
                            return Ok(new { id = id });
                        else
                            return InternalServerError();

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
                return NotFound();
            }

        }
        #endregion

        #region Reviewss

        #endregion
    }
}
