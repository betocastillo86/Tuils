using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Web.Framework.Mvc.Api;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Services.Customers;

namespace Nop.Web.Controllers.Api
{
    [Route("api/orders")]
    public class OrdersController : ApiController
    {
        #region Props
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreContext _storeContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IShoppingCartService _shoppingCartService;
        #endregion

        #region Ctor
        public OrdersController(IOrderService orderService, IWorkContext workContext,
            ILocalizationService localizationService,
            IStoreContext storeContext,
            IOrderProcessingService orderProcessingService,
            IGenericAttributeService genericAttributeService,
            IShoppingCartService shoppingCartService,
            IProductService productService)
        {
            this._orderService = orderService;
            this._workContext = workContext;
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._orderProcessingService = orderProcessingService;
            this._genericAttributeService = genericAttributeService;
            this._shoppingCartService = shoppingCartService;
            this._productService = productService;
        }
        #endregion
        #region Insert
        /// <summary>
        /// Crea una nueva orden después de una solicitud de un usuario de consultar el producto
        /// </summary>
        /// <returns></returns>
        //Se elimina por ahora la autorización ya que se va permitir ver el teléfono a los usuarios que no están registrados
        //[AuthorizeApi]
        ////[HttpPost]
        ////[Route("api/orders")]
        ////public IHttpActionResult Insert(OrderModel model)
        ////{
            
        ////    if(model.ProductId == 0)
        ////        return BadRequest();

        ////    //Consulta y valida que el producto exista
        ////    var product = _productService.GetProductById(model.ProductId);
        ////    if(product == null)
        ////        return NotFound();

        ////    //Si el usuario autenticado es el mismo vendedor del producto
        ////    //no suma el ver numero
        ////    if (_workContext.CurrentVendor != null && _workContext.CurrentVendor.Id == product.VendorId)
        ////    {
        ////        return Ok(new { sameUser = true });
        ////    }
        ////    else
        ////    {
        ////        product.NumClicksForMoreInfo++;
        ////        _productService.UpdateProduct(product);
        ////        return Ok(model);
        ////    }
        ////}



        #endregion
        
    }
}
