﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Topics;
using Nop.Services.Vendors;
using Nop.Web.Extensions;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Security;
using Nop.Web.Infrastructure.Cache;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Media;
using Nop.Services.Orders;
using Nop.Services.Customers;
using Nop.Core.Domain.Orders;
using Nop.Web.Framework;
using Nop.Web.Framework.UI.Captcha;
using Nop.Services.Messages;
using Nop.Services.Helpers;

namespace Nop.Web.Controllers
{
    public partial class CatalogController : BasePublicController
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;
        private readonly ICategoryTemplateService _categoryTemplateService;
        private readonly IManufacturerTemplateService _manufacturerTemplateService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ITaxService _taxService;
        private readonly ICurrencyService _currencyService;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IWebHelper _webHelper;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IProductTagService _productTagService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ITopicService _topicService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ISearchTermService _searchTermService;
        private readonly MediaSettings _mediaSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly VendorSettings _vendorSettings;
        private readonly BlogSettings _blogSettings;
        private readonly ForumSettings _forumSettings;
        private readonly TuilsSettings _tuilsSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IOrderService _orderService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IAddressService _addressService;
        private readonly PlanSettings _planSettings;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IDateTimeHelper _dateTimeHelper;


        #endregion

        #region Constructors

        public CatalogController(ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IVendorService vendorService,
            ICategoryTemplateService categoryTemplateService,
            IManufacturerTemplateService manufacturerTemplateService,
            IWorkContext workContext,
            IStoreContext storeContext,
            ITaxService taxService,
            ICurrencyService currencyService,
            IPictureService pictureService,
            ILocalizationService localizationService,
            IPriceCalculationService priceCalculationService,
            IPriceFormatter priceFormatter,
            IWebHelper webHelper,
            ISpecificationAttributeService specificationAttributeService,
            IProductTagService productTagService,
            IGenericAttributeService genericAttributeService,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IPermissionService permissionService,
            ICustomerActivityService customerActivityService,
            ITopicService topicService,
            IEventPublisher eventPublisher,
            ISearchTermService searchTermService,
            MediaSettings mediaSettings,
            CatalogSettings catalogSettings,
            VendorSettings vendorSettings,
            BlogSettings blogSettings,
            ForumSettings forumSettings,
            ICacheManager cacheManager,
            IStateProvinceService stateProvinceService,
            IOrderService orderService,
            TuilsSettings tuilsSettings,
            IOrderProcessingService orderProcessingService,
            PlanSettings planSettings,
            IAddressService addressService,
            IWorkflowMessageService workflowMessageService,
            IDateTimeHelper dateTimeHelper)
        {
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._productService = productService;
            this._vendorService = vendorService;
            this._categoryTemplateService = categoryTemplateService;
            this._manufacturerTemplateService = manufacturerTemplateService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._taxService = taxService;
            this._currencyService = currencyService;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._priceCalculationService = priceCalculationService;
            this._priceFormatter = priceFormatter;
            this._webHelper = webHelper;
            this._specificationAttributeService = specificationAttributeService;
            this._productTagService = productTagService;
            this._genericAttributeService = genericAttributeService;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._permissionService = permissionService;
            this._customerActivityService = customerActivityService;
            this._topicService = topicService;
            this._eventPublisher = eventPublisher;
            this._searchTermService = searchTermService;
            this._mediaSettings = mediaSettings;
            this._catalogSettings = catalogSettings;
            this._vendorSettings = vendorSettings;
            this._blogSettings = blogSettings;
            this._forumSettings = forumSettings;
            this._cacheManager = cacheManager;
            this._stateProvinceService = stateProvinceService;
            this._orderService = orderService;
            this._tuilsSettings = tuilsSettings;
            this._orderProcessingService = orderProcessingService;
            this._planSettings = planSettings;
            this._addressService = addressService;
            this._workflowMessageService = workflowMessageService;
            this._dateTimeHelper = dateTimeHelper;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareSortingOptions(CatalogPagingFilteringModel pagingFilteringModel, CatalogPagingFilteringModel command)
        {
            if (pagingFilteringModel == null)
                throw new ArgumentNullException("pagingFilteringModel");

            if (command == null)
                throw new ArgumentNullException("command");

            pagingFilteringModel.AllowProductSorting = _catalogSettings.AllowProductSorting;
            if (pagingFilteringModel.AllowProductSorting)
            {
                foreach (ProductSortingEnum enumValue in Enum.GetValues(typeof(ProductSortingEnum)))
                {
                    var currentPageUrl = _webHelper.GetThisPageUrl(true);
                    var sortUrl = _webHelper.ModifyQueryString(currentPageUrl, "orderby=" + ((int)enumValue).ToString(), null);

                    var sortValue = enumValue.GetLocalizedEnum(_localizationService, _workContext);
                    pagingFilteringModel.AvailableSortOptions.Add(new SelectListItem
                    {
                        Text = sortValue,
                        Value = sortUrl,
                        Selected = enumValue == (ProductSortingEnum)command.OrderBy
                    });
                }
            }
        }

        [NonAction]
        protected virtual void PrepareViewModes(CatalogPagingFilteringModel pagingFilteringModel, CatalogPagingFilteringModel command)
        {
            if (pagingFilteringModel == null)
                throw new ArgumentNullException("pagingFilteringModel");

            if (command == null)
                throw new ArgumentNullException("command");

            pagingFilteringModel.AllowProductViewModeChanging = _catalogSettings.AllowProductViewModeChanging;

            var viewMode = !string.IsNullOrEmpty(command.ViewMode)
                ? command.ViewMode
                : _catalogSettings.DefaultViewMode;
            pagingFilteringModel.ViewMode = viewMode;
            if (pagingFilteringModel.AllowProductViewModeChanging)
            {
                var currentPageUrl = _webHelper.GetThisPageUrl(true);
                //grid
                pagingFilteringModel.AvailableViewModes.Add(new SelectListItem
                {
                    Text = _localizationService.GetResource("Catalog.ViewMode.Grid"),
                    Value = _webHelper.ModifyQueryString(currentPageUrl, "viewmode=grid", null),
                    Selected = viewMode == "grid"
                });
                //list
                pagingFilteringModel.AvailableViewModes.Add(new SelectListItem
                {
                    Text = _localizationService.GetResource("Catalog.ViewMode.List"),
                    Value = _webHelper.ModifyQueryString(currentPageUrl, "viewmode=list", null),
                    Selected = viewMode == "list"
                });
            }

        }

        [NonAction]
        protected virtual void PreparePageSizeOptions(CatalogPagingFilteringModel pagingFilteringModel, CatalogPagingFilteringModel command,
            bool allowCustomersToSelectPageSize, string pageSizeOptions, int fixedPageSize)
        {
            if (pagingFilteringModel == null)
                throw new ArgumentNullException("pagingFilteringModel");

            if (command == null)
                throw new ArgumentNullException("command");

            if (command.PageNumber <= 0)
            {
                command.PageNumber = 1;
            }
            pagingFilteringModel.AllowCustomersToSelectPageSize = false;
            if (allowCustomersToSelectPageSize && pageSizeOptions != null)
            {
                var pageSizes = pageSizeOptions.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (pageSizes.Any())
                {
                    // get the first page size entry to use as the default (category page load) or if customer enters invalid value via query string
                    if (command.PageSize <= 0 || !pageSizes.Contains(command.PageSize.ToString()))
                    {
                        int temp;
                        if (int.TryParse(pageSizes.FirstOrDefault(), out temp))
                        {
                            if (temp > 0)
                            {
                                command.PageSize = temp;
                            }
                        }
                    }

                    var currentPageUrl = _webHelper.GetThisPageUrl(true);
                    var sortUrl = _webHelper.ModifyQueryString(currentPageUrl, "pagesize={0}", null);
                    sortUrl = _webHelper.RemoveQueryString(sortUrl, "pagenumber");

                    foreach (var pageSize in pageSizes)
                    {
                        int temp;
                        if (!int.TryParse(pageSize, out temp))
                        {
                            continue;
                        }
                        if (temp <= 0)
                        {
                            continue;
                        }

                        pagingFilteringModel.PageSizeOptions.Add(new SelectListItem
                        {
                            Text = pageSize,
                            Value = String.Format(sortUrl, pageSize),
                            Selected = pageSize.Equals(command.PageSize.ToString(), StringComparison.InvariantCultureIgnoreCase)
                        });
                    }

                    if (pagingFilteringModel.PageSizeOptions.Any())
                    {
                        pagingFilteringModel.PageSizeOptions = pagingFilteringModel.PageSizeOptions.OrderBy(x => int.Parse(x.Text)).ToList();
                        pagingFilteringModel.AllowCustomersToSelectPageSize = true;

                        if (command.PageSize <= 0)
                        {
                            command.PageSize = int.Parse(pagingFilteringModel.PageSizeOptions.FirstOrDefault().Text);
                        }
                    }
                }
            }
            else
            {
                //customer is not allowed to select a page size
                command.PageSize = fixedPageSize;
            }

            //ensure pge size is specified
            if (command.PageSize <= 0)
            {
                command.PageSize = fixedPageSize;
            }

            pagingFilteringModel.q = command.q;
        }

        [NonAction]
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
               .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_CHILD_IDENTIFIERS_MODEL_KEY, parentCategoryId, string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
            return _cacheManager.Get(cacheKey, () =>
            {
                #region Codigo eliminado
                //YA NO LO TOMA DE IR VARIAS VECES A BASE DE DATOS, SINO QUE BUSCA EN EL CAMPO CHILDRENCATEGORIESSTR
                //var categoriesIds = new List<int>();
                //var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId);
                //foreach (var category in categories)
                //{
                //    categoriesIds.Add(category.Id);
                //    categoriesIds.AddRange(GetChildCategoryIds(category.Id));
                //}
                //return categoriesIds;
                #endregion

                return _categoryService.GetChildCategoryIds(parentCategoryId);
            });
        }




        [NonAction]
        protected virtual IList<CategorySimpleModel> PrepareCategorySimpleModels(int rootCategoryId,
            IList<int> loadSubCategoriesForIds, int level, int levelsToLoad, bool validateIncludeInTopMenu)
        {
            var result = new List<CategorySimpleModel>();

            foreach (var category in _categoryService.GetAllCategoriesByParentCategoryId(rootCategoryId))
            {
                if (validateIncludeInTopMenu && !category.IncludeInTopMenu)
                {
                    continue;
                }

                var categoryModel = new CategorySimpleModel
                {
                    Id = category.Id,
                    Name = category.GetLocalized(x => x.Name),
                    SeName = category.GetSeName()
                };

                //product number for each category
                if (_catalogSettings.ShowCategoryProductNumber)
                {
                    var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                        .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                    string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_NUMBER_OF_PRODUCTS_MODEL_KEY,
                        string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id, category.Id);
                    categoryModel.NumberOfProducts = _cacheManager.Get(cacheKey, () =>
                    {
                        var categoryIds = new List<int>();
                        categoryIds.Add(category.Id);
                        //include subcategories
                        if (_catalogSettings.ShowCategoryProductNumberIncludingSubcategories)
                            categoryIds.AddRange(GetChildCategoryIds(category.Id));
                        return _productService.GetCategoryProductNumber(categoryIds, _storeContext.CurrentStore.Id);
                    });
                }

                //load subcategories?
                bool loadSubCategories = false;
                if (loadSubCategoriesForIds == null)
                {
                    //load all subcategories
                    loadSubCategories = true;
                }
                else
                {
                    //we load subcategories only for certain categories
                    for (int i = 0; i <= loadSubCategoriesForIds.Count - 1; i++)
                    {
                        if (loadSubCategoriesForIds[i] == category.Id)
                        {
                            loadSubCategories = true;
                            break;
                        }
                    }
                }
                if (levelsToLoad <= level)
                {
                    loadSubCategories = false;
                }
                if (loadSubCategories)
                {
                    var subCategories = PrepareCategorySimpleModels(category.Id, loadSubCategoriesForIds, level + 1, levelsToLoad, validateIncludeInTopMenu);
                    categoryModel.SubCategories.AddRange(subCategories);
                }
                result.Add(categoryModel);
            }



            return result;
        }

        [NonAction]
        protected virtual IEnumerable<ProductOverviewModel> PrepareProductOverviewModels(IEnumerable<Product> products,
            bool preparePriceModel = true, bool preparePictureModel = true,
            int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
            bool forceRedirectionAfterAddingToCart = false, bool prepareManufacturer = false, string utm_source = null, string utm_medium = null, string utm_campaign = null)
        {
            return this.PrepareProductOverviewModels(_workContext,
                _storeContext, _categoryService, _productService, _specificationAttributeService,
                _priceCalculationService, _priceFormatter, _permissionService,
                _localizationService, _taxService, _currencyService,
                _pictureService, _webHelper, _cacheManager, _stateProvinceService,
                _catalogSettings, _mediaSettings, products,
                preparePriceModel, preparePictureModel,
                productThumbPictureSize, prepareSpecificationAttributes,
                forceRedirectionAfterAddingToCart, prepareManufacturer, utm_source, utm_medium, utm_campaign);
        }

        #endregion

        #region Categories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="specFilterId">Es diferente de null si por en la URL viene el nombre del atributo por el que debe filtrar</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Category(int categoryId, CatalogPagingFilteringModel command)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null || category.Deleted)
                return InvokeHttp404();

            //Check whether the current user has a "Manage catalog" permission
            //It allows him to preview a category before publishing
            if (!category.Published && !_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
                return InvokeHttp404();

            //ACL (access control list)
            if (!_aclService.Authorize(category))
                return InvokeHttp404();

            //Store mapping
            if (!_storeMappingService.Authorize(category))
                return InvokeHttp404();

            //'Continue shopping' URL
            _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                SystemCustomerAttributeNames.LastContinueShoppingPage,
                _webHelper.GetThisPageUrl(false),
                _storeContext.CurrentStore.Id);

            var model = category.ToModel();




            //sorting
            PrepareSortingOptions(model.PagingFilteringContext, command);
            //view mode
            PrepareViewModes(model.PagingFilteringContext, command);
            //page size
            PreparePageSizeOptions(model.PagingFilteringContext, command,
                category.AllowCustomersToSelectPageSize,
                category.PageSizeOptions,
                category.PageSize);



            //category breadcrumb
            model.DisplayCategoryBreadcrumb = _catalogSettings.CategoryBreadcrumbEnabled;
            if (model.DisplayCategoryBreadcrumb)
            {
                foreach (var catBr in category.GetCategoryBreadCrumb(_categoryService, _aclService, _storeMappingService))
                {
                    model.CategoryBreadcrumb.Add(new CategoryModel
                    {
                        Id = catBr.Id,
                        Name = catBr.GetLocalized(x => x.Name),
                        SeName = catBr.GetSeName()
                    });
                }
            }



            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();



            //subcategories
            string subCategoriesCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_SUBCATEGORIES_KEY,
                categoryId,
                string.Join(",", customerRolesIds),
                _storeContext.CurrentStore.Id,
                _workContext.WorkingLanguage.Id,
                _webHelper.IsCurrentConnectionSecured());
            model.SubCategories = _cacheManager.Get(subCategoriesCacheKey, () =>
                _categoryService.GetAllCategoriesByParentCategoryId(categoryId)
                .Select(x =>
                {
                    var subCatModel = new CategoryModel.SubCategoryModel
                    {
                        Id = x.Id,
                        Name = x.GetLocalized(y => y.Name),
                        SeName = x.GetSeName(),
                    };

                    //prepare picture model
                    int pictureSize = _mediaSettings.CategoryThumbPictureSize;
                    var categoryPictureCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_PICTURE_MODEL_KEY, x.Id, pictureSize, true, _workContext.WorkingLanguage.Id, _webHelper.IsCurrentConnectionSecured(), _storeContext.CurrentStore.Id);
                    subCatModel.PictureModel = _cacheManager.Get(categoryPictureCacheKey, () =>
                    {
                        var picture = _pictureService.GetPictureById(x.PictureId);
                        var pictureModel = new PictureModel
                        {
                            FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                            ImageUrl = _pictureService.GetPictureUrl(picture, pictureSize, crop: true),
                            Title = string.Format(_localizationService.GetResource("Media.Category.ImageLinkTitleFormat"), subCatModel.Name),
                            AlternateText = string.Format(_localizationService.GetResource("Media.Category.ImageAlternateTextFormat"), subCatModel.Name)
                        };
                        return pictureModel;
                    });

                    return subCatModel;
                })
                .ToList()
            );




            //featured products
            if (!_catalogSettings.IgnoreFeaturedProducts)
            {
                //We cache a value indicating whether we have featured products
                IPagedList<Product> featuredProducts = null;
                string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_HAS_FEATURED_PRODUCTS_KEY, categoryId,
                    string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
                var hasFeaturedProductsCache = _cacheManager.Get<bool?>(cacheKey);
                if (!hasFeaturedProductsCache.HasValue)
                {
                    //no value in the cache yet
                    //let's load products and cache the result (true/false)
                    featuredProducts = _productService.SearchProducts(
                       categoryIds: new List<int> { category.Id },
                       storeId: _storeContext.CurrentStore.Id,
                       visibleIndividuallyOnly: true,
                       featuredProducts: true);
                    hasFeaturedProductsCache = featuredProducts.TotalCount > 0;
                    _cacheManager.Set(cacheKey, hasFeaturedProductsCache, 60);
                }
                if (hasFeaturedProductsCache.Value && featuredProducts == null)
                {
                    //cache indicates that the category has featured products
                    //let's load them
                    featuredProducts = _productService.SearchProducts(
                       categoryIds: new List<int> { category.Id },
                       storeId: _storeContext.CurrentStore.Id,
                       visibleIndividuallyOnly: true,
                       featuredProducts: true);
                }
                if (featuredProducts != null)
                {
                    model.FeaturedProducts = PrepareProductOverviewModels(featuredProducts).ToList();
                }
            }


            decimal? minPriceConverted = null;
            decimal? maxPriceConverted = null;
            //Valida el rango de precios
            var priceFilter = command.PriceRangeFilter.GetSelectedPriceRange(_webHelper, null);
            if (priceFilter != null)
            {
                minPriceConverted = priceFilter.From;
                maxPriceConverted = priceFilter.To;
            }


            //products
            IList<int> alreadyFilteredSpecOptionIds = model.PagingFilteringContext.SpecificationFilter.GetAlreadyFilteredIds(_webHelper);
            //Categorias seleccionadas
            IList<int> alreadyFilteredCategoryIds = model.PagingFilteringContext.CategoryFilter.GetAlreadyFilteredIds(_webHelper);
            //Ciudad seleccionada
            int? stateProvinceId = model.PagingFilteringContext.StateProvinceFilter.GetAlreadyFilteredId(_webHelper);
            //Marca seleccionada
            int? manufacturerId = model.PagingFilteringContext.ManufacturerFilter.GetAlreadyFilteredId(_webHelper);
            //Categoria especial seleccionada (Marca de la moto)
            int? specialCategoryId = model.PagingFilteringContext.BikeReferenceFilter.GetAlreadyFilteredId(_webHelper);

            //Ya que la pagina es de una categoría, muestra adicionalmente todas las categorías hijas que
            //tienen productos en el filtro. Y toma el valor.
            int? newFilterCategory = model.PagingFilteringContext.CategoryFilter.GetAlreadyFilteredId(_webHelper);

            //Agrega la categoría nueva si viene filtrada, sino agrega la Padre
            var categoryIds = new List<int>();
            categoryIds.Add(newFilterCategory ?? category.Id);

            if (_catalogSettings.ShowProductsFromSubcategories)
            {
                //include subcategories
                categoryIds.AddRange(GetChildCategoryIds(newFilterCategory ?? category.Id));
            }


            Dictionary<int, int> filterableSpecificationAttributeOptionIds;
            Dictionary<int, int> filterableCategoryIds;
            Dictionary<int, int> filterableStateProvinceIds;
            Dictionary<int, int> filterableManufacturerIds;
            Dictionary<int, int> filterableSpecialCategoryIds;
            Tuple<int, int> minMaxPrice;


            var products = _productService.SearchProducts(
                out filterableSpecificationAttributeOptionIds,
                out filterableCategoryIds,
                out filterableStateProvinceIds,
                out filterableManufacturerIds,
                out filterableSpecialCategoryIds,
                out minMaxPrice,
                true,
                loadFilterableCategoryIds: true,  //Realiza busqueda de categorías porque posiblemente puede estar en una categoría hija
                loadFilterableStateProvinceIds: !stateProvinceId.HasValue, //Solo realiza conteo de ciudaddes si no está filtrando por ciudad,
                loadPriceRange: !minPriceConverted.HasValue && !maxPriceConverted.HasValue, //Solo realiza conteo de precios si no está filtrando por precio
                loadFilterableManufacturerIds: !manufacturerId.HasValue, //Solo realiza conteo de marcas si no está filtrando por marca
                loadFilterableSpecialCategoryIds: !specialCategoryId.HasValue,//Solo realiza connteo de categorias especiales si no está filtrado por referencia
                categoryIds: categoryIds,
                storeId: _storeContext.CurrentStore.Id,
                visibleIndividuallyOnly: true,
                featuredProducts: _catalogSettings.IncludeFeaturedProductsInNormalLists ? null : (bool?)false,
                priceMin: minPriceConverted,
                priceMax: maxPriceConverted,
                filteredSpecs: alreadyFilteredSpecOptionIds,
                orderBy: (ProductSortingEnum)command.OrderBy,
                pageIndex: command.PageNumber - 1,
                pageSize: command.PageSize,
                manufacturerId: manufacturerId ?? 0,
                stateProvinceId: stateProvinceId,
                specialCategoryId: specialCategoryId);
            model.Products = PrepareProductOverviewModels(products).ToList();

            model.PagingFilteringContext.LoadPagedList(products);

            //Price
            model.PagingFilteringContext.PriceRangeFilter.LoadPriceRangeFilters(minMaxPrice.Item1, minMaxPrice.Item2, _webHelper, _priceFormatter);

            //specs
            model.PagingFilteringContext.SpecificationFilter.PrepareSpecsFilters(alreadyFilteredSpecOptionIds,
        filterableSpecificationAttributeOptionIds,
        _specificationAttributeService, _webHelper, _workContext, true);

            //categories
            model.PagingFilteringContext.CategoryFilter.PrepareCategoriesFilters(alreadyFilteredCategoryIds,
        filterableCategoryIds,
        _categoryService, _webHelper, _workContext, true);

            //state provinces
            model.PagingFilteringContext.StateProvinceFilter.PrepareStateProvinceFilters(stateProvinceId,
        filterableStateProvinceIds,
        _stateProvinceService, _webHelper, _workContext, true);

            //manufacturer
            model.PagingFilteringContext.ManufacturerFilter.PrepareFilters(manufacturerId,
        filterableManufacturerIds,
        _manufacturerService, _webHelper, _workContext, true);


            //bike references
            model.PagingFilteringContext.BikeReferenceFilter.PrepareFilters(specialCategoryId,
        filterableSpecialCategoryIds,
        _categoryService, _webHelper, _workContext);

            //template
            var templateCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_TEMPLATE_MODEL_KEY, category.CategoryTemplateId);
            var templateViewPath = _cacheManager.Get(templateCacheKey, () =>
                {
                    var template = _categoryTemplateService.GetCategoryTemplateById(category.CategoryTemplateId);
                    if (template == null)
                        template = _categoryTemplateService.GetAllCategoryTemplates().FirstOrDefault();
                    if (template == null)
                        throw new Exception("No default template could be loaded");
                    return template.ViewPath;
                });

            //activity log
            _customerActivityService.InsertActivity("PublicStore.ViewCategory", _localizationService.GetResource("ActivityLog.PublicStore.ViewCategory"), category.Name);

            model.IsMobileDevice = Request.Browser.IsMobileDevice;


            string filtersUrl = GetTitleParts(model.PagingFilteringContext);

            if (!string.IsNullOrEmpty(model.MetaTitle))
                model.MetaTitle = string.Format(model.MetaTitle, filtersUrl);
            else
                model.MetaTitle = string.Format(_localizationService.GetResource("PageTitle.Search"), model.Name, filtersUrl);

            if (string.IsNullOrEmpty(model.MetaDescription))
                model.MetaDescription = string.Format(_localizationService.GetResource("Category.DefaultMetadescription"), model.Name, filtersUrl);

            return View(templateViewPath, model);
        }

        [NonAction]
        /// <summary>
        /// Genera un string que va ser complemento del titulo en las consultas
        /// </summary>
        /// <returns></returns>
        public string GetTitleParts(CatalogPagingFilteringModel command)
        {
            string stateProvincePart = string.Empty;
            if (command.StateProvinceFilter.FilteredItem != null)
            {
                stateProvincePart = string.Format(" en {0}", command.StateProvinceFilter.FilteredItem.Name);
            }

            string categoryPart = string.Empty;
            if (command.CategoryFilter.AlreadyFilteredItems != null && command.CategoryFilter.AlreadyFilteredItems.Count > 0)
            {
                categoryPart = string.Format(" {0}", command.CategoryFilter.AlreadyFilteredItems.FirstOrDefault().Name);
            }

            string manufacturerPart = string.Empty;
            if (command.ManufacturerFilter.FilteredItem != null)
            {
                manufacturerPart = string.Format(" marca {0}", command.ManufacturerFilter.FilteredItem.Name);
            }

            return string.Concat(categoryPart, manufacturerPart, stateProvincePart);
        }

        [ChildActionOnly]
        public ActionResult CategoryNavigation(int currentCategoryId, int currentProductId)
        {
            //get active category
            int activeCategoryId = 0;
            if (currentCategoryId > 0)
            {
                //category details page
                activeCategoryId = currentCategoryId;
            }
            else if (currentProductId > 0)
            {
                //product details page
                var productCategories = _categoryService.GetProductCategoriesByProductId(currentProductId);
                if (productCategories.Count > 0)
                    activeCategoryId = productCategories[0].CategoryId;
            }

            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_NAVIGATION_MODEL_KEY, _workContext.WorkingLanguage.Id,
                string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id, activeCategoryId);
            var cachedModel = _cacheManager.Get(cacheKey, () =>
            {
                if (_catalogSettings.LoadAllSideCategoryMenuSubcategories)
                {
                    return PrepareCategorySimpleModels(0, null, 0, int.MaxValue, false).ToList();
                }

                var activeCategory = _categoryService.GetCategoryById(activeCategoryId);
                var breadCrumb = activeCategory != null
                    ? activeCategory.GetCategoryBreadCrumb(_categoryService, _aclService, _storeMappingService).Select(x => x.Id).ToList()
                    : new List<int>();
                return PrepareCategorySimpleModels(0, breadCrumb, 0, int.MaxValue, false).ToList();
            });

            var model = new CategoryNavigationModel
            {
                CurrentCategoryId = activeCategoryId,
                Categories = cachedModel
            };

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {

            #region Codigo eliminado
            //categories


            //////top menu topics
            //string topicCacheKey = string.Format(ModelCacheEventConsumer.TOPIC_TOP_MENU_MODEL_KEY,
            //    _workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id);
            //var cachedTopicModel = _cacheManager.Get(topicCacheKey, () =>
            //    _topicService.GetAllTopics(_storeContext.CurrentStore.Id)
            //    .Where(t => t.IncludeInTopMenu)
            //    .Select(t => new TopMenuModel.TopMenuTopicModel
            //    {
            //        Id = t.Id,
            //        Name = t.GetLocalized(x => x.Title),
            //        SeName = t.GetSeName()
            //    })
            //    .ToList()
            //);


            #endregion


            return PartialView(new TopMenuModel());
        }

        #region TopNavigation Menu ELIMINADO
        ////////[ChildActionOnly]
        ////////public ActionResult TopMenuNavigation()
        ////////{
        ////////    var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
        ////////        .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

        ////////    //Carga todas las categorias que van en el menú principal
        ////////    string categoryCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_MENU_MODEL_KEY, _workContext.WorkingLanguage.Id,
        ////////        string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
        ////////    var cachedCategoriesModel = _cacheManager.Get(categoryCacheKey, () =>
        ////////        PrepareCategoryTopMenuSimpleModels().ToList()
        ////////    );

        ////////    //Consulta los atributos que sirven como filro para las categorias en el home
        ////////    string attributesCacheKey = string.Format(ModelCacheEventConsumer.TOPIC_TOP_MENU_ATTRIBUTES_KEY);
        ////////    var cachedMenuAttributes = _cacheManager.Get(attributesCacheKey, () =>
        ////////    {
        ////////        var attributes = new List<TopMenuModel.SpecificationAttributeOptionModel>();
        ////////        foreach (var attribute in _specificationAttributeService
        ////////        .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeBikeType))
        ////////        {
        ////////            attributes.Add(new TopMenuModel.SpecificationAttributeOptionModel()
        ////////            {
        ////////                Id = attribute.Id,
        ////////                Name = attribute.Name,
        ////////                SeName = attribute.GetSeName(_workContext.WorkingLanguage.Id, ensureTwoPublishedLanguages: false)
        ////////            });
        ////////        }
        ////////        return attributes;
        ////////    }

        ////////    );

        ////////    int idSelectedAttribute = 0;
        ////////    //Valida en que categoría se encuentra para así seleccionar el attributo especificado en el menú
        ////////    if (RouteData.Values["specsFilter"] != null)
        ////////    {
        ////////        var attributeSelected = cachedMenuAttributes.FirstOrDefault(a => a.SeName.Equals(RouteData.Values["specsFilter"]));
        ////////        idSelectedAttribute = attributeSelected != null ? attributeSelected.Id : 0;
        ////////    }

        ////////    var customer = _workContext.CurrentCustomer;
        ////////    var model = new TopMenuModel
        ////////    {
        ////////        Categories = cachedCategoriesModel,
        ////////        Topics = new List<TopMenuModel.TopMenuTopicModel>(),
        ////////        SpecificationAttributesFilter = cachedMenuAttributes,
        ////////        //Si no viene filtrado por atributo tipo moto, selecciona la primera de la lista
        ////////        SelectedSpecificationAttribute = idSelectedAttribute > 0 ? idSelectedAttribute : _catalogSettings.DefaultSpecificationAttributeTopMenu,
        ////////        SelectedCategory = RouteData.Values["categoryId"] != null ? (int)RouteData.Values["categoryId"] : 0,
        ////////        IsAuthenticated = customer.IsRegistered(),
        ////////        CustomerEmailUsername = customer.IsRegistered() ? (customer.GetFullName()) : "",
        ////////        WishlistEnabled = _permissionService.Authorize(StandardPermissionProvider.EnableWishlist) && !_workContext.CurrentCustomer.IsGuest(),
        ////////    };

        ////////    //performance optimization (use "HasShoppingCartItems" property)
        ////////    if (customer.HasShoppingCartItems)
        ////////    {
        ////////        model.WishlistItems = customer.ShoppingCartItems
        ////////            .Where(sci => sci.ShoppingCartType == ShoppingCartType.Wishlist)
        ////////            .LimitPerStore(_storeContext.CurrentStore.Id)
        ////////            .ToList()
        ////////            .GetTotalProducts();
        ////////    }



        ////////    return PartialView(model);
        ////////}

        /////////// <summary>
        /////////// Prepare category (simple) models
        /////////// </summary>
        /////////// <param name="rootCategoryId">Root category identifier</param>
        /////////// <param name="loadSubCategoriesForIds">Load subcategories only for the specified category IDs; pass null to load subcategories for all categories</param>
        /////////// <param name="level">Current level</param>
        /////////// <param name="levelsToLoad">A value indicating how many levels to load (max)</param>
        /////////// <param name="validateIncludeInTopMenu">A value indicating whether we should validate "include in top menu" property</param>
        /////////// <returns>Category models</returns>
        ////////[NonAction]
        ////////protected virtual IList<CategorySimpleModel> PrepareCategoryTopMenuSimpleModels()
        ////////{
        ////////    var result = new List<CategorySimpleModel>();

        ////////    foreach (var category in _categoryService.GetAllCategories(includeInTopMenu: true))
        ////////    {
        ////////        var categoryModel = new CategorySimpleModel
        ////////        {
        ////////            Id = category.Id,
        ////////            Name = category.GetLocalized(x => x.Name),
        ////////            SeName = category.GetSeName()
        ////////        };
        ////////        result.Add(categoryModel);
        ////////    }

        ////////    return result;
        ////////}
        #endregion



        [ChildActionOnly]
        public ActionResult ManufacturerHomePage()
        {
            if (!_catalogSettings.ShowManufacturersHomePage)
                return Content(string.Empty);

            //Para moviles no se habilita esta f
            if (HttpContext.Request.Browser.IsMobileDevice)
                return Content(string.Empty);

            var model = new ManufacturerHomePageModel();
            model.Enable = true;

            //Consulta las marcas que van en el home
            string cacheKey = string.Format(ModelCacheEventConsumer.MANUFACTURER_ON_HOMEPAGE);
            var cachedManufacturers = _cacheManager.Get(cacheKey, () =>
            {
                return _manufacturerService
                    .GetManufacturersOnHomePage()
                    .ToModels(true, _localizationService, _mediaSettings, _pictureService);
            });


            string cacheKeyCategories = string.Format(ModelCacheEventConsumer.MANUFACTURER_AS_CATEGORIES_ON_HOMEPAGE);
            var cachedCategories = _cacheManager.Get(cacheKeyCategories, () =>
            {
                return _categoryService
                    .GetAllCategoriesDisplayedWithManufacturers()
                    .ToModels(true, _localizationService, _mediaSettings, _pictureService);
            });

            //Toma aleatoreamente un número de registros para ser mostrados dividido entre dos
            model.Manufacturers = cachedManufacturers
                .OrderBy(elem => Guid.NewGuid())
                .Take(_catalogSettings.NumberManufacturersOnHome / 2)
                .ToList();

            //Toma aleatoreamente un número de registros para ser mostrados dividido entre dos
            model.Categories = cachedCategories
               .OrderBy(elem => Guid.NewGuid())
               .Take(_catalogSettings.NumberManufacturersOnHome / 2)
               .ToList();

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult HomepageCategories()
        {
            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

            string categoriesCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_HOMEPAGE_KEY,
                string.Join(",", customerRolesIds),
                _storeContext.CurrentStore.Id,
                _workContext.WorkingLanguage.Id,
                _webHelper.IsCurrentConnectionSecured());

            var model = _cacheManager.Get(categoriesCacheKey, () =>
                _categoryService.GetAllCategoriesDisplayedOnHomePage()
                .Select(x =>
                {
                    var catModel = x.ToModel();

                    //prepare picture model
                    int pictureSize = _mediaSettings.CategoryThumbPictureSize;
                    var categoryPictureCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_PICTURE_MODEL_KEY, x.Id, pictureSize, true, _workContext.WorkingLanguage.Id, _webHelper.IsCurrentConnectionSecured(), _storeContext.CurrentStore.Id);
                    catModel.PictureModel = _cacheManager.Get(categoryPictureCacheKey, () =>
                    {
                        var picture = _pictureService.GetPictureById(x.PictureId);
                        var pictureModel = new PictureModel
                        {
                            FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                            ImageUrl = _pictureService.GetPictureUrl(picture, pictureSize),
                            Title = string.Format(_localizationService.GetResource("Media.Category.ImageLinkTitleFormat"), catModel.Name),
                            AlternateText = string.Format(_localizationService.GetResource("Media.Category.ImageAlternateTextFormat"), catModel.Name)
                        };
                        return pictureModel;
                    });

                    return catModel;
                })
                .ToList()
            );

            if (model.Count == 0)
                return Content("");

            return PartialView(model);
        }

        #endregion

        #region CategoriesHomePage

        [ChildActionOnly]
        public ActionResult CategoriesHomePage()
        {
            var cacheKey = ModelCacheEventConsumer.CATEGORIES_HOMEPAGE;

            var cachedCategories = _cacheManager.Get(cacheKey, () =>
            {

                var model = new CategoriesHomePageModel();
                model.Categories = new List<CategoriesHomePageModel.CategoryHomePageModel>();

                ///Consulta las categorias que van en el home y las organiza por columna y despues por orden
                var categoriesHomePage = _categoryService.GetCategoryOrganizationHomeMenu()
                    .OrderBy(c => c.ColumnId)
                    .OrderBy(c => c.Order);

                int column = 0;
                foreach (var parentCategory in categoriesHomePage)
                {
                    //Consulta el detalle de la categoria
                    var category = _categoryService.GetCategoryById(parentCategory.CategoryId);

                    //Inicial el modelo con los datos de la categoría
                    var categoryModel = new CategoriesHomePageModel.CategoryHomePageModel()
                    {
                        CategoryId = parentCategory.CategoryId,
                        Name = category.Name,
                        SeName = category.GetSeName(),
                        Order = parentCategory.Order,
                        PictureModel = category.GetPicture(_localizationService, _mediaSettings, _pictureService),
                        Column = parentCategory.ColumnId
                    };



                    //recorre los hijos y los agrega a la lista
                    categoryModel.ChildrenCategories = new List<CategoriesHomePageModel.CategoryHomePageModel>();
                    foreach (var childCategory in parentCategory.ChildrenCategories)
                    {
                        var child = _categoryService.GetCategoryById(childCategory.CategoryId);
                        categoryModel.ChildrenCategories.Add(new CategoriesHomePageModel.CategoryHomePageModel()
                        {
                            CategoryId = child.Id,
                            Name = child.Name,
                            SeName = child.GetSeName(),
                            Order = childCategory.Order
                        });
                    }


                    model.Categories.Add(categoryModel);
                }


                model.NumColumns = model.Categories.Max(c => c.Column);

                return model;
            });

            cachedCategories.IsMobileDevice = Request.Browser.IsMobileDevice;

            return View(cachedCategories);
        }


        #endregion



        #region Manufacturers

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Manufacturer(int manufacturerId, CatalogPagingFilteringModel command)
        {
            var manufacturer = _manufacturerService.GetManufacturerById(manufacturerId);
            if (manufacturer == null || manufacturer.Deleted)
                return InvokeHttp404();

            //Check whether the current user has a "Manage catalog" permission
            //It allows him to preview a manufacturer before publishing
            if (!manufacturer.Published && !_permissionService.Authorize(StandardPermissionProvider.ManageManufacturers))
                return InvokeHttp404();

            //ACL (access control list)
            if (!_aclService.Authorize(manufacturer))
                return InvokeHttp404();

            //Store mapping
            if (!_storeMappingService.Authorize(manufacturer))
                return InvokeHttp404();

            //'Continue shopping' URL
            _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                SystemCustomerAttributeNames.LastContinueShoppingPage,
                _webHelper.GetThisPageUrl(false),
                _storeContext.CurrentStore.Id);

            var model = manufacturer.ToModel();




            //sorting
            PrepareSortingOptions(model.PagingFilteringContext, command);
            //view mode
            PrepareViewModes(model.PagingFilteringContext, command);
            //page size
            PreparePageSizeOptions(model.PagingFilteringContext, command,
                manufacturer.AllowCustomersToSelectPageSize,
                manufacturer.PageSizeOptions,
                manufacturer.PageSize);


            //price ranges
            model.PagingFilteringContext.PriceRangeFilter.LoadPriceRangeFilters(manufacturer.PriceRanges, _webHelper, _priceFormatter);
            var selectedPriceRange = model.PagingFilteringContext.PriceRangeFilter.GetSelectedPriceRange(_webHelper, manufacturer.PriceRanges);
            decimal? minPriceConverted = null;
            decimal? maxPriceConverted = null;
            if (selectedPriceRange != null)
            {
                if (selectedPriceRange.From.HasValue)
                    minPriceConverted = _currencyService.ConvertToPrimaryStoreCurrency(selectedPriceRange.From.Value, _workContext.WorkingCurrency);

                if (selectedPriceRange.To.HasValue)
                    maxPriceConverted = _currencyService.ConvertToPrimaryStoreCurrency(selectedPriceRange.To.Value, _workContext.WorkingCurrency);
            }



            //featured products
            if (!_catalogSettings.IgnoreFeaturedProducts)
            {
                IPagedList<Product> featuredProducts = null;

                //We cache a value indicating whether we have featured products
                var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                    .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                string cacheKey = string.Format(ModelCacheEventConsumer.MANUFACTURER_HAS_FEATURED_PRODUCTS_KEY, manufacturerId,
                    string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
                var hasFeaturedProductsCache = _cacheManager.Get<bool?>(cacheKey);
                if (!hasFeaturedProductsCache.HasValue)
                {
                    //no value in the cache yet
                    //let's load products and cache the result (true/false)
                    featuredProducts = _productService.SearchProducts(
                       manufacturerId: manufacturer.Id,
                       storeId: _storeContext.CurrentStore.Id,
                       visibleIndividuallyOnly: true,
                       featuredProducts: true);
                    hasFeaturedProductsCache = featuredProducts.TotalCount > 0;
                    _cacheManager.Set(cacheKey, hasFeaturedProductsCache, 60);
                }
                if (hasFeaturedProductsCache.Value && featuredProducts == null)
                {
                    //cache indicates that the manufacturer has featured products
                    //let's load them
                    featuredProducts = _productService.SearchProducts(
                       manufacturerId: manufacturer.Id,
                       storeId: _storeContext.CurrentStore.Id,
                       visibleIndividuallyOnly: true,
                       featuredProducts: true);
                }
                if (featuredProducts != null)
                {
                    model.FeaturedProducts = PrepareProductOverviewModels(featuredProducts).ToList();
                }
            }


            var categoryIds = new List<int>();
            //advanced search
            var categoryId = 0;
            int.TryParse(Request.QueryString["Cid"] == null ? "0" : Request.QueryString["Cid"], out categoryId);
            if (categoryId > 0)
            {
                categoryIds.Add(categoryId);
            }

            //products
            IList<int> alreadyFilteredSpecOptionIds = model.PagingFilteringContext.SpecificationFilter.GetAlreadyFilteredIds(_webHelper);
            //Categorias seleccionadas
            IList<int> alreadyFilteredCategoryIds = model.PagingFilteringContext.CategoryFilter.GetAlreadyFilteredIds(_webHelper);
            //Ciudad seleccionada
            int? stateProvinceId = model.PagingFilteringContext.StateProvinceFilter.GetAlreadyFilteredId(_webHelper);
            //Categoria especial seleccionada (Marca de la moto)
            int? specialCategoryId = model.PagingFilteringContext.BikeReferenceFilter.GetAlreadyFilteredId(_webHelper);

            Dictionary<int, int> filterableSpecificationAttributeOptionIds;
            Dictionary<int, int> filterableCategoryIds;
            Dictionary<int, int> filterableStateProvinceIds;
            Dictionary<int, int> filterableManufacturerIds;
            Dictionary<int, int> filterableSpecialCategoryIds;
            Tuple<int, int> minMaxPrice;

            var products = _productService.SearchProducts(
                out filterableSpecificationAttributeOptionIds,
                out filterableCategoryIds,
                out filterableStateProvinceIds,
                out filterableManufacturerIds,
                out filterableSpecialCategoryIds,
                out minMaxPrice,
                true,
                loadFilterableCategoryIds: categoryIds.Count == 0,  //Solo realiza conteo de categorias si no está filtrando por categoria
                loadFilterableStateProvinceIds: !stateProvinceId.HasValue, //Solo realiza conteo de ciudaddes si no está filtrando por ciudad,
                loadPriceRange: !minPriceConverted.HasValue && !maxPriceConverted.HasValue, //Solo realiza conteo de precios si no está filtrando por precio
                loadFilterableManufacturerIds: false, //Solo realiza conteo de marcas si no está filtrando por marca
                loadFilterableSpecialCategoryIds: !specialCategoryId.HasValue,//Solo realiza connteo de categorias especiales si no está filtrado por referencia
                manufacturerId: manufacturer.Id,
                storeId: _storeContext.CurrentStore.Id,
                visibleIndividuallyOnly: true,
                featuredProducts: _catalogSettings.IncludeFeaturedProductsInNormalLists ? null : (bool?)false,
                priceMin: minPriceConverted,
                priceMax: maxPriceConverted,
                orderBy: (ProductSortingEnum)command.OrderBy,
                pageIndex: command.PageNumber - 1,
                pageSize: command.PageSize);
            model.Products = PrepareProductOverviewModels(products).ToList();

            model.PagingFilteringContext.LoadPagedList(products);


            //Price
            model.PagingFilteringContext.PriceRangeFilter.LoadPriceRangeFilters(minMaxPrice.Item1, minMaxPrice.Item2, _webHelper, _priceFormatter);

            //specs
            model.PagingFilteringContext.SpecificationFilter.PrepareSpecsFilters(alreadyFilteredSpecOptionIds,
        filterableSpecificationAttributeOptionIds,
        _specificationAttributeService, _webHelper, _workContext);

            //categories
            model.PagingFilteringContext.CategoryFilter.PrepareCategoriesFilters(alreadyFilteredCategoryIds,
        filterableCategoryIds,
        _categoryService, _webHelper, _workContext);

            //state provinces
            model.PagingFilteringContext.StateProvinceFilter.PrepareStateProvinceFilters(stateProvinceId,
        filterableStateProvinceIds,
        _stateProvinceService, _webHelper, _workContext);

            //bike references
            model.PagingFilteringContext.BikeReferenceFilter.PrepareFilters(specialCategoryId,
        filterableSpecialCategoryIds,
        _categoryService, _webHelper, _workContext);


            //template
            var templateCacheKey = string.Format(ModelCacheEventConsumer.MANUFACTURER_TEMPLATE_MODEL_KEY, manufacturer.ManufacturerTemplateId);
            var templateViewPath = _cacheManager.Get(templateCacheKey, () =>
            {
                var template = _manufacturerTemplateService.GetManufacturerTemplateById(manufacturer.ManufacturerTemplateId);
                if (template == null)
                    template = _manufacturerTemplateService.GetAllManufacturerTemplates().FirstOrDefault();
                if (template == null)
                    throw new Exception("No default template could be loaded");
                return template.ViewPath;
            });

            //activity log
            _customerActivityService.InsertActivity("PublicStore.ViewManufacturer", _localizationService.GetResource("ActivityLog.PublicStore.ViewManufacturer"), manufacturer.Name);
            model.IsMobileDevice = Request.Browser.IsMobileDevice;

            //Actualiza el titulo
            string filtersUrl = GetTitleParts(model.PagingFilteringContext);

            if (!string.IsNullOrEmpty(model.MetaTitle))
                model.MetaTitle = string.Format(model.MetaTitle, filtersUrl);
            else
                model.MetaTitle = string.Concat(model.Name, " ", filtersUrl);

            return View(templateViewPath, model);
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult ManufacturerAll()
        {
            var model = new List<ManufacturerModel>();
            var manufacturers = _manufacturerService.GetAllManufacturers();
            foreach (var manufacturer in manufacturers)
            {
                var modelMan = manufacturer.ToModel();

                //prepare picture model
                int pictureSize = _mediaSettings.ManufacturerThumbPictureSize;
                var manufacturerPictureCacheKey = string.Format(ModelCacheEventConsumer.MANUFACTURER_PICTURE_MODEL_KEY, manufacturer.Id, pictureSize, true, _workContext.WorkingLanguage.Id, _webHelper.IsCurrentConnectionSecured(), _storeContext.CurrentStore.Id);
                modelMan.PictureModel = _cacheManager.Get(manufacturerPictureCacheKey, () =>
                {
                    var picture = _pictureService.GetPictureById(manufacturer.PictureId);
                    var pictureModel = new PictureModel
                    {
                        FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                        ImageUrl = _pictureService.GetPictureUrl(picture, pictureSize),
                        Title = string.Format(_localizationService.GetResource("Media.Manufacturer.ImageLinkTitleFormat"), modelMan.Name),
                        AlternateText = string.Format(_localizationService.GetResource("Media.Manufacturer.ImageAlternateTextFormat"), modelMan.Name)
                    };
                    return pictureModel;
                });
                model.Add(modelMan);
            }

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult ManufacturerNavigation(int currentManufacturerId)
        {
            if (_catalogSettings.ManufacturersBlockItemsToDisplay == 0)
                return Content("");

            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.MANUFACTURER_NAVIGATION_MODEL_KEY, currentManufacturerId, _workContext.WorkingLanguage.Id, string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
            var cacheModel = _cacheManager.Get(cacheKey, () =>
                {
                    var currentManufacturer = _manufacturerService.GetManufacturerById(currentManufacturerId);

                    var manufacturers = _manufacturerService.GetAllManufacturers(pageSize: _catalogSettings.ManufacturersBlockItemsToDisplay);
                    var model = new ManufacturerNavigationModel
                    {
                        TotalManufacturers = manufacturers.TotalCount
                    };

                    foreach (var manufacturer in manufacturers)
                    {
                        var modelMan = new ManufacturerBriefInfoModel
                        {
                            Id = manufacturer.Id,
                            Name = manufacturer.GetLocalized(x => x.Name),
                            SeName = manufacturer.GetSeName(),
                            IsActive = currentManufacturer != null && currentManufacturer.Id == manufacturer.Id,
                        };
                        model.Manufacturers.Add(modelMan);
                    }
                    return model;
                });

            if (cacheModel.Manufacturers.Count == 0)
                return Content("");

            return PartialView(cacheModel);
        }

        #endregion

        #region Vendors

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Vendor(int vendorId, CatalogPagingFilteringModel command)
        {

            var vendor = _vendorService.GetVendorById(vendorId, true);
            if (vendor == null || vendor.Deleted || !vendor.Active || vendor.VendorType == VendorType.User)
                return InvokeHttp404();

            //Vendor is active?
            if (!vendor.Active)
                return InvokeHttp404();

            #region Codigo Eliminado
            //'Continue shopping' URL
            //_genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
            //    SystemCustomerAttributeNames.LastContinueShoppingPage,
            //    _webHelper.GetThisPageUrl(false),
            //    _storeContext.CurrentStore.Id);
            #endregion


            var model = PrepareVendorModel(vendor);

            //sorting
            PrepareSortingOptions(model.PagingFilteringContext, command);
            //view mode
            PrepareViewModes(model.PagingFilteringContext, command);
            //page size
            PreparePageSizeOptions(model.PagingFilteringContext, command,
                vendor.AllowCustomersToSelectPageSize,
                vendor.PageSizeOptions,
                vendor.PageSize);

            //products


            Dictionary<int, int> filterableSpecificationAttributeOptionIds;
            IPagedList<Product> products = null;



            //Por defecto no se muestran las especificaciones adidcionales, pero si está filtrado por Id
            bool prepareSpecificationAttributes = false;
            bool prepareManufacturers = false;
            //Si viene filtro por id de producto intenta realizar el filtro
            //Si  no encuentra ningún resultado trae todos los productos del vendedor
            if (command.pid > 0)
            {
                //Consulta el producto por el id
                var product = _productService.GetProductById(command.pid);
                //Valida que el producto exista y que este asociado al vendedor
                if (product != null && product.VendorId == vendor.Id)
                {
                    model.FilteredByProduct = true;
                    products = new PagedList<Product>(new List<Product>() { product }, 0, 1, 1);
                    prepareSpecificationAttributes = true;
                    prepareManufacturers = true;
                }
            }

            //Si no hay productos, realiza la busqueda por los productos del vendedor
            //Esta busqueda se hace por fuera del else de command.pid > 0 ya que si el producto no existe o no es del vendedor lista todos los demás
            if (products == null)
            {
                products = _productService.SearchProducts(out filterableSpecificationAttributeOptionIds, false,
                    vendorId: vendor.Id,
                    storeId: _storeContext.CurrentStore.Id,
                    visibleIndividuallyOnly: true,
                    orderBy: (ProductSortingEnum)command.OrderBy,
                    pageIndex: command.PageNumber - 1,
                    pageSize: command.PageSize,
                    keywords: string.IsNullOrWhiteSpace(command.q) ? null : command.q);
            }


            model.Products = PrepareProductOverviewModels(products, prepareSpecificationAttributes: prepareSpecificationAttributes, prepareManufacturer: prepareManufacturers).ToList();

            model.TotalActiveProducts = _productService.CountActiveProductsByVendorId(vendor.Id);

            //Consulta todas las ventas del vendedor
            var vendorSellings = _orderService.SearchOrders(vendorId: vendorId, os: Nop.Core.Domain.Orders.OrderStatus.Complete);
            model.TotalSoldProducts = vendorSellings.Count;


            model.PagingFilteringContext.LoadPagedList(products);

            //Agrega al view data que no cargue los estilos del ancho del template
            ViewData.Add("noWidth", true);
            this.HidePublishButton();
            model.IsMobileDevice = Request.Browser.IsMobileDevice;
            return View(model);
        }

        public VendorModel PrepareVendorModel(Vendor vendor)
        {
            return vendor.ToModel(_workContext, _pictureService, _localizationService, _mediaSettings, _vendorService, _addressService);
        }


        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult VendorAll()
        {
            //we don't allow viewing of vendors if "vendors" block is hidden
            if (_vendorSettings.VendorsBlockItemsToDisplay == 0)
                return RedirectToRoute("HomePage");

            string cacheKey = ModelCacheEventConsumer.VENDOR_ALL;

            var vendors = _cacheManager.Get(cacheKey, () => { 
                return _vendorService.GetAllVendors(vendorType: VendorType.Market);
            });

            var model = vendors.ToModels(_workContext, _pictureService, _localizationService, _mediaSettings, _vendorService, _addressService);
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult VendorNavigation()
        {
            if (_vendorSettings.VendorsBlockItemsToDisplay == 0)
                return Content("");

            string cacheKey = ModelCacheEventConsumer.VENDOR_NAVIGATION_MODEL_KEY;
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                var vendors = _vendorService.GetAllVendors(pageSize: _vendorSettings.VendorsBlockItemsToDisplay);
                var model = new VendorNavigationModel
                {
                    TotalVendors = vendors.TotalCount
                };

                foreach (var vendor in vendors)
                {
                    model.Vendors.Add(new VendorBriefInfoModel
                    {
                        Id = vendor.Id,
                        Name = vendor.GetLocalized(x => x.Name),
                        SeName = vendor.GetSeName(),
                    });
                }
                return model;
            });

            if (cacheModel.Vendors.Count == 0)
                return Content("");

            return PartialView(cacheModel);
        }

        [ChildActionOnly]
        public ActionResult VendorsHomePage()
        {
            string cacheKey = ModelCacheEventConsumer.VENDOR_HOMEPAGE_KEY;
            var vendors = _cacheManager.Get(cacheKey, () =>
            {
                return _vendorService.GetAllVendors(showOnHomePage: true)
                    .ToModels(_workContext, _pictureService, _localizationService, _mediaSettings, _vendorService);
            });
            var model = new VendorsHomePageModel();
            model.Vendors = vendors.OrderBy(v => Guid.NewGuid())
                .Take(Request.Browser.IsMobileDevice ? 6 : _catalogSettings.NumberOfVendorsOnHome)
                .ToList();

            return View(model);
        }

        #endregion

        #region Vendor reviews
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult VendorReviews(int vendorId)
        {
            var vendor = _vendorService.GetVendorById(vendorId);
            if (vendor == null || vendor.Deleted)
                return RedirectToRoute("HomePage");

            var model = new VendorReviewsModel();

            PrepareVendorReviewsModel(model, vendor);
            //only registered users can leave reviews
            if (_workContext.CurrentCustomer.IsGuest() && !_catalogSettings.AllowAnonymousUsersToReviewProduct)
                ModelState.AddModelError("", _localizationService.GetResource("Reviews.OnlyRegisteredUsersCanWriteReviews"));
            //default value
            model.AddVendorReview.Rating = _catalogSettings.DefaultProductRatingValue;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SetVendorReviewHelpfulness(int vendorReviewId, bool washelpful)
        {
            var vendorReview = _vendorService.GetVendorReviewById(vendorReviewId);
            if (vendorReview == null)
                throw new ArgumentException("No vendor review found with the specified id");

            if (_workContext.CurrentCustomer.IsGuest() && !_catalogSettings.AllowAnonymousUsersToReviewProduct)
            {
                return Json(new
                {
                    Result = _localizationService.GetResource("Reviews.Helpfulness.OnlyRegistered"),
                    TotalYes = vendorReview.HelpfulYesTotal,
                    TotalNo = vendorReview.HelpfulNoTotal
                });
            }

            //customers aren't allowed to vote for their own reviews
            if (vendorReview.CustomerId == _workContext.CurrentCustomer.Id)
            {
                return Json(new
                {
                    Result = _localizationService.GetResource("Reviews.Helpfulness.YourOwnReview"),
                    TotalYes = vendorReview.HelpfulYesTotal,
                    TotalNo = vendorReview.HelpfulNoTotal
                });
            }

            //delete previous helpfulness
            var prh = vendorReview.VendorReviewHelpfulnessEntries
                .FirstOrDefault(x => x.CustomerId == _workContext.CurrentCustomer.Id);
            if (prh != null)
            {
                //existing one
                prh.WasHelpful = washelpful;
            }
            else
            {
                //insert new helpfulness
                prh = new VendorReviewHelpfulness
                {
                    VendorReviewId = vendorReview.Id,
                    CustomerId = _workContext.CurrentCustomer.Id,
                    WasHelpful = washelpful,
                };
                vendorReview.VendorReviewHelpfulnessEntries.Add(prh);
            }
            _vendorService.UpdateVendor(vendorReview.Vendor);

            //new totals
            vendorReview.HelpfulYesTotal = vendorReview.VendorReviewHelpfulnessEntries.Count(x => x.WasHelpful);
            vendorReview.HelpfulNoTotal = vendorReview.VendorReviewHelpfulnessEntries.Count(x => !x.WasHelpful);
            _vendorService.UpdateVendor(vendorReview.Vendor);

            return Json(new
            {
                Result = _localizationService.GetResource("Reviews.Helpfulness.SuccessfullyVoted"),
                TotalYes = vendorReview.HelpfulYesTotal,
                TotalNo = vendorReview.HelpfulNoTotal
            });
        }

        [NonAction]
        protected virtual void PrepareVendorReviewsModel(VendorReviewsModel model, Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            if (model == null)
                throw new ArgumentNullException("model");

            model.VendorId = vendor.Id;
            model.VendorName = vendor.GetLocalized(x => x.Name);
            model.VendorSeName = vendor.GetSeName();
            model.VendorUrl = string.Concat(_storeContext.CurrentStore.Url, Url.RouteUrl("Vendor", new { seName = model.VendorSeName }));

            var vendorReviews = vendor.VendorReviews.Where(pr => pr.IsApproved).OrderBy(pr => pr.CreatedOnUtc);
            foreach (var vr in vendorReviews)
            {
                var customer = vr.Customer;
                model.Items.Add(new VendorReviewModel
                {
                    Id = vr.Id,
                    CustomerId = vr.CustomerId,
                    CustomerName = customer.FormatUserName(),
                    AllowViewingProfiles = /*_customerSettings.AllowViewingProfiles*/false && customer != null && !customer.IsGuest(),
                    Title = vr.Title,
                    ReviewText = vr.ReviewText,
                    Rating = vr.Rating,
                    Helpfulness = new VendorReviewHelpfulnessModel
                    {
                        VendorReviewId = vr.Id,
                        HelpfulYesTotal = vr.HelpfulYesTotal,
                        HelpfulNoTotal = vr.HelpfulNoTotal,
                    },
                    WrittenOnStr = _dateTimeHelper.ConvertToUserTime(vr.CreatedOnUtc, DateTimeKind.Utc).ToString("g"),
                });
            }


            //Si no teie reviews previamente dejados puede dejar        
            if (_workContext.CurrentCustomer != null && !_workContext.CurrentCustomer.IsGuest())
                model.AddVendorReview.CanCurrentCustomerLeaveReview = !_vendorService.CustomerHasVendorReview(_workContext.CurrentCustomer.Id, vendor.Id);
            else
                model.AddVendorReview.CanCurrentCustomerLeaveReview = true;
             
            model.AddVendorReview.DisplayCaptcha = false/*_captchaSettings.Enabled && _captchaSettings.ShowOnProductReviewPage*/;
        }

        #endregion

        #region Product tags

        [ChildActionOnly]
        public ActionResult PopularProductTags()
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.PRODUCTTAG_POPULAR_MODEL_KEY, _workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id);
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                var model = new PopularProductTagsModel();

                //get all tags
                var allTags = _productTagService
                    .GetAllProductTags()
                    //filter by current store
                    .Where(x => _productTagService.GetProductCount(x.Id, _storeContext.CurrentStore.Id) > 0)
                    //order by product count
                    .OrderByDescending(x => _productTagService.GetProductCount(x.Id, _storeContext.CurrentStore.Id))
                    .ToList();

                var tags = allTags
                    .Take(_catalogSettings.NumberOfProductTags)
                    .ToList();
                //sorting
                tags = tags.OrderBy(x => x.GetLocalized(y => y.Name)).ToList();

                model.TotalTags = allTags.Count;

                foreach (var tag in tags)
                    model.Tags.Add(new ProductTagModel
                    {
                        Id = tag.Id,
                        Name = tag.GetLocalized(y => y.Name),
                        SeName = tag.GetSeName(),
                        ProductCount = _productTagService.GetProductCount(tag.Id, _storeContext.CurrentStore.Id)
                    });
                return model;
            });

            if (cacheModel.Tags.Count == 0)
                return Content("");

            return PartialView(cacheModel);
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult ProductsByTag(int productTagId, CatalogPagingFilteringModel command)
        {
            var productTag = _productTagService.GetProductTagById(productTagId);
            if (productTag == null)
                return InvokeHttp404();

            var model = new ProductsByTagModel
            {
                Id = productTag.Id,
                TagName = productTag.GetLocalized(y => y.Name),
                TagSeName = productTag.GetSeName()
            };


            //sorting
            PrepareSortingOptions(model.PagingFilteringContext, command);
            //view mode
            PrepareViewModes(model.PagingFilteringContext, command);
            //page size
            PreparePageSizeOptions(model.PagingFilteringContext, command,
                _catalogSettings.ProductsByTagAllowCustomersToSelectPageSize,
                _catalogSettings.ProductsByTagPageSizeOptions,
                _catalogSettings.ProductsByTagPageSize);


            //products
            var products = _productService.SearchProducts(
                storeId: _storeContext.CurrentStore.Id,
                productTagId: productTag.Id,
                visibleIndividuallyOnly: true,
                orderBy: (ProductSortingEnum)command.OrderBy,
                pageIndex: command.PageNumber - 1,
                pageSize: command.PageSize);
            model.Products = PrepareProductOverviewModels(products).ToList();

            model.PagingFilteringContext.LoadPagedList(products);
            return View(model);
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult ProductTagsAll()
        {
            var model = new PopularProductTagsModel();
            model.Tags = _productTagService
                .GetAllProductTags()
                //filter by current store
                .Where(x => _productTagService.GetProductCount(x.Id, _storeContext.CurrentStore.Id) > 0)
                //sort by name
                .OrderBy(x => x.GetLocalized(y => y.Name))
                .Select(x =>
                            {
                                var ptModel = new ProductTagModel
                                {
                                    Id = x.Id,
                                    Name = x.GetLocalized(y => y.Name),
                                    SeName = x.GetSeName(),
                                    ProductCount = _productTagService.GetProductCount(x.Id, _storeContext.CurrentStore.Id)
                                };
                                return ptModel;
                            })
                .ToList();
            return View(model);
        }

        #endregion

        #region Searching

        [NopHttpsRequirement(SslRequirement.No)]
        [ValidateInput(false)]
        public ActionResult Search(SearchModel model, CatalogPagingFilteringModel command)
        {
            //'Continue shopping' URL
            _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                SystemCustomerAttributeNames.LastContinueShoppingPage,
                _webHelper.GetThisPageUrl(false),
                _storeContext.CurrentStore.Id);

            if (model == null)
                model = new SearchModel();

            model.IsMobileDevice = Request.Browser.IsMobileDevice;



            if (model.Q == null)
                model.Q = "";
            model.Q = model.Q.Trim();

            //sorting
            PrepareSortingOptions(model.PagingFilteringContext, command);
            //view mode
            PrepareViewModes(model.PagingFilteringContext, command);
            //page size
            PreparePageSizeOptions(model.PagingFilteringContext, command,
                _catalogSettings.SearchPageAllowCustomersToSelectPageSize,
                _catalogSettings.SearchPagePageSizeOptions,
                _catalogSettings.SearchPageProductsPerPage);

            #region CodigoEliminado
            //var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
            //    .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            //string cacheKey = string.Format(ModelCacheEventConsumer.SEARCH_CATEGORIES_MODEL_KEY, _workContext.WorkingLanguage.Id, string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
            //var categories = _cacheManager.Get(cacheKey, () =>
            //{
            //    var categoriesModel = new List<SearchModel.CategoryModel>();
            //    //all categories
            //    foreach (var c in _categoryService.GetAllCategories())
            //    {
            //        //generate full category name (breadcrumb)
            //        string categoryBreadcrumb = "";
            //        var breadcrumb = c.GetCategoryBreadCrumb(_categoryService, _aclService, _storeMappingService);
            //        for (int i = 0; i <= breadcrumb.Count - 1; i++)
            //        {
            //            categoryBreadcrumb += breadcrumb[i].GetLocalized(x => x.Name);
            //            if (i != breadcrumb.Count - 1)
            //                categoryBreadcrumb += " >> ";
            //        }
            //        categoriesModel.Add(new SearchModel.CategoryModel
            //        {
            //            Id = c.Id,
            //            Breadcrumb = categoryBreadcrumb
            //        });
            //    }
            //    return categoriesModel;
            //});
            //if (categories.Count > 0)
            //{
            //    //first empty entry
            //    model.AvailableCategories.Add(new SelectListItem
            //        {
            //            Value = "0",
            //            Text = _localizationService.GetResource("Common.All")
            //        });
            //    //all other categories
            //    foreach (var c in categories)
            //    {
            //        model.AvailableCategories.Add(new SelectListItem
            //        {
            //            Value = c.Id.ToString(),
            //            Text = c.Breadcrumb,
            //            Selected = model.Cid == c.Id
            //        });
            //    }
            //}

            //var manufacturers = _manufacturerService.GetAllManufacturers();
            //if (manufacturers.Count > 0)
            //{
            //    model.AvailableManufacturers.Add(new SelectListItem
            //    {
            //        Value = "0",
            //        Text = _localizationService.GetResource("Common.All")
            //    });
            //    foreach (var m in manufacturers)
            //        model.AvailableManufacturers.Add(new SelectListItem
            //        {
            //            Value = m.Id.ToString(),
            //            Text = m.GetLocalized(x => x.Name),
            //            Selected = model.Mid == m.Id
            //        });
            //}
            #endregion

            IPagedList<Product> products = new PagedList<Product>(new List<Product>(), 0, 1);
            // only search if query string search keyword is set (used to avoid searching or displaying search term min length error message on /search page load)
            //Se agrega validación de specs para poder realizar filtro desde el menu
            //if (Request.Params["Q"] != null || (Request.Params["Q"] == null && Request["specs"] != null))
            if (!string.IsNullOrEmpty(model.Q))
            {
                //Solo si tiene un filto por speficicación puede buscar
                if (model.Q.Length <= _catalogSettings.ProductSearchTermMinimumLength && Request["specs"] == null)
                {
                    model.Warning = string.Format(_localizationService.GetResource("Search.SearchTermMinimumLengthIsNCharacters"), _catalogSettings.ProductSearchTermMinimumLength);
                }
                else
                {

                    model.ShowSimilarSearches = _catalogSettings.ShowSimilarSearches;

                    var categoryIds = new List<int>();


                    bool searchInDescriptions = false;

                    //advanced search
                    var categoryId = model.Cid;
                    if (categoryId > 0)
                    {
                        categoryIds.Add(categoryId);
                        if (model.Isc)
                        {
                            //include subcategories
                            categoryIds.AddRange(GetChildCategoryIds(categoryId));
                        }
                    }

                    searchInDescriptions = model.Sid;

                    decimal? minPriceConverted = null;
                    decimal? maxPriceConverted = null;
                    //Valida el rango de precios
                    var priceFilter = command.PriceRangeFilter.GetSelectedPriceRange(_webHelper, null);
                    if (priceFilter != null)
                    {
                        minPriceConverted = priceFilter.From;
                        maxPriceConverted = priceFilter.To;
                    }
                    //especificaciones seleccionadas
                    IList<int> alreadyFilteredSpecOptionIds = model.PagingFilteringContext.SpecificationFilter.GetAlreadyFilteredIds(_webHelper);
                    //Categorias seleccionadas
                    IList<int> alreadyFilteredCategoryIds = model.PagingFilteringContext.CategoryFilter.GetAlreadyFilteredIds(_webHelper);
                    //Ciudad seleccionada
                    int? stateProvinceId = model.PagingFilteringContext.StateProvinceFilter.GetAlreadyFilteredId(_webHelper);
                    //Marca seleccionada
                    int? manufacturerId = model.PagingFilteringContext.ManufacturerFilter.GetAlreadyFilteredId(_webHelper);
                    //Categoria especial seleccionada (Marca de la moto)
                    int? specialCategoryId = model.PagingFilteringContext.BikeReferenceFilter.GetAlreadyFilteredId(_webHelper);

                    //categoria especial por la que debería ordenar que solo se activa si el usuario cuenta con esta registrada como moto
                    //Además si viene filtro por categoría especial NO debe ordenar por categoría especial
                    //Si viene algún orden seleccionado NO debe ordenar por categoria especial
                    int? orderBySpecialCategoryId = !specialCategoryId.HasValue && command.OrderBy == 0 ? _workContext.CurrentCustomer.GetBikeReference() : null;

                    //var searchInProductTags = false;
                    var searchInProductTags = searchInDescriptions;

                    //Se agrega filtro por especificaicones
                    Dictionary<int, int> filterableSpecificationAttributeOptionIds;
                    Dictionary<int, int> filterableCategoryIds;
                    Dictionary<int, int> filterableStateProvinceIds;
                    Dictionary<int, int> filterableManufacturerIds;
                    Dictionary<int, int> filterableSpecialCategoryIds;
                    Tuple<int, int> minMaxPrice;

                    //products
                    products = _productService.SearchProducts(
                        out filterableSpecificationAttributeOptionIds,
                        out filterableCategoryIds,
                        out filterableStateProvinceIds,
                        out filterableManufacturerIds,
                        out filterableSpecialCategoryIds,
                        out minMaxPrice,
                        true,
                        loadFilterableCategoryIds: categoryIds.Count == 0,  //Solo realiza conteo de categorias si no está filtrando por categoria
                        loadFilterableStateProvinceIds: !stateProvinceId.HasValue, //Solo realiza conteo de ciudaddes si no está filtrando por ciudad,
                        loadPriceRange: !minPriceConverted.HasValue && !maxPriceConverted.HasValue, //Solo realiza conteo de precios si no está filtrando por precio
                        loadFilterableManufacturerIds: !manufacturerId.HasValue, //Solo realiza conteo de marcas si no está filtrando por marca
                        loadFilterableSpecialCategoryIds: !specialCategoryId.HasValue,//Solo realiza connteo de categorias especiales si no está filtrado por referencia
                        categoryIds: categoryIds,
                        manufacturerId: manufacturerId ?? 0,
                        storeId: _storeContext.CurrentStore.Id,
                        visibleIndividuallyOnly: true,
                        priceMin: minPriceConverted,
                        priceMax: maxPriceConverted,
                        keywords: model.Q,
                        searchDescriptions: searchInDescriptions,
                        searchSku: searchInDescriptions,
                        searchProductTags: searchInProductTags,
                        languageId: _workContext.WorkingLanguage.Id,
                        orderBy: (ProductSortingEnum)command.OrderBy,
                        filteredSpecs: alreadyFilteredSpecOptionIds,
                        pageIndex: command.PageNumber - 1,
                        pageSize: command.PageSize,
                        stateProvinceId: stateProvinceId,
                        specialCategoryId: specialCategoryId,
                        orderBySpecialCategoryId: orderBySpecialCategoryId);
                    model.Products = PrepareProductOverviewModels(products).ToList();

                    //Price
                    model.PagingFilteringContext.PriceRangeFilter.LoadPriceRangeFilters(minMaxPrice.Item1, minMaxPrice.Item2, _webHelper, _priceFormatter);

                    //specs
                    model.PagingFilteringContext.SpecificationFilter.PrepareSpecsFilters(alreadyFilteredSpecOptionIds,
                filterableSpecificationAttributeOptionIds,
                _specificationAttributeService, _webHelper, _workContext, addFilterNameToUrl: false);

                    //categories
                    model.PagingFilteringContext.CategoryFilter.PrepareCategoriesFilters(alreadyFilteredCategoryIds,
                filterableCategoryIds,
                _categoryService, _webHelper, _workContext, addFilterNameToUrl:false);

                    //state provinces
                    model.PagingFilteringContext.StateProvinceFilter.PrepareStateProvinceFilters(stateProvinceId,
                filterableStateProvinceIds,
                _stateProvinceService, _webHelper, _workContext, addFilterNameToUrl: false);

                    //manufacturer
                    model.PagingFilteringContext.ManufacturerFilter.PrepareFilters(manufacturerId,
                filterableManufacturerIds,
                _manufacturerService, _webHelper, _workContext, addFilterNameToUrl: false);


                    //bike references
                    model.PagingFilteringContext.BikeReferenceFilter.PrepareFilters(specialCategoryId,
                filterableSpecialCategoryIds,
                _categoryService, _webHelper, _workContext, addFilterNameToUrl: false);

                    model.NoResults = !model.Products.Any();

                    //search term statistics
                    if (!String.IsNullOrEmpty(model.Q))
                    {
                        var searchTerm = _searchTermService.GetSearchTermByKeyword(model.Q, _storeContext.CurrentStore.Id);
                        if (searchTerm != null)
                        {
                            searchTerm.Count++;
                            _searchTermService.UpdateSearchTerm(searchTerm);
                        }
                        else
                        {
                            searchTerm = new SearchTerm
                            {
                                Keyword = model.Q,
                                StoreId = _storeContext.CurrentStore.Id,
                                Count = 1
                            };
                            _searchTermService.InsertSearchTerm(searchTerm);
                        }
                    }

                    //event
                    _eventPublisher.Publish(new ProductSearchEvent
                    {
                        SearchTerm = model.Q,
                        SearchInDescriptions = searchInDescriptions,
                        CategoryIds = categoryIds,
                        ManufacturerId = manufacturerId ?? 0,
                        WorkingLanguageId = _workContext.WorkingLanguage.Id
                    });
                }
            }

            model.PagingFilteringContext.LoadPagedList(products);



            string filtersUrl = GetTitleParts(model.PagingFilteringContext);

            model.Title = string.Format(_localizationService.GetResource("PageTitle.Search"), command.q, filtersUrl);
            model.Description = string.Format(_localizationService.GetResource("Search.Metadescription"), command.q, filtersUrl);

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult SimilarSearches(string q)
        {
            var model = new SimilarSearchesModel();


            if (_catalogSettings.ShowSimilarSearches && !Request.Browser.IsMobileDevice)
            {
                //Si no tiene ninguna busqueda es porque es la general
                string cacheKey = string.Empty;
                int top = _catalogSettings.NumSuggestionSimilarSearches;
                if (string.IsNullOrEmpty(q))
                {
                    cacheKey = ModelCacheEventConsumer.SEARCH_SUGGEST_BY_GENERAL;
                    top = _catalogSettings.NumSuggestionSimilarSearchesHome;
                    model.Title = _localizationService.GetResource("similarSearches.most");
                    model.TitleOfTitle = _localizationService.GetResource("similarSearches.most.title");
                }
                else
                {
                    cacheKey = string.Format(ModelCacheEventConsumer.SEARCH_SUGGEST_BY_NEW_SEARCH, q);
                    model.Title = _localizationService.GetResource("similarSearches");
                    model.TitleOfTitle = string.Format(_localizationService.GetResource("similarSearches.title"), q);
                }



                model.Enable = true;
                model.Searches = _cacheManager.Get(cacheKey, () =>
                {
                    return _searchTermService.GetTemsByKeyword(q, top, getMostCommon: true)
                    .Select(s => s.Keyword)
                    .ToList();
                });

                return View(model);
            }
            else
            {
                return Content(string.Empty);
            }



        }

        [ChildActionOnly]
        public ActionResult SearchBox()
        {
            var model = new SearchBoxModel
            {
                AutoCompleteEnabled = _catalogSettings.ProductSearchAutoCompleteEnabled,
                ShowProductImagesInSearchAutoComplete = _catalogSettings.ShowProductImagesInSearchAutoComplete,
                SearchTermMinimumLength = _catalogSettings.ProductSearchTermMinimumLength,
                SearchWithSearchTerms = _catalogSettings.ProductSearchAutoCompleteWithSearchTerms,
                CurrentSearchTerms = Request.QueryString["q"] ?? string.Empty
            };
            return PartialView(model);
        }

        public ActionResult SearchTermAutoComplete(string term)
        {
            if (String.IsNullOrWhiteSpace(term) || term.Length < _catalogSettings.ProductSearchTermMinimumLength)
                return Content("");

            //products
            var productNumber = _catalogSettings.ProductSearchAutoCompleteNumberOfProducts > 0 ?
            _catalogSettings.ProductSearchAutoCompleteNumberOfProducts : 10;

            //Busca los terminos por el buscador de terminos general
            if (_catalogSettings.ProductSearchAutoCompleteWithSearchTerms)
            {
                var result = _searchTermService.GetTemsByKeyword(term, productNumber)
                    .Select(s => new { label = s.Keyword });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var products = _productService.SearchProducts(
                    storeId: _storeContext.CurrentStore.Id,
                    keywords: term,
                    searchSku: false,
                    languageId: _workContext.WorkingLanguage.Id,
                    visibleIndividuallyOnly: true,
                    pageSize: productNumber);

                var models = PrepareProductOverviewModels(products, false, _catalogSettings.ShowProductImagesInSearchAutoComplete, _mediaSettings.AutoCompleteSearchThumbPictureSize).ToList();
                var result = (from p in models
                              select new
                              {
                                  label = p.Name,
                                  producturl = Url.RouteUrl("Product", new { SeName = p.SeName }),
                                  productpictureurl = p.DefaultPictureModel.ImageUrl
                              })
                              .ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion

        #region ExternalSearch

        public ActionResult ExternalSearch(SearchModel model, CatalogPagingFilteringModel command)
        {
            //Debe venir con la llave de cache
            if (!string.IsNullOrEmpty(Request["ck"]))
            {

                string cacheKey = string.Format(ModelCacheEventConsumer.SEARCH_EXTERNAL_BY_NEW_SEARCH, Request["ck"]);

                model = model ?? new SearchModel();
                model.IsMobileDevice = Request.Browser.IsMobileDevice;
                model.Q = string.IsNullOrEmpty(model.Q) ? string.Empty : model.Q.Trim();

                var pagedProducts = _cacheManager.Get(cacheKey, () =>
                {

                    IPagedList<Product> products = new PagedList<Product>(new List<Product>(), 0, 1);

                    if (!string.IsNullOrEmpty(model.Q))
                    {
                        //Solo si tiene un filto por speficicación puede buscar
                        if (model.Q.Length <= _catalogSettings.ProductSearchTermMinimumLength && Request["specs"] == null)
                        {
                            model.Warning = string.Format(_localizationService.GetResource("Search.SearchTermMinimumLengthIsNCharacters"), _catalogSettings.ProductSearchTermMinimumLength);
                        }
                        else
                        {

                            var categoryIds = new List<int>();

                            //advanced search
                            var categoryId = model.Cid;
                            if (categoryId > 0)
                            {
                                categoryIds.Add(categoryId);
                                if (model.Isc)
                                {
                                    //include subcategories
                                    categoryIds.AddRange(GetChildCategoryIds(categoryId));
                                }
                            }


                            decimal? minPriceConverted = null;
                            decimal? maxPriceConverted = null;
                            //Valida el rango de precios
                            var priceFilter = command.PriceRangeFilter.GetSelectedPriceRange(_webHelper, null);
                            if (priceFilter != null)
                            {
                                minPriceConverted = priceFilter.From;
                                maxPriceConverted = priceFilter.To;
                            }

                            //Ciudad seleccionada
                            int? stateProvinceId = model.PagingFilteringContext.StateProvinceFilter.GetAlreadyFilteredId(_webHelper);
                            //Marca seleccionada
                            int? manufacturerId = model.PagingFilteringContext.ManufacturerFilter.GetAlreadyFilteredId(_webHelper);
                            //Categoria especial seleccionada (Marca de la moto)
                            int? specialCategoryId = model.PagingFilteringContext.BikeReferenceFilter.GetAlreadyFilteredId(_webHelper);

                            //products
                            products = _productService.SearchProducts(
                                categoryIds: categoryIds,
                                manufacturerId: manufacturerId ?? 0,
                                storeId: _storeContext.CurrentStore.Id,
                                visibleIndividuallyOnly: true,
                                priceMin: minPriceConverted,
                                priceMax: maxPriceConverted,
                                keywords: model.Q,
                                languageId: _workContext.WorkingLanguage.Id,
                                orderBy: (ProductSortingEnum)command.OrderBy,
                                pageIndex: 0,
                                pageSize: command.PageSize > 20 ? 20 : command.PageSize,
                                stateProvinceId: stateProvinceId,
                                specialCategoryId: specialCategoryId);


                        }
                    }

                    return products;


                });

                model.Products = PrepareProductOverviewModels(pagedProducts, utm_source: command.ana_source, utm_medium: command.ana_medium, utm_campaign: command.ana_campaign).ToList();
                model.PagingFilteringContext.LoadPagedList(pagedProducts);

                return View(model);
            }
            else
                return Content(string.Empty);
        }


        #endregion

        #region Plans
        [HttpGet]
        [Authorize]
        [SameVendorProduct]
        public ActionResult SelectFeaturedAttributesByPlan(int id, FeaturedAttributesByPlanRequest command, Product product)
        {
            //var product = _productService.GetProductById(id);
            //if (product == null)
            //    return HttpNotFound();

            var model = new SelectFeaturedAttributesByPlanModel();

            try
            {
                PrepareSelectFeaturedAttributesByPlanModel(id, command, product, model);
                model.SelectOnHome = product.ShowOnHomePage;
                model.SelectOnSliders = product.FeaturedForSliders;
                model.SelectOnSocialNetworks = product.SocialNetworkFeatured;
            }
            catch (NopException e)
            {
                if (e.Message.Equals("NotFound"))
                    return HttpNotFound();
                if (e.Message.Equals("NoPlanSelected"))
                    return RedirectToAction("SelectPlan", "Sales");
                else
                    return RedirectToAction("MyProducts", "ControlPanel");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [SameVendorProduct]
        public ActionResult SelectFeaturedAttributesByPlan(int id, FeaturedAttributesByPlanRequest command, SelectFeaturedAttributesByPlanModel model, Product product)
        {

            //var product = _productService.GetProductById(id);
            //if (product == null)
            //    return HttpNotFound();

            try
            {
                //Procesa las validaciones y carga nuevamente el modelo
                PrepareSelectFeaturedAttributesByPlanModel(id, command, product, model);
            }
            catch (NopException e)
            {
                return RedirectToAction("MyProducts", "ControlPanel");
            }


            //Si el producto lo quiere seleccionar destacado en el home
            //lo actualiza asi como con las demás propiedades
            //Si se está activando el producto en esta caracteristica
            //Se debe validar que pueda hacerlo, sino no se actualiza
            if (model.SelectOnHome && !product.ShowOnHomePage)
            {
                if (model.NumProductsOnHomeLeft > 0)
                    product.ShowOnHomePage = true;
            }
            else
                product.ShowOnHomePage = model.SelectOnHome;


            if (model.SelectOnSocialNetworks && !product.SocialNetworkFeatured)
            {
                if (model.NumProductsOnSocialNetworksLeft > 0)
                    product.SocialNetworkFeatured = true;
            }
            else
                product.SocialNetworkFeatured = model.SelectOnSocialNetworks;


            //Recorreo las categorias y las marcas y las deja como destacadas
            bool activateSliders = false;
            if (model.SelectOnSliders && !product.FeaturedForSliders)
            {
                activateSliders = model.NumProductsOnSlidersLeft > 0;
            }
            else
                activateSliders = model.SelectOnSliders;


            foreach (var category in product.ProductCategories)
            {
                category.IsFeaturedProduct = activateSliders;
                _eventPublisher.EntityUpdated(category);
            }

            foreach (var manufacturer in product.ProductManufacturers)
            {
                manufacturer.IsFeaturedProduct = activateSliders;
                _eventPublisher.EntityUpdated(manufacturer);
            }

            product.FeaturedForSliders = activateSliders;


            //Actualiza los valores seleccionados por el usuario
            _productService.UpdateProduct(product);

            //Si viene de la publicación confirma la publicación
            if (string.Equals(command.from, "publish"))
                return RedirectToAction("ConfirmationWithoutPlan", "Sales", new { id = id });
            else
                return RedirectToAction("MyProducts", "ControlPanel");
        }


        public void PrepareSelectFeaturedAttributesByPlanModel(int id, FeaturedAttributesByPlanRequest command, Product product, SelectFeaturedAttributesByPlanModel model)
        {
            Order order = null;
            bool validatePlan = true;

            //Si la llave NO viene nula significa que debe validarla. 
            if (!string.IsNullOrEmpty(command.sign))
            {
                order = _orderService.GetOrderById(command.orderId.Value);
                validatePlan = false;

                //Si llega ser invalida retorna al usuario al panel de control
                if (!_orderProcessingService.GetPaymentPlanValidationKeys(order, id).Equals(command.sign))
                    throw new NopException("MyProducts");
            }
            else
            {

                model.ShowGoToMyProductsButton = true;
                //en el caso contrario debe validar que el usuario tenga un plan comprado valido
                order = _workContext.CurrentVendor.CurrentOrderPlan;

                int limit;
                model.HasReachedLimitOfProducts = _productService.HasReachedLimitOfProducts(_workContext.CurrentVendor, out limit);
                model.NumLimitOfProducts = limit;

                //La orden asociada al vendedor debe ser una orden paga  y finalizada
                //No debe estar vencido el plan en el vendor
                if (order == null
                    || order.PaymentStatus != Core.Domain.Payments.PaymentStatus.Paid
                    || order.OrderStatus != OrderStatus.Complete
                    || _workContext.CurrentVendor.PlanExpiredOnUtc < DateTime.UtcNow
                    )
                {
                    throw new NopException("NoPlanSelected");
                }
            }

            var selectedPlan = order.OrderItems.FirstOrDefault().Product;

            //Valida cuantos productos les queda disponibles para destacar en cada uno de las caraceteristicas
            var leftProductsOnPlan = _productService.CountLeftFeaturedPlacesByVendor(product, validatePlan, order);
            model.ProductDetails = PrepareProductOverviewModels(new List<Product>() { product }).First();


            //Valida que tenga productos en el home
            model.NumProductsOnHomeLeft = leftProductsOnPlan.HomePageLeft;
            model.NumProductsOnHomeByPlan = leftProductsOnPlan.HomePageByPlan;

            //valida productos en redes sociales
            model.NumProductsOnSocialNetworksLeft = leftProductsOnPlan.SocialNetworkLeft;
            model.NumProductsOnSocialNetworksByPlan = leftProductsOnPlan.SocialNetworkByPlan;

            //Valida productos en sliders
            model.NumProductsOnSlidersLeft = leftProductsOnPlan.SlidersLeft;
            model.NumProductsOnSlidersByPlan = leftProductsOnPlan.SlidersByPlan;


            model.HasReachedLimitOfFeature = model.NumProductsOnSlidersLeft + model.NumProductsOnSocialNetworksLeft + model.NumProductsOnHomeLeft <= 0;

            model.ShowFinishPublishingButton = command.from == "publish";
        }

        #endregion
    }
}
