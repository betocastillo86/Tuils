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
using Nop.Web.Extensions.Api;
using Nop.Services.Security;

namespace Nop.Web.Controllers
{
    [Authorize]
    public class ControlPanelController : BasePublicController
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
        private readonly IPermissionService _permissionService;
        private readonly MediaSettings _mediaSettings;
        private readonly ControlPanelSettings _controlPanelSettings;
        private readonly PlanSettings _planSettings;


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
            IProductService productService,
            PlanSettings planSettings)
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
            this._planSettings = planSettings;
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
            model.ShowWelcomeMessage = !string.IsNullOrEmpty(Request.QueryString["w"]);

            if (_workContext.CurrentVendor != null)
            {
                model.AvgRating = _workContext.CurrentVendor.AvgRating ?? 0;
                model.NumRatings = _workContext.CurrentVendor.NumRatings;

                //cuenta el numero de productos activos
                model.PublishedProducts = _productService.CountActiveProductsByVendorId(vendorId: _workContext.CurrentVendor.Id);

                //Consulta todas las ventas del vendedor
                var vendorSellings = _orderService.SearchOrders(vendorId: _workContext.CurrentVendor.Id);
                model.SoldProducts = vendorSellings.Count;

                if(model.PublishedProducts > 0)
                    //Suma el numero de preguntas sin responder
                    model.UnansweredQuestions = _productService.CountUnansweredQuestionsByVendorId(_workContext.CurrentVendor.Id);

                model.VendorType = _workContext.CurrentVendor.VendorType;
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
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeBrandId, model.BikeBrand.CategoryId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeReferenceId, model.BikeReferenceId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeYear, model.BikeYear);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeCarriagePlate, model.BikeCarriagePlate);

                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.Newsletter, Core.Domain.Messages.NewsLetterSuscriptionType.General, customer.GetFullName() );
                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.NewsletterBrand, Core.Domain.Messages.NewsLetterSuscriptionType.MyBrand, customer.GetFullName());
                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.NewsletterReference, Core.Domain.Messages.NewsLetterSuscriptionType.MyReference, customer.GetFullName());

                    ///Actualiza los teléfonos del vendedor
                    if (_workContext.CurrentVendor != null)
                    {
                        _workContext.CurrentVendor.PhoneNumber = model.Phone;
                        _vendorService.UpdateVendor(_workContext.CurrentVendor); 
                    }

                    model.ConfirmMessage = _localizationService.GetResource("MyAccount.Confirm");

                }
                else
                {
                    model.ConfirmMessage = _localizationService.GetResource("MyAccount.ModelInvalid");
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message);
                model.ConfirmMessage = _localizationService.GetResource("common.error");
            }

            model = GetModelMyAccount(model);

            return View(model);
        }

        private MyAccountModel GetModelMyAccount(MyAccountModel model)
        {
            if (model == null)
                model = _workContext.CurrentCustomer.ToMyAccountModel();

            model.BikeReferences = model.BikeBrand.CategoryId.HasValue ? _categoryService.GetAllCategoriesByParentCategoryId(model.BikeBrand.CategoryId.Value) : new List<Category>();

            model.States = _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
            model.BikeBrands = _categoryService.GetAllCategoriesByParentCategoryId(_tuilsSettings.productBaseTypes_bike);

            //Intenta cargar la imagen de la marca solo si tiene seleccionada una
            if (model.BikeBrand.CategoryId.HasValue)
            {
                var categoryBrand = _categoryService.GetCategoryById(model.BikeBrand.CategoryId.Value);
                if (categoryBrand != null)
                    model.BikeBrand.Picture = categoryBrand.GetPicture(_localizationService, _mediaSettings, _pictureService);
            }

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
            if (_workContext.CurrentVendor != null && _workContext.CurrentVendor.Id > 0)
            {
                var model = new OfficesModel();
                model.States = _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
                model.Name = _workContext.CurrentVendor.Name;
                model.VendorSeName = _workContext.CurrentVendor.GetSeName();
                model.VendorId = _workContext.CurrentVendor.Id;
                return View(model);
            }
            else
                return InvokeHttp404();
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
                model.VendorSeName = _workContext.CurrentVendor.GetSeName();

                return View(model);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult VendorServices(VendorServicesModel model)
        {

            if (_workContext.CurrentVendor != null)
            {
                //Toma la cadena separada por comas y crea una lista de categorias relacinoadas
                List<SpecialCategoryVendor> bikeReferences;
                if (!string.IsNullOrEmpty(model.BikeReferencesString))
                {
                    bikeReferences = model.BikeReferencesString
                    .Split(new char[] { ',' })
                    .Select(c => new SpecialCategoryVendor()
                    {
                        CategoryId = Convert.ToInt32(c),
                        VendorId = _workContext.CurrentVendor.Id,
                        SpecialType = SpecialCategoryVendorType.BikeBrand
                    })
                    .ToList();
                }
                else
                {
                    bikeReferences = new List<SpecialCategoryVendor>();
                }

                List<SpecialCategoryVendor> specializedCategories;
                if (!string.IsNullOrEmpty(model.SpecializedCategoriesString))
                {
                    specializedCategories = model.SpecializedCategoriesString
                    .Split(new char[] { ',' })
                    .Select(c => new SpecialCategoryVendor() { CategoryId = Convert.ToInt32(c), VendorId = _workContext.CurrentVendor.Id, SpecialType = SpecialCategoryVendorType.SpecializedCategory })
                    .ToList();
                }
                else
                {
                    specializedCategories = new List<SpecialCategoryVendor>();
                }

                //Concatena las dos listas anteriores y las envía a ser actualizadas
                _vendorService.InsertUpdateVendorSpecialCategories(_workContext.CurrentVendor.Id, bikeReferences.Concat(specializedCategories).ToList());

                model.ConfirmMessage = _localizationService.GetResource("VendorServices.Confirm");

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
            var model = new MyOrdersModel() { VendorId = _workContext.CurrentVendor.Id };

            //configura el paginador
            PreparePageSizeOptions(model.PagingFilteringContext, command);

            //bool? publishedProducts = null;
            //bool? withRating = null;

            //switch (command.Filter)
            //{
            //    case "rating":
            //        withRating = true;
            //        model.ResorceMessageNoRows = string.Format("{0}.NoRows.Rating", isMyOrders ? "MyOrders": "MySales");
            //        break;
            //    case "norating":
            //        withRating = false;
            //        model.ResorceMessageNoRows = string.Format("{0}.NoRows.NoRating", isMyOrders ? "MyOrders": "MySales");
            //        break;
            //    case "active":
            //        publishedProducts = true;
            //        model.ResorceMessageNoRows = string.Format("{0}.NoRows.Active", isMyOrders ? "MyOrders" : "MySales");
            //        break;
            //    default:
            //        model.ResorceMessageNoRows = string.Format("{0}.NoRows.General", isMyOrders ? "MyOrders" : "MySales");
            //        break;
            //}


            //IPagedList<Order> orders = null;
            //if (isMyOrders)
            //    orders = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id,
            //        customerId: _workContext.CurrentCustomer.Id, pageIndex: command.PageIndex, pageSize: command.PageSize,
            //        publishedProducts: publishedProducts, withRating: withRating);
            //else
            //    orders = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id,
            //        vendorId: _workContext.CurrentVendor.Id, pageIndex: command.PageIndex, pageSize: command.PageSize,
            //        publishedProducts: publishedProducts, withRating: withRating);

            var orders = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id,
                customerId: _workContext.CurrentCustomer.Id, pageIndex: command.PageIndex, pageSize: command.PageSize);


            foreach (var order in orders)
            {
                var orderModel = new OrderItemModel
                {
                    Id = order.Id,
                    CreatedOn = order.CreatedOnUtc.ToShortDateString(),
                    PlanExpirationOnUtc = order.PlanExpirationOnUtc.HasValue ? order.PlanExpirationOnUtc.Value.ToShortDateString() : string.Empty,
                    PlanStartOnUtc = order.PlanStartOnUtc.HasValue ? order.PlanStartOnUtc.Value.ToShortDateString() : string.Empty,
                    PaymentStatus = order.PaymentStatus.GetLocalizedEnum(_localizationService, _workContext)
                };

                //Si la orden fue completada y la fecha de expiracion es menor que la actual: Cambia el nombre del estado
                if (order.OrderStatus == OrderStatus.Complete && order.PlanExpirationOnUtc.HasValue && order.PlanExpirationOnUtc.Value < DateTime.UtcNow)
                    orderModel.OrderStatus = _localizationService.GetResource("Myorders.Expired");
                else
                    orderModel.OrderStatus = order.OrderStatus.GetLocalizedEnum(_localizationService, _workContext);

                var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
                orderModel.Price = _priceFormatter.FormatPrice(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, _workContext.WorkingLanguage);

                if (order.OrderItems.Count > 0)
                {
                    var item = order.OrderItems.FirstOrDefault();

                    //Consulta el review de una orden
                    //var review = _productService.GetAllProductReviews(orderItemId: item.Id, approved: isMyOrders ? (bool?)null : true).FirstOrDefault();
                    //if (review != null)
                    //{
                    //    orderModel.Rating = review.Rating;
                    //    orderModel.RatingApproved = review.IsApproved;
                    //    //Solo muestra la calificacion si ya fue aprobada
                    //    //Con excepción que si son "Mis Compras" si muesta que ya fue calificado
                    //    orderModel.ShowRating = (review.IsApproved && !isMyOrders) || isMyOrders;
                    //}

                    orderModel.Product = new Models.Catalog.ProductOverviewModel()
                    {
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
                            ImageUrl = _pictureService.GetPictureUrl(picture, pictureSize, crop: true),
                            FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                            Title = string.Format(_localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), orderModel.Product.Name),
                            AlternateText = string.Format(_localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), orderModel.Product.Name)
                        };
                        return pictureModel;
                    });
                    #endregion

                    #region Anterior Logica
                    //if (isMyOrders)
                    //    orderModel.Vendor = new Models.Catalog.VendorModel()
                    //    {
                    //        Id = item.Product.VendorId,
                    //        Name = item.Product.Vendor.Name,
                    //        SeName = item.Product.Vendor.GetSeName()
                    //    };
                    //else
                    //{
                    //    orderModel.Customer = new CustomerInfoModel()
                    //    {
                    //        FirstName = order.Customer.GetAttribute<string>("FirstName"),
                    //        LastName = order.Customer.GetAttribute<string>("LastName"),
                    //        Phone = order.Customer.GetAttribute<string>("Phone"),
                    //        Email = order.Customer.Email
                    //    };
                    //}
                    #endregion
                   

                }
                else
                {
                    throw new NopException("La orden no tiene productos asociados");
                }

                model.Orders.Add(orderModel);
            }

            #region CurrentPlan
            //Carga los datos del plan actual del usuario si no lo tiene vencido
            if (_workContext.CurrentVendor != null && _workContext.CurrentVendor.CurrentOrderPlan != null && _workContext.CurrentVendor.PlanExpiredOnUtc > DateTime.Now)
            {
                model.ShowCurrentPlan = true;
                
                var currentOrder = _workContext.CurrentVendor.CurrentOrderPlan;
                var selectedPlan = currentOrder.OrderItems.FirstOrDefault().Product;

                model.CurrentPlan.NumDaysToExpirePlan = Convert.ToInt32(_workContext.CurrentVendor.PlanExpiredOnUtc.Value.Subtract(DateTime.UtcNow).TotalDays);
                model.CurrentPlan.ShowRenovateButton = _workContext.CurrentVendor.PlanExpiredOnUtc < DateTime.UtcNow.AddDays(10);
                model.CurrentPlan.ShowUpgradeButton = true;
                

                //Carga los datos basicos de la orden
                model.CurrentPlan.Order = new OrderItemModel
                {
                    Id = currentOrder.Id,
                    CreatedOn = currentOrder.CreatedOnUtc.ToShortDateString(),
                    PlanExpirationOnUtc = _workContext.CurrentVendor.PlanExpiredOnUtc.Value.ToShortDateString(),
                    PlanStartOnUtc = currentOrder.PlanStartOnUtc.HasValue ? currentOrder.PlanStartOnUtc.Value.ToShortDateString() : string.Empty,
                    PaymentStatus = currentOrder.PaymentStatus.GetLocalizedEnum(_localizationService, _workContext)
                };

                //Carga los dats del producto
                model.CurrentPlan.Order.Product = new Models.Catalog.ProductOverviewModel()
                {
                    Id = selectedPlan.Id,
                    Name = selectedPlan.Name
                };

                var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(currentOrder.OrderTotal, currentOrder.CurrencyRate);
                model.CurrentPlan.Order.Price = _priceFormatter.FormatPrice(orderTotalInCustomerCurrency, true, currentOrder.CustomerCurrencyCode, false, _workContext.WorkingLanguage);

                var leftProductsOnPlan = _productService.CountLeftFeaturedPlacesByVendor(currentOrder, _workContext.CurrentVendor.Id);
                //Carga los datos de los productos que han sido destacados en el plan
                if (leftProductsOnPlan.ContainsKey(_planSettings.SpecificationAttributeIdProductsOnHomePage))
                {
                    model.CurrentPlan.NumProductsOnHomeLeft = leftProductsOnPlan[_planSettings.SpecificationAttributeIdProductsOnHomePage][0];
                    model.CurrentPlan.NumProductsOnHomeByPlan = leftProductsOnPlan[_planSettings.SpecificationAttributeIdProductsOnHomePage][1];
                }
                //valida productos en redes sociales
                if (leftProductsOnPlan.ContainsKey(_planSettings.SpecificationAttributeIdProductsOnSocialNetworks))
                {
                    model.CurrentPlan.NumProductsOnSocialNetworksLeft = leftProductsOnPlan[_planSettings.SpecificationAttributeIdProductsOnSocialNetworks][0];
                    model.CurrentPlan.NumProductsOnSocialNetworksByPlan = leftProductsOnPlan[_planSettings.SpecificationAttributeIdProductsOnSocialNetworks][1];
                }
                //Valida productos en sliders
                if (leftProductsOnPlan.ContainsKey(_planSettings.SpecificationAttributeIdProductsFeaturedOnSliders))
                {
                    model.CurrentPlan.NumProductsOnSlidersLeft = leftProductsOnPlan[_planSettings.SpecificationAttributeIdProductsFeaturedOnSliders][0];
                    model.CurrentPlan.NumProductsOnSlidersByPlan = leftProductsOnPlan[_planSettings.SpecificationAttributeIdProductsFeaturedOnSliders][1];
                }
                //Valida productos
                if (leftProductsOnPlan.ContainsKey(_planSettings.SpecificationAttributeIdLimitProducts))
                {
                    model.CurrentPlan.NumProductsLeft = leftProductsOnPlan[_planSettings.SpecificationAttributeIdLimitProducts][0];
                    model.CurrentPlan.NumProductsByPlan = leftProductsOnPlan[_planSettings.SpecificationAttributeIdLimitProducts][1];
                }
            }
            #endregion
            


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

        #region MyProducts
        public ActionResult MyProducts(MyOrdersPagingFilteringModel command)
        {
            if (_workContext.CurrentVendor != null)
            {
                var model = new MyProductsModel();
                //Carga los tamaños de la paginación
                PreparePageSizeOptions(model.PagingFilteringContext, command);

                bool showPublished = command.p.HasValue ? command.p.Value : true;

                string keywordsSearch = !string.IsNullOrWhiteSpace(command.q) ? command.q : null;

                IList<int> categoriesIds = null;
                if (command.pt.HasValue)
                    categoriesIds = GetChildCategoryIds(command.pt.Value);

                var products = _productService.SearchProducts(showHidden: !showPublished, categoryIds: categoriesIds, vendorId: _workContext.CurrentVendor.Id,
                    pageSize: command.PageSize, pageIndex: command.PageIndex, keywords: keywordsSearch, 
                    orderBy: ProductSortingEnum.UpdatedOn, published: showPublished);

                model.Products = products.Select(p => new ProductOverviewModel()
                {
                    Id = p.Id,
                    Visits = p.Visits,
                    Name = p.Name,
                    SeName = p.GetSeName(),
                    UnansweredQuestions = p.UnansweredQuestions,
                    ApprovedTotalReviews = p.ApprovedTotalReviews,
                    TotalSales = p.TotalSales,
                    AvailableStartDate = p.AvailableStartDateTimeUtc ?? DateTime.UtcNow,
                    AvailableEndDate = p.AvailableEndDateTimeUtc ?? DateTime.UtcNow,
                    Published = p.IsTotallyAvailable(),
                    DefaultPictureModel = p.GetPicture(_localizationService, _mediaSettings, _pictureService),
                    NumClicksForMoreInfo = p.NumClicksForMoreInfo,
                    HasPlanSelected = p.OrderPlanId.HasValue
                }).ToList();

                model.PagingFilteringContext.q = command.q;
                model.PagingFilteringContext.LoadPagedList(products);

                //Carga la cadena de respuesta cuando no  hay resultados del filtro
                model.ResorceMessageNoRows = string.Format("MyProducts.NoRows.{0}", showPublished ? "Active" : "Inactive");
                //Solo puede mostrar el botón de los planes si es un vendor
                model.ShowButtonFeatureByPlan = _workContext.CurrentVendor.VendorType == VendorType.Market;

                string url = _webHelper.GetThisPageUrl(true);
                model.UrlFilterByServices = new MyProductsModel.LinkFilter()
                {
                    Url = _webHelper.ModifyQueryString(url, "pt=" + _tuilsSettings.productBaseTypes_service, null),
                    Active = command.pt.HasValue && command.pt.Value == _tuilsSettings.productBaseTypes_service
                };
                model.UrlFilterByBikes = new MyProductsModel.LinkFilter()
                {
                    Url = _webHelper.ModifyQueryString(url, "pt=" + _tuilsSettings.productBaseTypes_bike, null),
                    Active = command.pt.HasValue && command.pt.Value == _tuilsSettings.productBaseTypes_bike
                };
                model.UrlFilterByProducts = new MyProductsModel.LinkFilter()
                {
                    Url = _webHelper.ModifyQueryString(url, "pt=" + _tuilsSettings.productBaseTypes_product, null),
                    Active = command.pt.HasValue && command.pt.Value == _tuilsSettings.productBaseTypes_product
                };

                return View(model);
            }
            else
                return InvokeHttp404();
        }


        [NonAction]
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
               .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_CHILD_IDENTIFIERS_MODEL_KEY, parentCategoryId, string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
            return _cacheManager.Get(cacheKey, () =>
            {
                return _categoryService.GetChildCategoryIds(parentCategoryId);
            });
        }




        #endregion

        #region EditProductControlPanel
        public ActionResult EditProduct(int id)
        {

            //Consulta el producto que se desea editar
            var product = _productService.GetProductById(id);

            if (product == null || product.Deleted)
                return InvokeHttp404();

            //Is published?
            //Check whether the current user has a "Manage catalog" permission
            //It allows him to preview a product before publishing
            if (!product.Published && !_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return InvokeHttp404();

            //Si el vendedor del producto que se desea editar no es el mismo, lo saca
            if (product.VendorId != _workContext.CurrentVendor.Id)
                return InvokeHttp404();

            var model = PrepareEditProductModel(product);

            return View(model);
        }

        [NonAction]
        private EditProductModel PrepareEditProductModel(Product product)
        {
            var model = new EditProductModel();
            model.Name = product.Name;
            model.ShortDescription = product.ShortDescription;
            model.Price = product.Price;
            return model;
        }
        #endregion

        #region Questions
        public ActionResult Questions(QuestionsPaginFilteringModel command)
        {
            //Valida que exista un vendedor en sesion
            if (_workContext.CurrentVendor != null)
            {
                var model = new QuestionsModel();

                //si el filtro es por producto
                if (command.p > 0)
                {
                    model.Questions = _productService.GetProductQuestions(productId: command.p, status: QuestionStatus.Created).ToModels(_dateTimeHelper);
                    //Valida que el vendedor en sesion sea el correspondiente al producto
                    var product = _productService.GetProductById(command.p);
                    if (product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        return InvokeHttp404();
                    }
                }
                else
                    model.Questions = _productService.GetProductQuestions(vendorId: _workContext.CurrentVendor.Id, status: QuestionStatus.Created).ToModels(_dateTimeHelper);

                return View(model);
            }
            else
            {
                return InvokeHttp404();
            }
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
            model.IsMobileDevice = Request.Browser.IsMobileDevice;

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
            string currentAction = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString().ToLower();
            string currentController = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString().ToLower();
            foreach (var module in modules)
            {
                //Valida que todas las llaves del módulo sean iguales al querystring
                Func<System.Collections.Specialized.NameValueCollection, object, object, bool> validateQueryString = delegate(System.Collections.Specialized.NameValueCollection queryString, object routeValuesModule, object routeValuesModuleOptional)
                {
                    if (routeValuesModule != null)
                    {
                        //Recorre todas las llaves que tiene el modulo como parametros OBLIGATORIOS
                        foreach (var property in routeValuesModule.GetType().GetProperties())
                        {
                            var valueParam = property.GetValue(routeValuesModule).ToString().ToLower();
                            //Si la variable de querystring es nula o es diferente al valor que debe tener esa propiedad retorna false
                            if (queryString[property.Name] == null || !queryString[property.Name].ToLower().Equals(valueParam))
                            {
                                return false;
                            }
                        }
                    }

                    if (routeValuesModuleOptional != null)
                    {
                        //Recorre todas las llaves que tiene el modulo como parametros OPCIONALES
                        foreach (var property in routeValuesModuleOptional.GetType().GetProperties())
                        {
                            var valueParam = property.GetValue(routeValuesModuleOptional).ToString().ToLower();
                            //Si la variable de querystring es diferente al valor que debe tener esa propiedad retorna false
                            //Si la variable llega a ser null no retorna False ya que esta es opcional
                            if (queryString[property.Name] != null && !queryString[property.Name].ToLower().Equals(valueParam))
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                };

                parent = module.Name;
                //Valida que no tenga submodulos activos, que corresponda a la misma acción y que los parametros adicionales coincidan todos
                var queryStringParent = ControllerContext.ParentActionViewContext.RequestContext.HttpContext.Request.QueryString;
                if (module.SubModules.Count == 0
                    && module.Action.Equals(currentAction)
                    && module.Controller.Equals(currentController)
                    && validateQueryString(queryStringParent, module.Parameters, module.OptionalParameters))
                    return module.Name;
                else
                {

                    string subModuleName = string.Empty;
                    //Si no es de tipo padre recorre los submodulos
                    foreach (var sm in module.SubModules)
                    {
                        if (sm.Action.ToLower().Equals(currentAction) && sm.Controller.ToLower().Equals(currentController) && validateQueryString(queryStringParent, sm.Parameters, sm.OptionalParameters))
                            subModuleName = sm.Name;
                    }
                    //Si algún submodulo fue encontrado lo retorna
                    if (!string.IsNullOrEmpty(subModuleName)) return subModuleName;
                }



            }
            return string.Empty;
        }



        #endregion
    }
}