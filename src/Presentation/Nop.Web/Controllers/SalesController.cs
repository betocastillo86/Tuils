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
        public ActionResult Index()
        {
            var model = new SelectPublishCategoryModel();
            //Solo los talleres pueden publicar servicios
            if (_workContext.CurrentVendor != null)
            {
                model.CanSelectService = _workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.RepairShop;
                //Si el usuario tiene más productos publicados que el limite, muestra un mensaje de advertencia
                model.HasReachedLimitOfProducts = HasReachedLimitOfProducts();
            }

            return View(model);
        }

        private bool HasReachedLimitOfProducts()
        {
            if (_workContext.CurrentVendor != null)
                //Si el usuario tiene más productos publicados que el limite, muestra un mensaje de advertencia
                return _productService.HasReachedLimitOfProducts(_workContext.CurrentVendor.Id);
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
        /// Funcionalidad que permite seleccionar un plan  
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [SameVendorProduct]
        public ActionResult SelectPlan(int id, Product product)
        {
            //Consulta de acuerdo al tipo de vendedor la categoria desde la cual va sacar los planes
            int categoryPlanId = _workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.User ? _planSettings.CategoryProductPlansId : _planSettings.CategoryStorePlansId;

            //Si el vendedor tiene un plan activo que no ha expirado lo envia directamente a destacar
            if (_workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.Market &&
                _workContext.CurrentVendor.CurrentOrderPlanId.HasValue && _workContext.CurrentVendor.PlanExpiredOnUtc > DateTime.UtcNow)
            {
                return RedirectToAction("SelectFeaturedAttributesByPlan", "Catalog", new { id = id });
            }

            var model = new SelectPlanModel() { ProductId = id };

            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_ACTIVE_PLANS_MODEL_KEY, categoryPlanId);
            model.Plans = _cacheManager.Get(cacheKey, () =>
            {

                //Consulta tdos los productos pertenecientes a la categoria de los planes
                var plans = _productService.SearchProducts(categoryIds: new List<int>() { categoryPlanId });

                var listPlans = new List<SelectPlanModel.PlanModel>();

                foreach (var plan in plans)
                {
                    var planModel = new SelectPlanModel.PlanModel();
                    planModel.Id = plan.Id;
                    planModel.Name = plan.Name;
                    planModel.Price = _priceFormatter.FormatPrice(plan.Price); 
                    //Agrega las caracteristicas del plan
                    foreach (var spec in plan.ProductSpecificationAttributes)
                    {
                        planModel.Specifications.Add(new SelectPlanModel.SpecificationPlan()
                        {
                            Name = spec.SpecificationAttributeOption.SpecificationAttribute.Name,
                            SpecificationAttributeId = spec.SpecificationAttributeOption.SpecificationAttributeId,
                            Value = string.IsNullOrEmpty(spec.CustomValue) ? spec.SpecificationAttributeOption.Name : spec.CustomValue
                        });
                    }

                    listPlans.Add(planModel);
                }

                return listPlans;
            });

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

            string cacheStatesKey = string.Format(ModelCacheEventConsumer.STATEPROVINCES_BY_COUNTRY_MODEL_KEY, _tuilsSettings.defaultCountry, "empty", _workContext.WorkingLanguage.Id);
            model.StateProvinces = new SelectList(
                _cacheManager.Get(cacheStatesKey, () => { return _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry); })
                , "Id", "Name",
                model.CustomerAddressInformation.StateProvinceId);

            model.CustomerInformation.Email = _workContext.CurrentCustomer.Email;
            model.CustomerInformation.PhoneNumber = _workContext.CurrentCustomer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
            model.CustomerInformation.FullName = _workContext.CurrentCustomer.GetFullName();


            return View(model);
        }

        public ActionResult Prueba()
        {

            return Content(string.Empty);
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
            model.ProductDetails.ProductPrice.Price = _priceFormatter.FormatPrice(product.Price);
            model.ProductDetails.DefaultPictureModel = product.GetPicture(_localizationService, _mediaSettings, _pictureService);
            
            //De acuerdo a si es tienda o no carga el limite de dias de publicación
            model.LimitDaysOfProductPublished = _workContext.CurrentVendor.VendorType == VendorType.Market ? _catalogSettings.LimitDaysOfStoreProductPublished : _catalogSettings.LimitDaysOfProductPublished;

            return View(model);
        }


        #region Metodos Privados
        private PublishProductModel GetPublishModel()
        {
            var model = new PublishProductModel();
            model.LimitDaysOfProductPublished = _catalogSettings.LimitDaysOfProductPublished;

            string cacheStatesKey = string.Format(ModelCacheEventConsumer.STATEPROVINCES_BY_COUNTRY_MODEL_KEY, _tuilsSettings.defaultCountry, "empty", _workContext.WorkingLanguage.Id);

            var stateProvinces = _cacheManager.Get(cacheStatesKey, () => {
                return _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
            });

            model.StateProvinces = new SelectList(stateProvinces, "Id", "Name");

            model.IsMobileDevice = Request.Browser.IsMobileDevice;
            model.HasReachedLimitOfProducts = HasReachedLimitOfProducts();
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