using Nop.Services.Common;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Web.Extensions.Api;

namespace Nop.Web.Controllers.Api
{
    [Route("api/vendors")]
    public class VendorsController : ApiController
    {
        #region Fields
        private readonly IAddressService _addressService;
        #endregion

        #region ctor
        public VendorsController(IAddressService addressService)
        {
            this._addressService = addressService;
        }
        #endregion


        [Route("api/vendors/{id}/offices")]
        public List<AddressModel> GetOffices(int id)
        {
            return _addressService.GetAddressesByVendorId(id).ToModels();
        }
    }
}
