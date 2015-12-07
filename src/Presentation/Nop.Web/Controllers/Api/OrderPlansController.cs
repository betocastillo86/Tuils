using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Plugins;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Web.Framework.Mvc.Api;
using Nop.Web.Models.Api;
using Nop.Web.Models.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nop.Web.Controllers.Api
{
    [Route("api/orderplans")]
    public class OrderPlansController : ApiController
    {

        public readonly IOrderService _orderService;
        public readonly IWorkContext _workContext;
        public readonly ILocalizationService _localizationService;
        public readonly IStoreContext _storeContext;
        public readonly IGenericAttributeService _genericAttributeService;
        public readonly IShoppingCartService _shoppingCartService;
        public readonly IOrderProcessingService _orderProcessingService;
        public readonly IProductService _productService;
        public readonly ICustomerActivityService _customerActivityService;
        public readonly ICustomerService _customerService;
        public readonly ICountryService _countryService;
        public readonly IOrderTotalCalculationService _orderTotalCalculationService;
        public readonly ILogger _logger;
        public readonly IPaymentService _paymentService;
        public readonly IPluginFinder _pluginFinder;
        public readonly PaymentSettings _paymentSettings;
        public readonly TuilsSettings _tuilsSettings;
        public readonly AddressSettings _addressSettings;
        public readonly OrderSettings _orderSettings;




        public OrderPlansController(IOrderService orderService,
            IWorkContext workContext,
            ILocalizationService localizationService,
            IStoreContext storeContext,
            IGenericAttributeService genericAttributeService,
            IShoppingCartService shoppingCartService,
            IOrderProcessingService orderProcessingService,
            ICustomerActivityService customerActivityService,
            ICustomerService customerService,
            TuilsSettings tuilsSettings,
            ICountryService countryService,
            ILogger logger,
            IProductService productService,
            IOrderTotalCalculationService orderTotalCalculationService,
            AddressSettings addressSettings,
            IPaymentService paymentService,
            PaymentSettings paymentSettings,
            IPluginFinder pluginFinder,
            OrderSettings orderSettings)
        {
            this._orderService = orderService;
            this._workContext = workContext;
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._genericAttributeService = genericAttributeService;
            this._shoppingCartService = shoppingCartService;
            this._orderProcessingService = orderProcessingService;
            this._customerActivityService = customerActivityService;
            this._customerService = customerService;
            this._tuilsSettings = tuilsSettings;
            this._countryService = countryService;
            this._logger = logger;
            this._productService = productService;
            this._orderTotalCalculationService = orderTotalCalculationService;
            this._addressSettings = addressSettings;
            this._paymentService = paymentService;
            this._paymentSettings = paymentSettings;
            this._pluginFinder = pluginFinder;
            this._orderSettings = orderSettings;
        }

        /// <summary>
        /// Crea
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeApi]
        public IHttpActionResult Insert(OrderPlanModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedPlan = _productService.GetProductById(model.PlanId);

                if (selectedPlan == null)
                    return BadRequest("El plan seleccionado no existe");

                try
                {
                    //Realiza la adquisición del plan
                    return Ok(BuyPlan(model, selectedPlan));
                }
                catch (NopException exc)
                {
                    _logger.Debug(exc.Message, exc, _workContext.CurrentCustomer);
                    return BadRequest(exc.Message);
                }
                catch (Exception exc)
                {
                    _logger.Error(exc.Message, exc, _workContext.CurrentCustomer);
                    return InternalServerError(exc);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        #region BuyPlan
        private CreateOrderResult BuyPlan(OrderPlanModel model, Product selectedPlan)
        {
            //Agrega el plan al carrito
            AddPlanToCart(selectedPlan);

            //Agrega la dirección de facturación
            var cart = AddBillingAddress(model);

            //Crea la solicitud de pago
            var paymentRequest = AddPayment(cart);

            //Procesa la orden y obtiene el resultado
            return PlaceOrder(paymentRequest);
        }

        /// <summary>
        /// Agrega el plan seleccionado al carrito
        /// </summary>
        /// <param name="selectedPlan"></param>
        private void AddPlanToCart(Product selectedPlan)
        {
            //por defecto solo se puede adquirir un solo plan al tiempo
            int quantity = 1;

            //Inicialmente no tiene atributos
            string attributes = string.Empty;


            //save item
            var addToCartWarnings = new List<string>();
            var cartType = ShoppingCartType.ShoppingCart;


            //Valida que el usuario no tenga ordenes pendientes de pagar en cierto tiempo
            if (!_orderService.CustomerCanAddPlanToCart(_workContext.CurrentCustomer.Id))
            {
                throw new NopException(CodeNopException.CategoryDoesntExist, string.Format("Ya has iniciado un proceso de compra previamente, debes esperar a que se cumplan {0} minutos antes de intentarlo nuevamente", _orderSettings.MinutesBeforeCanAddPlanToCart));
            }

            //add to the cart
            addToCartWarnings.AddRange(_shoppingCartService.AddToCart(_workContext.CurrentCustomer,
                selectedPlan, cartType, _storeContext.CurrentStore.Id,
                attributes, decimal.Zero,
                null, null, quantity, true));

            //Valida que no tenga errores al agregar al carrito
            if (addToCartWarnings.Count > 0)
            {
                string cartWarnings = String.Join("\n", addToCartWarnings.ToArray());
                _logger.Warning(cartWarnings);
                throw new NopException(cartWarnings);
            }


            //activity log
            _customerActivityService.InsertActivity("PublicStore.AddToShoppingCart", _localizationService.GetResource("ActivityLog.PublicStore.AddToShoppingCart"), selectedPlan.Name);
        }

        /// <summary>
        /// Agrega la dirección de facturación
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<ShoppingCartItem> AddBillingAddress(OrderPlanModel model)
        {
            var cart = _workContext.CurrentCustomer.ShoppingCartItems
                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                .LimitPerStore(_storeContext.CurrentStore.Id)
                .ToList();




            if (cart.Count == 0)
                throw new NopException("Your cart is empty");

            //Intenta consultar la dirección del usuario para que sea la de facturación
            if (model.AddressId > 0)
            {
                //existing address
                var address = _workContext.CurrentCustomer.Addresses.FirstOrDefault(a => a.Id == model.AddressId);
                if (address == null)
                    throw new NopException("Address can't be loaded");

                _workContext.CurrentCustomer.BillingAddress = address;
                _customerService.UpdateCustomer(_workContext.CurrentCustomer);
            }
            else
            {
                //Intenta buscar la dirección del usuario por los datos enviados
                var newAddress = _workContext.CurrentCustomer.Addresses.ToList().FindAddress(
                        _workContext.CurrentCustomer.GetFirstName(), _workContext.CurrentCustomer.GetLastName(), model.PhoneNumber,
                        _workContext.CurrentCustomer.Email, null, null,
                        model.Address, null, model.City,
                        model.StateProvinceId, null,
                        _tuilsSettings.defaultCountry, null);

                //Si no existe previamente la crea
                if (newAddress == null)
                {
                    newAddress = new Address();
                    newAddress.FirstName = _workContext.CurrentCustomer.GetFirstName();
                    newAddress.LastName = _workContext.CurrentCustomer.GetLastName();
                    newAddress.PhoneNumber = model.PhoneNumber;
                    newAddress.StateProvinceId = model.StateProvinceId;
                    newAddress.CountryId = _tuilsSettings.defaultCountry;
                    newAddress.City = model.City;
                    newAddress.Active = true;
                    newAddress.CreatedOnUtc = DateTime.UtcNow;
                    newAddress.Email = _workContext.CurrentCustomer.Email;
                    newAddress.Country = _countryService.GetCountryById(newAddress.CountryId.Value);
                    newAddress.Address1 = model.Address;
                    _workContext.CurrentCustomer.Addresses.Add(newAddress);
                }

                _workContext.CurrentCustomer.BillingAddress = newAddress;
                _customerService.UpdateCustomer(_workContext.CurrentCustomer);
            }

            return cart;
        }

        /// <summary>
        /// Realiza las validaciones del tipo de pago que va realizar y retorna el request del pago dependiendo del plugin
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private ProcessPaymentRequest AddPayment(IList<ShoppingCartItem> cart)
        {
            bool isPaymentWorkflowRequired = IsPaymentWorkflowRequired(cart, true);
            if (isPaymentWorkflowRequired)
            {
                //filter by country
                int filterByCountryId = 0;
                if (_addressSettings.CountryEnabled &&
                    _workContext.CurrentCustomer.BillingAddress != null &&
                    _workContext.CurrentCustomer.BillingAddress.Country != null)
                {
                    filterByCountryId = _workContext.CurrentCustomer.BillingAddress.Country.Id;
                }

                //payment is required
                var paymentMethodModel = PreparePaymentMethodModel(cart, filterByCountryId);

                if (_paymentSettings.BypassPaymentMethodSelectionIfOnlyOne &&
                    paymentMethodModel.PaymentMethods.Count == 1 && !paymentMethodModel.DisplayRewardPoints)
                {
                    //if we have only one payment method and reward points are disabled or the current customer doesn't have any reward points
                    //so customer doesn't have to choose a payment method

                    var selectedPaymentMethodSystemName = paymentMethodModel.PaymentMethods[0].PaymentMethodSystemName;
                    _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                        SystemCustomerAttributeNames.SelectedPaymentMethod,
                        selectedPaymentMethodSystemName, _storeContext.CurrentStore.Id);

                    var paymentMethodInst = _paymentService.LoadPaymentMethodBySystemName(selectedPaymentMethodSystemName);
                    if (paymentMethodInst == null ||
                        !paymentMethodInst.IsPaymentMethodActive(_paymentSettings) ||
                        !_pluginFinder.AuthenticateStore(paymentMethodInst.PluginDescriptor, _storeContext.CurrentStore.Id))
                        throw new Exception("Selected payment method can't be parsed");

                }
                else
                {
                    var ex = new Exception("Hay más metodos de pago activos. Porfavor comunicar este error a info@tuils.com");
                    _logger.Fatal("No hay o Hay más metodos de pago activos.", ex);
                    throw ex;
                }
            }


            //Crea el request para el pago
            var processPaymentRequest = new Nop.Services.Payments.ProcessPaymentRequest();
            processPaymentRequest.StoreId = _storeContext.CurrentStore.Id;
            processPaymentRequest.CustomerId = _workContext.CurrentCustomer.Id;
            processPaymentRequest.PaymentMethodSystemName = _workContext.CurrentCustomer.GetAttribute<string>(
                SystemCustomerAttributeNames.SelectedPaymentMethod,
                _genericAttributeService, _storeContext.CurrentStore.Id);

            return processPaymentRequest;
        }

        /// <summary>
        /// Crea la orden y genera la respuesta para el cliente
        /// </summary>
        /// <param name="processPaymentRequest"></param>
        /// <returns></returns>
        private CreateOrderResult PlaceOrder(ProcessPaymentRequest processPaymentRequest)
        {
            //Despues de cargar el tipo de pago crea la orden
            var placeOrderResult = _orderProcessingService.PlaceOrder(processPaymentRequest);

            if (placeOrderResult.Success)
            {
                var postProcessPaymentRequest = new PostProcessPaymentRequest
                {
                    Order = placeOrderResult.PlacedOrder
                };

                var paymentMethod = _paymentService.LoadPaymentMethodBySystemName(placeOrderResult.PlacedOrder.PaymentMethodSystemName);
                if (paymentMethod == null)
                {
                    throw new NopException("No fue seleccionado metodo de pago ");
                }

                //Despues de crear la orden realiza la validación y creación del Signature
                _paymentService.PostProcessPayment(postProcessPaymentRequest);

                //Asigna los datos a la respuesta y retorna
                var result = new CreateOrderResult();
                result.Signature = postProcessPaymentRequest.Signature;
                result.MerchantId = placeOrderResult.ResultPayment.AdditionalKeys["MerchantId"];
                result.ReferenceCode = postProcessPaymentRequest.ReferenceCode;
                result.AccountId = placeOrderResult.ResultPayment.AdditionalKeys["AccountId"];
                result.Amount = placeOrderResult.PlacedOrder.OrderTotal.ToString("0");
                result.Currency = _workContext.WorkingCurrency.CurrencyCode;
                result.ResponseUrl = placeOrderResult.ResultPayment.AdditionalKeys["ResponseUrl"];
                result.ConfirmationUrl = placeOrderResult.ResultPayment.AdditionalKeys["ConfirmationUrl"];
                result.UrlPayment = placeOrderResult.ResultPayment.AdditionalKeys["UrlPayment"];

                return result;
            }
            else
            {
                string placeOrderResultErrors = String.Join("\n", placeOrderResult.Errors.ToArray());
                _logger.Warning(placeOrderResultErrors);
                throw new NopException(placeOrderResultErrors);
            }
        }
            #endregion


        /// <summary>
        /// Codigo copiado de CheckoutController
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="ignoreRewardPoints"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual bool IsPaymentWorkflowRequired(IList<ShoppingCartItem> cart, bool ignoreRewardPoints = false)
        {
            bool result = true;

            //check whether order total equals zero
            decimal? shoppingCartTotalBase = _orderTotalCalculationService.GetShoppingCartTotal(cart, ignoreRewardPoints);
            if (shoppingCartTotalBase.HasValue && shoppingCartTotalBase.Value == decimal.Zero)
                result = false;
            return result;
        }

        /// <summary>
        /// Codigo tomado de Checkout controller con cambios
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="filterByCountryId"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual CheckoutPaymentMethodModel PreparePaymentMethodModel(IList<ShoppingCartItem> cart, int filterByCountryId)
        {
            var model = new CheckoutPaymentMethodModel();

            //reward points
            //if (_rewardPointsSettings.Enabled && !cart.IsRecurring())
            //{
            //    int rewardPointsBalance = _workContext.CurrentCustomer.GetRewardPointsBalance();
            //    decimal rewardPointsAmountBase = _orderTotalCalculationService.ConvertRewardPointsToAmount(rewardPointsBalance);
            //    decimal rewardPointsAmount = _currencyService.ConvertFromPrimaryStoreCurrency(rewardPointsAmountBase, _workContext.WorkingCurrency);
            //    if (rewardPointsAmount > decimal.Zero &&
            //        _orderTotalCalculationService.CheckMinimumRewardPointsToUseRequirement(rewardPointsBalance))
            //    {
            //        model.DisplayRewardPoints = true;
            //        model.RewardPointsAmount = _priceFormatter.FormatPrice(rewardPointsAmount, true, false);
            //        model.RewardPointsBalance = rewardPointsBalance;
            //    }
            //}

            //filter by country
            var paymentMethods = _paymentService
                .LoadActivePaymentMethods(_workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id, filterByCountryId)
                .Where(pm => pm.PaymentMethodType == PaymentMethodType.Standard || pm.PaymentMethodType == PaymentMethodType.Redirection)
                .Where(pm => !pm.HidePaymentMethod(cart))
                .ToList();
            foreach (var pm in paymentMethods)
            {
                if (cart.IsRecurring() && pm.RecurringPaymentType == RecurringPaymentType.NotSupported)
                    continue;

                var pmModel = new CheckoutPaymentMethodModel.PaymentMethodModel
                {
                    Name = pm.GetLocalizedFriendlyName(_localizationService, _workContext.WorkingLanguage.Id),
                    PaymentMethodSystemName = pm.PluginDescriptor.SystemName
                };
                //payment method additional fee
                //decimal paymentMethodAdditionalFee = _paymentService.GetAdditionalHandlingFee(cart, pm.PluginDescriptor.SystemName);
                //decimal rateBase = _taxService.GetPaymentMethodAdditionalFee(paymentMethodAdditionalFee, _workContext.CurrentCustomer);
                //decimal rate = _currencyService.ConvertFromPrimaryStoreCurrency(rateBase, _workContext.WorkingCurrency);
                //if (rate > decimal.Zero)
                //    pmModel.Fee = _priceFormatter.FormatPaymentMethodAdditionalFee(rate, true);

                model.PaymentMethods.Add(pmModel);
            }

            //find a selected (previously) payment method
            var selectedPaymentMethodSystemName = _workContext.CurrentCustomer.GetAttribute<string>(
                SystemCustomerAttributeNames.SelectedPaymentMethod,
                _genericAttributeService, _storeContext.CurrentStore.Id);
            if (!String.IsNullOrEmpty(selectedPaymentMethodSystemName))
            {
                var paymentMethodToSelect = model.PaymentMethods.ToList()
                    .Find(pm => pm.PaymentMethodSystemName.Equals(selectedPaymentMethodSystemName, StringComparison.InvariantCultureIgnoreCase));
                if (paymentMethodToSelect != null)
                    paymentMethodToSelect.Selected = true;
            }
            //if no option has been selected, let's do it for the first one
            if (model.PaymentMethods.FirstOrDefault(so => so.Selected) == null)
            {
                var paymentMethodToSelect = model.PaymentMethods.FirstOrDefault();
                if (paymentMethodToSelect != null)
                    paymentMethodToSelect.Selected = true;
            }

            return model;
        }



    }
}













