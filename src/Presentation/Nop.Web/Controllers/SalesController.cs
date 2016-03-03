using Nop.Core.Domain.Common;
using Nop.Services.Catalog;
using Nop.Web.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Extensions;
using Nop.Services.Directory;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework;
using Nop.Core.Caching;
using Nop.Web.Infrastructure.Cache;
using Nop.Services.Customers;
using Nop.Services.Common;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Core.Domain.Orders;
using Nop.Services.Vendors;
using Nop.Core.Domain.Vendors;
using Nop.Services.Localization;
using Nop.Core.Domain.Media;
using Nop.Services.Media;

namespace Nop.Web.Controllers
{
    public class SalesController : BasePublicController
    {

        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly TuilsSettings _tuilsSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;
        private readonly PlanSettings _planSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IVendorService _vendorService;
        private readonly ILocalizationService _localizationService;
        private readonly MediaSettings _mediaSettings;
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctor

        public SalesController(ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            IStateProvinceService stateProvinceService,
            TuilsSettings tuilsSettings,
            IWorkContext workContext,
            CatalogSettings catalogSettings,
            IProductService productService,
            PlanSettings planSettings,
            ICacheManager cacheManager,
            IOrderService orderService,
            IPaymentService paymentService,
            IPriceFormatter priceFormatter,
            IOrderProcessingService orderProcessingService,
            IVendorService vendorService,
            ILocalizationService localizationService,
            MediaSettings mediaSettings,
            IPictureService pictureService)
        {
            this._categoryService = categoryService;
            this._tuilsSettings = tuilsSettings;
            this._specificationAttributeService = specificationAttributeService;
            this._stateProvinceService = stateProvinceService;
            this._workContext = workContext;
            this._catalogSettings = catalogSettings;
            this._productService = productService;
            this._planSettings = planSettings;
            this._cacheManager = cacheManager;
            this._orderService = orderService;
            this._paymentService = paymentService;
            this._priceFormatter = priceFormatter;
            this._orderProcessingService = orderProcessingService;
            this._vendorService = vendorService;
            this._localizationService = localizationService;
            this._mediaSettings = mediaSettings;
            this._pictureService = pictureService;
        }
        #endregion

        // GET: /quiero-vender/
        /// <summary>
        /// Pantalla principal para vender un producto
        /// </summary>
        /// <returns></returns>
        [CheckFacebookBrowser]
        public ActionResult Index()
        {
            var model = new SelectPublishCategoryModel();
            //Solo los talleres pueden publicar servicios
            if (_workContext.CurrentVendor != null)
            {
                model.CanSelectService = _workContext.CurrentVendor.VendorType != VendorType.User;
                int limit;
                //Si el usuario tiene más productos publicados que el limite, muestra un mensaje de advertencia
                model.HasReachedLimitOfProducts = HasReachedLimitOfProducts(out limit);
                model.NumLimitOfProducts = limit;
            }

            model.VendorType = _workContext.CurrentVendor != null ? _workContext.CurrentVendor.VendorType : VendorType.User;

            return View(model);
        }

        private bool HasReachedLimitOfProducts(out int limit)
        {
            limit = 0;
            if (_workContext.CurrentVendor != null)
                //Si el usuario tiene más productos publicados que el limite, muestra un mensaje de advertencia
                return _productService.HasReachedLimitOfProducts(_workContext.CurrentVendor, out limit);

            return false;
        }

        /// <summary>
        /// Publicación de producto simple
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishProduct(int? id)
        {
            var model = GetPublishModel();

            //Tipo de producto servicio
            model.ProductType = ProductTypePublished.Product;
            model.SubSectionTitle = "Productos";

            return View(model);
        }

        /// <summary>
        /// Publicación de producto tipo motocicleta
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishProductBike(int? id)
        {
            var model = GetPublishModel();

            //Caga los colores existentes
            model.ColorOptions = new SelectList(
                _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeColor)
                .Select(a => new { Id = a.Id, Name = a.Name })
                .ToList(),
                "Id",
                "Name");

            model.ConditionOptions = new SelectList(
                _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeCondition)
                .Select(a => new { Id = a.Id, Name = a.Name })
                .ToList(),
                "Id",
                "Name");

            model.AccesoriesOptions = _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeAccesories);

            model.NegotiationOptions = _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeNegotiation);

            //Tipo de producto moto
            model.ProductType = ProductTypePublished.Bike;
            model.SubSectionTitle = "Motos";

            return View("PublishProduct", model);
        }

        /// <summary>
        /// OPción para publicar un servicio
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishProductService(int? id)
        {
            var model = GetPublishModel();

            //Tipo de producto moto
            model.ProductType = ProductTypePublished.Service;
            model.SubSectionTitle = "Servicios";

            return View("PublishProduct", model);
        }

        #region Planes Pagos
        /// <summary>
        /// Muestra el listado de planes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Plans(string tab)
        {
            var model = new ShowAllPlansModel();

            var vendor = _workContext.CurrentVendor;

            if (tab.Equals("empresas")
                && !_workContext.CurrentCustomer.IsGuest()
                && vendor != null && vendor.VendorType == VendorType.Market)
                return RedirectToAction("SelectPlanVendor");



            model.IsAuthenticated = !_workContext.CurrentCustomer.IsGuest();

            //El boton de comprar para usuarios se oculta si una tienda está autenticada
            model.HideBuyButtonForUsers = model.IsAuthenticated && vendor != null && vendor.VendorType == VendorType.Market;
            //El boton de comprar para tiendas se oculta si un usuario está autenticada
            model.HideBuyButtonForMarket = model.IsAuthenticated && vendor != null && vendor.VendorType == VendorType.User;

            model.MarketPlans = new SelectPlanModel() { VendorType = Core.Domain.Vendors.VendorType.Market, Plans = LoadPlansByVendorType(VendorType.Market) };
            model.UserPlans = new SelectPlanModel() { VendorType = Core.Domain.Vendors.VendorType.User, Plans = LoadPlansByVendorType(VendorType.User) };


            model.MarketPlans.FeaturedPlan = model.MarketPlans.Plans.Count > 1 ? model.MarketPlans.Plans[1].Id : _planSettings.PlanStoresFree;
            model.UserPlans.FeaturedPlan = model.UserPlans.Plans.Count > 1 ? model.UserPlans.Plans[1].Id : _planSettings.PlanProductsFree;

            return View(model);
        }
        
        
        /// <summary>
        /// Funcionalidad que permite seleccionar un plan desde un producto seleccionado o publicado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [SameVendorProduct]
        public ActionResult SelectPlan(int id, Product product, SelectPlanRequest command)
        {
            //Si el vendedor tiene un plan activo que no ha expirado lo envia directamente a destacar
            if (_workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.Market &&
                _workContext.CurrentVendor.CurrentOrderPlanId.HasValue && _workContext.CurrentVendor.PlanExpiredOnUtc > DateTime.UtcNow
                && (!command.force.HasValue || !command.force.Value) )
            {
                return RedirectToAction("SelectFeaturedAttributesByPlan", "Catalog", new { id = id, from = "publish" });
            }

            if (product.Sold)
                return RedirectToAction("MyProducts", "ControlPanel");

            var model = new SelectPlanModel() { ProductId = id };
            //Carga los planes para seleccionar
            PrepareSelectPlanModel(model, command, product);

            return View(model);
        }

        /// <summary>
        /// Permite la selección de un plan. SOLO aplica para tiendas
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult SelectPlanVendor(SelectPlanRequest command)
        {
            if (_workContext.CurrentVendor == null || _workContext.CurrentVendor.VendorType != VendorType.Market)
                return RedirectToAction("Index", "ControlPanel");
            
            var model = new SelectPlanModel();
            PrepareSelectPlanModel(model, command);
            model.DisableFreePlan = true;
            return View("SelectPlan", model);
        }

        /// <summary>
        /// Selected product
        /// </summary>
        /// <param name="model"></param>
        /// <param name="command"></param>
        /// <param name="product"></param>
        private void PrepareSelectPlanModel(SelectPlanModel model, SelectPlanRequest command, Product product = null)
        {

            //Carga los planes de cache
            model.Plans = LoadPlansByVendorType(_workContext.CurrentVendor.VendorType);
            //Carga el plan del vendor para saber cual destacar
            var vendorPlan = _workContext.CurrentVendor.GetCurrentPlan(_productService, _planSettings);

            //Si es un upgrade no le marca ningún plan al usuario
            if (!command.limit.HasValue || command.plan.HasValue)
            {
                model.FeaturedPlan = command.plan.HasValue ? command.plan.Value : vendorPlan.ProductId;

                //Si lo que se va a hacer es refrendar un plan, carga los datos adicionales abiertos
                if (vendorPlan.ProductId != _planSettings.PlanStoresFree || command.plan.HasValue)
                    model.AutoShowAdditionalData = true;
            }
            else
                model.IsUpgrade = true;
            
            
            //Si viene forzado deshabilita los planes que no se adecuen
            if (command.force.HasValue && command.force.Value)
            {
                foreach (var plan in model.Plans)
                {
                    //busca la propiedad que contiene el limite de productos por plan
                    var spec = plan.Specifications.FirstOrDefault(s => s.SpecificationAttributeId == _planSettings.SpecificationAttributeIdLimitProducts);
                    //Si el numero de productos permitidos a publicar por el plan es menor que el limite del actual lo deshabilita
                    if (spec != null && Convert.ToInt32(spec.Value) <= command.limit)
                    {
                        model.DisabledPlans.Add(plan.Id);
                    }
                }
            }

            //Si el cliente tiene direcciones registradas la carga
            if (_workContext.CurrentCustomer.Addresses.Count > 0)
            {
                var address = _workContext.CurrentCustomer.BillingAddress ?? _workContext.CurrentCustomer.Addresses.FirstOrDefault();
                model.CustomerAddressInformation.AddressId = address.Id;
                model.CustomerAddressInformation.Address = address.Address1;
                model.CustomerAddressInformation.PhoneNumber = address.PhoneNumber;
                model.CustomerAddressInformation.City = address.City;
                model.CustomerAddressInformation.StateProvinceId = address.StateProvinceId.Value;
            }
            else
            {
                //Si viene con producto seleccionado asigna la ciudad del producto al formulario
                if (product != null)
                    model.CustomerAddressInformation.StateProvinceId = product.StateProvinceId.Value;
                
                model.CustomerAddressInformation.PhoneNumber = _workContext.CurrentVendor.PhoneNumber;
            }

            string cacheStatesKey = string.Format(ModelCacheEventConsumer.STATEPROVINCES_BY_COUNTRY_MODEL_KEY, _tuilsSettings.defaultCountry, "empty", _workContext.WorkingLanguage.Id);
            model.StateProvinces = new SelectList(
                _cacheManager.Get(cacheStatesKey, () => { return _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry); })
                , "Id", "Name",
                model.CustomerAddressInformation.StateProvinceId);

            //Carga el tipo de vendedor
            if (_workContext.CurrentVendor != null)
                model.VendorType = _workContext.CurrentVendor.VendorType;

            model.CustomerInformation.Email = _workContext.CurrentCustomer.Email;
            model.CustomerInformation.PhoneNumber = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
            model.CustomerInformation.FullName = _workContext.CurrentCustomer.GetFullName();
            model.IsTest = _planSettings.RunLikeTest;
            model.DisableFreePlan = command.retry;
        }

        /// <summary>
        /// Carga los planes dependiendo del tipo de vendedor
        /// </summary>
        /// <param name="vendorType"></param>
        /// <returns></returns>
        private List<Models.Sales.PlanModel> LoadPlansByVendorType(VendorType vendorType)
        {
            //Consulta de acuerdo al tipo de vendedor la categoria desde la cual va sacar los planes
            int categoryPlanId = vendorType == Core.Domain.Vendors.VendorType.User ? _planSettings.CategoryProductPlansId : _planSettings.CategoryStorePlansId;

            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_ACTIVE_PLANS_MODEL_KEY, categoryPlanId);
            return _cacheManager.Get(cacheKey, () =>
            {

                //Consulta tdos los productos pertenecientes a la categoria de los planes
                var plans = _productService.SearchProducts(categoryIds: new List<int>() { categoryPlanId }, hidden: true, orderBy: ProductSortingEnum.PriceAsc);

                var listPlans = new List<Nop.Web.Models.Sales.PlanModel>();

                foreach (var plan in plans)
                {
                    var planModel = new Nop.Web.Models.Sales.PlanModel();
                    planModel.Id = plan.Id;
                    planModel.Name = plan.Name;
                    planModel.Price = _priceFormatter.FormatPrice(plan.Price);
                    planModel.PriceDecimal = plan.Price;

                    //Agrega las caracteristicas del plan
                    foreach (var spec in plan.ProductSpecificationAttributes.Where(ps => ps.ShowOnProductPage).OrderBy(ps => ps.DisplayOrder))
                    {
                        //Busca si el atributo fue agregado previamente principalmente para sliders
                        var specAddedPreviously = planModel.Specifications.FirstOrDefault(s => s.SpecificationAttributeId == spec.SpecificationAttributeOption.SpecificationAttributeId);
                        //Si no fue agregada la agrega
                        if (specAddedPreviously == null)
                        {
                            string specValue = string.IsNullOrEmpty(spec.CustomValue) ? spec.SpecificationAttributeOption.Name : spec.CustomValue;
                            int specAttributeId = spec.SpecificationAttributeOption.SpecificationAttributeId;

                            bool showWithCheck = false;
                            if (vendorType == VendorType.Market)
                            { 
                                //Solo muestra en la interfaz el check y el numero para las siguientes caracteristicas
                                showWithCheck = specAttributeId == _planSettings.SpecificationAttributeIdProductsFeaturedOnSliders 
                                    || specAttributeId == _planSettings.SpecificationAttributeIdProductsOnHomePage
                                    || specAttributeId == _planSettings.SpecificationAttributeIdProductsOnSocialNetworks;
                            }
                            
                            
                            //Para el valor de las bandas rotativas solo actualiza con un Si
                            if (specAttributeId == _planSettings.SpecificationAttributeIdSliders)
                                specValue = "Si";
                            else if (specAttributeId == _planSettings.SpecificationAttributeIdDisplayOrder)
                                specValue = _localizationService.GetResource(string.Format("showplans.specificationAttributeDisplayOrder.{0}", specValue));
                            
                            planModel.Specifications.Add(new Nop.Web.Models.Sales.PlanModel.SpecificationPlanModel()
                            {
                                Name = spec.SpecificationAttributeOption.SpecificationAttribute.Name,
                                SpecificationAttributeId = specAttributeId,
                                Value = specValue,
                                Description = _localizationService.GetResource(string.Format("showplans.specificationAttributeDescription.{0}.{1}", vendorType, specAttributeId)),
                                ShowWithCheck = showWithCheck
                            });
                        }
                        //else
                        //{ 
                        //    //Sino actualiza el valor
                        //    string specValue = string.IsNullOrEmpty(spec.CustomValue) ? spec.SpecificationAttributeOption.Name : spec.CustomValue;
                        //    specAddedPreviously.Value = string.Concat(specAddedPreviously.Value, specValue);
                        //}

                    }

                    listPlans.Add(planModel);
                }

                return listPlans;
            });
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentResponse(int referenceCode)
        {
            if (referenceCode <= 0)
                return HttpNotFound();


            //Consulta la información de la orden para validar que exista
            var order = _orderService.GetOrderById(referenceCode);

            if (order == null)
                return HttpNotFound();

            //consulta el metodo de pago para cargar la respuesta
            var selectedPaymentMethod = _paymentService.LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
            string controllerResponse = null;
            string actionResponse = null;
            System.Web.Routing.RouteValueDictionary routeValues;
            selectedPaymentMethod.GetPaymentInfoRoute(out actionResponse, out controllerResponse, out routeValues);

            var model = new PaymentResponseModel();
            model.PluginControllerResponse = controllerResponse;
            model.PluginActionResponse = actionResponse;
            model.PluginRouteValuesResponse = routeValues;

            return View(model);
        }


        #endregion

        [HttpGet]
        [Authorize]
        [SameVendorProduct]
        public ActionResult ConfirmationWithoutPlan(int id, Product product)
        {
            var model = new ConfirmationWithoutPlanModel();
            model.ProductDetails.Name = product.Name;

            model.ProductDetails.ProductPrice.CallForPrice = product.CallForPrice;
            model.ProductDetails.ProductPrice.Price = _priceFormatter.FormatPrice(product.Price);
            model.ProductDetails.DefaultPictureModel = product.GetPicture(_localizationService, _mediaSettings, _pictureService);

            //De acuerdo a si es tienda o no carga el limite de dias de publicación
            model.LimitDaysOfProductPublished = _workContext.CurrentVendor.GetCurrentPlan(_productService, _planSettings).DaysPlan;
            model.VendorType = _workContext.CurrentVendor.VendorType;
            model.IsMobileDevice = Request.Browser.IsMobileDevice;
            

            return View(model);
        }


        #region Metodos Privados
        private PublishProductModel GetPublishModel()
        {
            var selectedPlan = _productService.GetPlanById(_workContext.CurrentVendor != null && _workContext.CurrentVendor.VendorType != VendorType.Market ? _planSettings.PlanProductsFree : _planSettings.PlanStoresFree);
            
            var model = new PublishProductModel();
            model.LimitDaysOfProductPublished = selectedPlan.DaysPlan;

            string cacheStatesKey = string.Format(ModelCacheEventConsumer.STATEPROVINCES_BY_COUNTRY_MODEL_KEY, _tuilsSettings.defaultCountry, "empty", _workContext.WorkingLanguage.Id);

            var stateProvinces = _cacheManager.Get(cacheStatesKey, () => {
                return _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
            });

            model.StateProvinces = new SelectList(stateProvinces, "Id", "Name");

            model.IsMobileDevice = Request.Browser.IsMobileDevice;
            int limit;
            model.HasReachedLimitOfProducts = HasReachedLimitOfProducts(out limit);
            model.VendorType = _workContext.CurrentVendor != null ? _workContext.CurrentVendor.VendorType : VendorType.User;
            model.NumLimitOfProducts = limit;
            model.MaxSizeFileUpload = _tuilsSettings.maxFileUploadSize;
            if (_workContext.CurrentVendor != null)
                model.PhoneNumber = _workContext.CurrentVendor.PhoneNumber;
            return model;
        }
        #endregion
    }

    public enum ProductTypePublished
    {
        Product,
        Service,
        Bike
    }
}