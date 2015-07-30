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
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Services.Common;
using Nop.Core.Domain.Customers;


namespace Nop.Web.Controllers.Api
{
    [Route("api/products")]
    public class ProductsController : ApiController
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        private readonly IVendorService _vendorService;
        private readonly ICategoryService _categoryService;
        private readonly CatalogSettings _catalogSettings;
        private readonly TuilsSettings _tuilsSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        #endregion

        #region Ctor
        public ProductsController(IProductService productService, 
            IWorkContext workContext,
            IVendorService vendorService,
            ICategoryService categoryService,
            CatalogSettings catalogSettings,
            TuilsSettings tuilsSettings,
            IGenericAttributeService genericAttributeService)
        {
            this._productService = productService;
            this._workContext = workContext;
            this._vendorService = vendorService;
            this._categoryService = categoryService;
            this._catalogSettings = catalogSettings;
            this._tuilsSettings = tuilsSettings;
            this._genericAttributeService = genericAttributeService;
        }
        #endregion
        [Route("api/products")]
        [HttpPost]
        [AuthorizeApi]
        public IHttpActionResult PublishProduct(ProductBaseModel model)
        {
            if (ModelState.IsValid && model.Validate())
            {
                //Si llegan más valores de los posibles para categorias especiales, toma solo los permitidos
                if (model.SpecialCategories != null && model.SpecialCategories.Where(s => s.SpecialType == SpecialCategoryProductType.BikeBrand).Count() > _catalogSettings.LimitOfSpecialCategories)
                    model.SpecialCategories = model.SpecialCategories.Take(_catalogSettings.LimitOfSpecialCategories).ToList();

                
                var product = model.ToEntity(_categoryService);

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
                
                //Guarda el número telefónico de contacto
                _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer, SystemCustomerAttributeNames.BikeCarriagePlate, model.PhoneNumber);
                _workContext.CurrentVendor.PhoneNumber = model.PhoneNumber;
                _vendorService.UpdateVendor(_workContext.CurrentVendor);

                try
                {
                    //Crea el producto en un estado inactivo 
                    _productService.PublishProduct(product);
                    return Ok(new { Id = product.Id });
                }
                catch (NopException e)
                {
                    ModelState.AddModelError("ErrorCode", Convert.ToInt32(e.Code).ToString());
                    ModelState.AddModelError("ErrorMessage", e.Message);
                    return BadRequest(ModelState);
                }
                
               
            }
            else
            {
                return Conflict();
            }
        }
    }
}