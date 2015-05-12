using Nop.Core;
using Nop.Services.Customers;
//using Nop.Web.Models.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Extensions;
using Nop.Core.Domain.Common;
using Nop.Services.Directory;
using Nop.Web.Models.Customer;
using Nop.Web.Models.ControlPanel;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication;
using Nop.Core.Domain.Catalog;
using Nop.Services.Messages;
using Nop.Services.ControlPanel;
using Nop.Core.Domain.ControlPanel;
using Nop.Services.Vendors;
using Nop.Web.Extensions;
using Nop.Core.Domain.Vendors;
using Nop.Services.Orders;
using Nop.Web.Models.Order;
using Nop.Services.Helpers;
using Nop.Services.Seo;
using Nop.Core.Domain.Media;
using Nop.Web.Infrastructure.Cache;
using Nop.Core.Caching;
using Nop.Services.Media;
using Nop.Web.Models.Media;
using Nop.Services.Localization;
using Nop.Web.Models.Catalog;
using Nop.Core.Domain.Orders;

namespace Nop.Web.Controllers
{
    [Authorize]
    public class ControlPanelController : Controller
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly TuilsSettings _tuilsSettings;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ICategoryService _categoryService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IStoreContext _storeContext;
        private readonly IControlPanelService _controlPanelService;
        private readonly IVendorService _vendorService;
        private readonly IOrderService _orderService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ICurrencyService _currencyService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IWebHelper _webHelper;
        private readonly ICacheManager _cacheManager;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IProductService _productService;
        private readonly MediaSettings _mediaSettings;
        private readonly ControlPanelSettings _controlPanelSettings;

        
        #endregion

        #region Ctor
        public ControlPanelController(ICustomerService customerService,
            IWorkContext workContext,
            TuilsSettings tuilsSettings,
            IStateProvinceService stateProvinceService,
            ICategoryService categoryService,
            ICustomerRegistrationService customerRegistrationService,
            IAuthenticationService authenticationService,
            IGenericAttributeService genericAttributeService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IStoreContext storeContext,
            IControlPanelService controlPanelService,
            IVendorService vendorService,
            IOrderService orderService,
            IDateTimeHelper dateTimeHelper,
            ICurrencyService currencyService,
            IPriceFormatter priceFormatter,
            MediaSettings mediaSettings,
            IWebHelper webHelper,
            ICacheManager cacheManager,
            IPictureService pictureService,
            ILocalizationService localizationService,
            ControlPanelSettings controlPanelSettings,
            IProductService productService)
        {
            this._customerService = customerService;
            this._workContext = workContext;
            this._tuilsSettings = tuilsSettings;
            this._stateProvinceService = stateProvinceService;
            this._categoryService = categoryService;
            this._customerRegistrationService = customerRegistrationService;
            this._authenticationService = authenticationService;
            this._genericAttributeService = genericAttributeService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._storeContext = storeContext;
            this._controlPanelService = controlPanelService;
            this._vendorService = vendorService;
            this._orderService = orderService;
            this._dateTimeHelper = dateTimeHelper;
            this._currencyService = currencyService;
            this._priceFormatter = priceFormatter;
            this._mediaSettings = mediaSettings;
            this._webHelper = webHelper;
            this._cacheManager = cacheManager;
            this._localizationService = localizationService;
            this._pictureService = pictureService;
            this._controlPanelSettings = controlPanelSettings;
            this._productService = productService;
        }
        #endregion

        #region ControlPanel
        public ActionResult Index()
        {
            return View(PrepareControlPanelModel());
        }
        
        /// <summary>
        /// Carga las variables del modelo del panel de control
        /// </summary>
        /// <returns></returns>
        private ControlPanelModel PrepareControlPanelModel()
        {
            var model = new ControlPanelModel();
            model.Modules = this._controlPanelService.GetModulesActiveUser();
            model.Customer = _workContext.CurrentCustomer.ToMyAccountModel();

            if (_workContext.CurrentVendor != null)
            {
                model.AvgRating = _workContext.CurrentVendor.AvgRating ?? 0;
                model.NumRatings = _workContext.CurrentVendor.NumRatings;

                //trae todos los productos del vendedor y los cuenta
                var vendorProducts =  _productService.SearchProducts(vendorId: _workContext.CurrentVendor.Id);
                model.PublishedProducts = vendorProducts.Count;

                //Consulta todas las ventas del vendedor
                var vendorSellings = _orderService.SearchOrders(vendorId:_workContext.CurrentVendor.Id);
                model.SoldProducts = vendorSellings.Count;

            }

            return model;

        }
        #endregion
        


        #region MyAccount

        public ActionResult MyAccount()
        {
            var model = GetModelMyAccount(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult MyAccount(MyAccountModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            try
            {
                if (ModelState.IsValid)
                {

                    //email
                    if (!customer.Email.Equals(model.Email.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        _customerRegistrationService.SetEmail(customer, model.Email.Trim());
                        _authenticationService.SignIn(customer, true);
                    }
                    
                    //Actualiza el valor del genero
                    model.Gender = model.Gender == null ? "M" : "F";

                    //Guarda los atributos
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender); 
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeBrandId, model.BikeBrandId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeReferenceId, model.BikeReferenceId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeYear, model.BikeYear);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeCarriagePlate, model.BikeCarriagePlate);

                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.Newsletter, Core.Domain.Messages.NewsLetterSuscriptionType.General);
                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.NewsletterBrand, Core.Domain.Messages.NewsLetterSuscriptionType.MyBrand);
                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.NewsletterReference, Core.Domain.Messages.NewsLetterSuscriptionType.MyReference);
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message);
            }

            model = GetModelMyAccount(model);

            return View(model);
        }

        private MyAccountModel GetModelMyAccount(MyAccountModel model)
        {
            
            if(model == null)
                model = _workContext.CurrentCustomer.ToMyAccountModel();

            model.BikeReferences = model.BikeBrandId.HasValue ? _categoryService.GetAllCategoriesByParentCategoryId(model.BikeBrandId.Value) : new List<Category>();

            model.States = _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
            model.BikeBrands = _categoryService.GetAllCategoriesByParentCategoryId(_tuilsSettings.productBaseTypes_bike);
            return model;
        }

        #endregion

        #region MySales

        public ActionResult MySales(MyOrdersPagingFilteringModel command)
        {
            var model = PrepareMyOrdersModel(command, false);
            return View(model);
        }

        #endregion

        #region Offices
        public ActionResult Offices()
        {
            var model = new OfficesModel();
            model.States = _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
            model.VendorId = _workContext.CurrentVendor.Id;
            return View(model);
        }
        #endregion

        #region Verdor Services
        public ActionResult VendorServices()
        {
            if (_workContext.CurrentVendor != null)
            {
                int vendorId = _workContext.CurrentVendor.Id;
                var model = new VendorServicesModel();
                var specialCategories = _vendorService.GetSpecialCategoriesByVendorId(vendorId);
                model.SpecializedCategories = specialCategories.Where(c => c.SpecialType == Core.Domain.Vendors.SpecialCategoryVendorType.SpecializedCategory)
                    .Select(c => c.CategoryId)
                    .ToList();
                model.BikeReferences = specialCategories.Where(c => c.SpecialType == Core.Domain.Vendors.SpecialCategoryVendorType.BikeBrand)
                    .Select(c => c.CategoryId)
                    .ToList();
                model.SpecializedCategoriesString = model.SpecializedCategories.ToStringSeparatedBy();
                model.BikeReferencesString = model.BikeReferences.ToStringSeparatedBy();

                return View(model);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult VendorServices(VendorServicesModel model)
        {
            
            if(_workContext.CurrentVendor != null)
            {
                //Toma la cadena separada por comas y crea una lista de categorias relacinoadas
                var bikeReferences = model.BikeReferencesString
                .Split(new char[]{ ',' })
                .ToList()
                .Select(c => new SpecialCategoryVendor() { CategoryId = Convert.ToInt32(c), VendorId = _workContext.CurrentVendor.Id, SpecialType = SpecialCategoryVendorType.BikeBrand  });

                var specializedCategories = model.SpecializedCategoriesString
                .Split(new char[]{ ',' })
                .ToList()
                .Select(c => new SpecialCategoryVendor() { CategoryId = Convert.ToInt32(c), VendorId = _workContext.CurrentVendor.Id, SpecialType = SpecialCategoryVendorType.SpecializedCategory  });

                //Concatena las dos listas anteriores y las envía a ser actualizadas
                _vendorService.InsertUpdateVendorSpecialCategories(_workContext.CurrentVendor.Id, bikeReferences.Concat(specializedCategories).ToList());

                return View(model);
            }
            else
            {
                return this.HttpNotFound();
            }

        }
        #endregion

        #region MyOrders

        public ActionResult MyOrders(MyOrdersPagingFilteringModel command)
        {
            var model = PrepareMyOrdersModel(command, true);
            return View(model);
        }

        /// <summary>
        /// Llena los modelos para retornar en las secciones de mis ventas y mis compras
        /// </summary>
        /// <param name="command">Variables de filtro</param>
        /// <param name="isMyOrders">True: Consulta las compras del usuario en sesión. False: Consulta las ventas</param>
        /// <returns>Modelo lleno</returns>
        [NonAction]
        protected virtual MyOrdersModel PrepareMyOrdersModel(MyOrdersPagingFilteringModel command, bool isMyOrders)
        {
            var model = new MyOrdersModel();

            //configura el paginador
            PreparePageSizeOptions(model.PagingFilteringContext, command);

            bool? publishedProducts = null;
            bool? withRating = null;

            switch (command.Filter)
            {
                case "rating":
                    withRating = true;
                    break;
                case "norating":
                    withRating = false;
                    break;
                case "active":
                    publishedProducts = true;
                    break;
                default:
                    break;
            }


            IPagedList<Order> orders = null;
            if (isMyOrders)
                orders = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id,
                    customerId: _workContext.CurrentCustomer.Id, pageIndex: command.PageIndex, pageSize: command.PageSize,
                    publishedProducts: publishedProducts, withRating: withRating);
            else
                orders = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id, 
                    vendorId: _workContext.CurrentVendor.Id, pageIndex: command.PageIndex, pageSize: command.PageSize,
                    publishedProducts: publishedProducts, withRating: withRating);


            foreach (var order in orders)
            {
                var orderModel = new OrderItemModel
                {
                    Id = order.Id,
                    CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc),
                };

                var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
                orderModel.Price = _priceFormatter.FormatPrice(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, _workContext.WorkingLanguage);

                if (order.OrderItems.Count > 0)
                {
                    var item = order.OrderItems.FirstOrDefault();
                    orderModel.Rating = item.Rating;
                    orderModel.Product = new Models.Catalog.ProductOverviewModel() { 
                         Id = item.Product.Id,
                         Name = item.Product.Name,
                         SeName = item.Product.GetSeName()
                    };

                    #region DefaultPictureModel
                    int pictureSize = _mediaSettings.ProductThumbPictureSize;
                    //prepare picture model
                    var defaultProductPictureCacheKey = string.Format(ModelCacheEventConsumer.PRODUCT_DEFAULTPICTURE_MODEL_KEY, orderModel.Product.Id, pictureSize, true, _workContext.WorkingLanguage.Id, _webHelper.IsCurrentConnectionSecured(), 1);
                    orderModel.Product.DefaultPictureModel = _cacheManager.Get(defaultProductPictureCacheKey, () =>
                    {
                        var picture = _pictureService.GetPicturesByProductId(orderModel.Product.Id, 1).FirstOrDefault();
                        var pictureModel = new PictureModel
                        {
                            ImageUrl = _pictureService.GetPictureUrl(picture, pictureSize),
                            FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                            Title = string.Format(_localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), orderModel.Product.Name),
                            AlternateText = string.Format(_localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), orderModel.Product.Name)
                        };
                        return pictureModel;
                    });
                    #endregion

                    if (isMyOrders)
                        orderModel.Vendor = new Models.Catalog.VendorModel()
                        {
                            Id = item.Product.VendorId,
                            Name = item.Product.Vendor.Name,
                            SeName = item.Product.Vendor.GetSeName()
                        };
                    else
                    {
                         orderModel.Customer = new CustomerInfoModel() { 
                            FirstName = order.Customer.GetAttribute<string>("FirstName"),
                            LastName = order.Customer.GetAttribute<string>("LastName"),
                            Phone = order.Customer.GetAttribute<string>("Phone"),
                            Email = order.Customer.Email
                        };
                    }
                       
                }
                else
                {
                    throw new NopException("La orden no tiene productos asociados");
                }

                model.Orders.Add(orderModel);
            }

            model.PagingFilteringContext.LoadPagedList(orders);

            return model;
        }

        [NonAction]
        protected virtual void PreparePageSizeOptions(CatalogPagingFilteringModel pagingFilteringModel, MyOrdersPagingFilteringModel command)
        {
            if (pagingFilteringModel == null)
                throw new ArgumentNullException("pagingFilteringModel");

            if (command == null)
                throw new ArgumentNullException("command");

            if (command.PageNumber <= 0)
            {
                command.PageNumber = 1;
            }

            command.PageSize = _controlPanelSettings.defaultPageSize;
        }

        #endregion

        #region Menu
        [ChildActionOnly]
        public ActionResult Menu() 
        {
            var model = new MenuModel();
            model.Modules = this._controlPanelService.GetModulesActiveUser();

            //Busca cual es el modulo actual
            string parentModule = string.Empty;
            model.SelectedModule = GetCurrentModule(model.Modules, ref parentModule);
            model.SelectedParentModule = parentModule;

            return PartialView("_Menu", model);
        }

        /// <summary>
        /// Retorna el modulo seleccionado actualmente, por referencia envia el modulo padre seleccionado
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private string GetCurrentModule(List<ControlPanelModule> modules, ref string parent)
        {
            string currentAction = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString();
            string currentController = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();
            foreach (var module in modules)
            {
                parent = module.Name;
                if (module.Action.Equals(currentAction) && module.Controller.Equals(currentController))
                    return module.Name;
                else
                {
                    //Si no es de tipo padre recorre los submodulos
                    foreach (var sm in module.SubModules)
                    {
                        if (sm.Action.Equals(currentAction) && sm.Controller.Equals(currentController))
                            return module.Name;
                    }
                }

            }
            return string.Empty;
        }


        
        #endregion
    }
}