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

namespace Nop.Web.Controllers.Api
{
    [Route("api/addresses")]
    public class AddressesController : ApiController
    {
        #region Fields
        private readonly IAddressService _addressService;
        private readonly IWorkContext _workContext;
        private readonly ILogger _logger;
        #endregion

        #region ctor
        public AddressesController(IAddressService addressService,
            IWorkContext workContext,
            ILogger logger)
        {
            this._addressService = addressService;
            this._logger = logger;
            this._workContext = workContext;
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


        [Authorize]
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

        [Authorize]
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


        [Authorize]
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

    }
}