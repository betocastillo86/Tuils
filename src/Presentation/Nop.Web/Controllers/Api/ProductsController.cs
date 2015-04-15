using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Web.Extensions.Api;
using Nop.Services.Catalog;
using Nop.Core;
using Nop.Services.Vendors;
using Nop.Web.Framework.Mvc.Api;


namespace Nop.Web.Controllers.Api
{
    [Route("api/products")]
    public class ProductsController : ApiController
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        private readonly IVendorService _vendorService;
        #endregion

        #region Ctor
        public ProductsController(IProductService productService, 
            IWorkContext workContext,
            IVendorService vendorService)
        {
            this._productService = productService;
            this._workContext = workContext;
            this._vendorService = vendorService;
        }
        #endregion
        
        [Route("api/products")]
        [Authorize]
        [HttpGet]
        public IHttpActionResult PublisProduct()
        {
            return Ok();
        }

        [Route("api/products")]
        [HttpPost]
        [AuthorizeApi]
        public IHttpActionResult PublisProduct(ProductBaseModel model)
        {
            if (ModelState.IsValid)
            {
                var product = model.ToEntity();

                //Si el vendor no existe, es necesario crearlo con base en el usuario
                if (_workContext.CurrentVendor == null || _workContext.CurrentVendor.Id == 0)
                {
                    //Consulta el vendor y lo crea si es necesario
                    var vendor = _vendorService.GetVendorByCustomerId(_workContext.CurrentCustomer.Id, true);
                    product.VendorId = vendor.Id;
                }
                else
                {
                    product.VendorId = _workContext.CurrentVendor.Id;
                }

                //Crea el producto en un estado inactivo 
                _productService.PublishProduct(product);
                return Ok(new { Id = product.Id });
            }
            else
            {
                return Conflict();
            }
        }
    }
}