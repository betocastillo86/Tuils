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
        [HttpPost]
        [Route("api/orders")]
        public IHttpActionResult Insert(OrderModel model)
        {
            
            if(model.ProductId == 0)
                return BadRequest();

            //Consulta y valida que el producto exista
            var product = _productService.GetProductById(model.ProductId);
            if(product == null)
                return NotFound();

            //Si no hay sesión, se suma a los clicks del producto pero no se crea ninguna orden
            if (_workContext.CurrentCustomer.IsGuest())
            {
                product.NumClicksForMoreInfo++;
                _productService.UpdateProduct(product);
                return Ok(model);
            }
            else
            {
                //Si el usuario ya compró el producto omite las validaciones y devuelve un true
                if (_orderService.CustomerBoughtProduct(_workContext.CurrentCustomer.Id, model.ProductId))
                    return Ok(new { id = model.ProductId });


                var processPaymentRequest = new Nop.Services.Payments.ProcessPaymentRequest();

                //prevent 2 orders being placed within an X seconds time frame
                //Se elimina validacion ya que el anterior cumple con la condicion if (_orderService.CustomerBoughtProduct(_workContext.CurrentCustomer.Id, model.ProductId))
                //if (!_orderService.IsMinimumOrderPlacementIntervalValid(_workContext.CurrentCustomer))
                //    throw new Exception(_localizationService.GetResource("Checkout.MinOrderPlacementInterval"));

                //place order
                processPaymentRequest.StoreId = _storeContext.CurrentStore.Id;
                processPaymentRequest.CustomerId = _workContext.CurrentCustomer.Id;
                processPaymentRequest.PaymentMethodSystemName = _workContext.CurrentCustomer.GetAttribute<string>(
                    SystemCustomerAttributeNames.SelectedPaymentMethod,
                    _genericAttributeService, _storeContext.CurrentStore.Id);

                //intenta agregar los datos al carro de compras
                var addCartResult = _shoppingCartService.AddToCart(customer: _workContext.CurrentCustomer,
                    product: product,
                    shoppingCartType: ShoppingCartType.ShoppingCart,
                    storeId: _storeContext.CurrentStore.Id,
                    quantity: 1,
                    validateCartDisabled: false);

                if (addCartResult.Count == 0)
                {
                    //Intenta registrar la orden como paga
                    var placeOrderResult = _orderProcessingService.PlaceOrder(processPaymentRequest);
                    if (placeOrderResult.Errors.Count == 0)
                    {
                        var order = placeOrderResult.PlacedOrder;
                        return Ok(new { id = order.Id });
                    }
                    else
                    {
                        return InternalServerError(new NopException(addCartResult.FirstOrDefault()));
                    }
                }
                else
                {
                    return InternalServerError(new NopException(addCartResult.FirstOrDefault()));
                }
            }

        }



        #endregion
        
    }
}
