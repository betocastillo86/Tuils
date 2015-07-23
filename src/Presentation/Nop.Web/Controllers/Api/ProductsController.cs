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
        #endregion

        #region Ctor
        public ProductsController(IProductService productService, 
            IWorkContext workContext,
            IVendorService vendorService,
            ICategoryService categoryService,
            CatalogSettings catalogSettings)
        {
            this._productService = productService;
            this._workContext = workContext;
            this._vendorService = vendorService;
            this._categoryService = categoryService;
            this._catalogSettings = catalogSettings;
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
                if (model.SpecialCategories.Where(s => s.SpecialType == SpecialCategoryProductType.BikeBrand).Count() > _catalogSettings.LimitOfSpecialCategories)
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