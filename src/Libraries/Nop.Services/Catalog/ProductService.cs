using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Media;
using Nop.Services.Vendors;
using Nop.Services.Orders;
using Nop.Services.Logging;
using Nop.Core.Domain.Vendors;
using Nop.Core.Domain.Media;
using Nop.Services.Common;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Product service
    /// </summary>
    public partial class ProductService : IProductService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        private const string PRODUCTS_BY_ID_KEY = "Nop.product.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTS_PATTERN_KEY = "Nop.product.";

        /// <summary>
        /// Key para guardar el detalle de un plan
        /// </summary>
        private const string PRODUCTS_PLAN_PATTERN = "Nop.product.";
        /// <summary>
        /// Key para guardar el detalle de un plan
        /// </summary>
        private const string PRODUCTS_PLAN_BY_ID_KEY = "Nop.product.{0}";
        #endregion

        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<RelatedProduct> _relatedProductRepository;
        private readonly IRepository<CrossSellProduct> _crossSellProductRepository;
        private readonly IRepository<TierPrice> _tierPriceRepository;
        private readonly IRepository<LocalizedProperty> _localizedPropertyRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IRepository<ProductPicture> _productPictureRepository;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeRepository;
        private readonly IRepository<ProductReview> _productReviewRepository;
        private readonly IRepository<ProductWarehouseInventory> _productWarehouseInventoryRepository;
        private readonly IRepository<ProductQuestion> _productQuestionRepository;
        private readonly IRepository<SpecialCategoryProduct> _specialCategoryProductRepository;
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly ILanguageService _languageService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CommonSettings _commonSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly TuilsSettings _tuilsSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPictureService _pictureService;
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly ICategoryService _categoryService;
        private readonly ILocalizationService _localizationService;
        private readonly PlanSettings _planSettings;
        


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="relatedProductRepository">Related product repository</param>
        /// <param name="crossSellProductRepository">Cross-sell product repository</param>
        /// <param name="tierPriceRepository">Tier price repository</param>
        /// <param name="localizedPropertyRepository">Localized property repository</param>
        /// <param name="aclRepository">ACL record repository</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="productPictureRepository">Product picture repository</param>
        /// <param name="productSpecificationAttributeRepository">Product specification attribute repository</param>
        /// <param name="productReviewRepository">Product review repository</param>
        /// <param name="productWarehouseInventoryRepository">Product warehouse inventory repository</param>
        /// <param name="productAttributeService">Product attribute service</param>
        /// <param name="productAttributeParser">Product attribute parser service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="workflowMessageService">Workflow message service</param>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbContext">Database Context</param>
        /// <param name="workContext">Work context</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="localizationSettings">Localization settings</param>
        /// <param name="commonSettings">Common settings</param>
        /// <param name="catalogSettings">Catalog settings</param>
        /// <param name="eventPublisher">Event published</param>
        /// <param name="aclService">ACL service</param>
        /// <param name="storeMappingService">Store mapping service</param>
        public ProductService(ICacheManager cacheManager,
            IRepository<Product> productRepository,
            IRepository<RelatedProduct> relatedProductRepository,
            IRepository<CrossSellProduct> crossSellProductRepository,
            IRepository<TierPrice> tierPriceRepository,
            IRepository<ProductPicture> productPictureRepository,
            IRepository<LocalizedProperty> localizedPropertyRepository,
            IRepository<AclRecord> aclRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository,
            IRepository<ProductReview> productReviewRepository,
            IRepository<ProductWarehouseInventory> productWarehouseInventoryRepository,
            IProductAttributeService productAttributeService,
            IProductAttributeParser productAttributeParser,
            ILanguageService languageService,
            IWorkflowMessageService workflowMessageService,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IWorkContext workContext,
            IStoreContext storeContext,
            LocalizationSettings localizationSettings,
            CommonSettings commonSettings,
            CatalogSettings catalogSettings,
            IEventPublisher eventPublisher,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IPictureService pictureService,
            IRepository<ProductQuestion> productQuestionRepository,
            TuilsSettings tuilsSettings,
            IOrderService orderService,
            IRepository<SpecialCategoryProduct> specialCategoryProductRepository,
            ILogger logger,
            ICategoryService categoryService,
            ILocalizationService localizacionService,
            IRepository<Vendor> vendorRepository,
            PlanSettings planSettings)
        {
            this._cacheManager = cacheManager;
            this._productRepository = productRepository;
            this._relatedProductRepository = relatedProductRepository;
            this._crossSellProductRepository = crossSellProductRepository;
            this._tierPriceRepository = tierPriceRepository;
            this._productPictureRepository = productPictureRepository;
            this._localizedPropertyRepository = localizedPropertyRepository;
            this._aclRepository = aclRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._productSpecificationAttributeRepository = productSpecificationAttributeRepository;
            this._productReviewRepository = productReviewRepository;
            this._productWarehouseInventoryRepository = productWarehouseInventoryRepository;
            this._productAttributeService = productAttributeService;
            this._productAttributeParser = productAttributeParser;
            this._languageService = languageService;
            this._workflowMessageService = workflowMessageService;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._localizationSettings = localizationSettings;
            this._commonSettings = commonSettings;
            this._catalogSettings = catalogSettings;
            this._eventPublisher = eventPublisher;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._pictureService = pictureService;
            this._productQuestionRepository = productQuestionRepository;
            this._tuilsSettings = tuilsSettings;
            this._orderService = orderService;
            this._specialCategoryProductRepository = specialCategoryProductRepository;
            this._logger = logger;
            this._categoryService = categoryService;
            this._localizationService = localizacionService;
            this._vendorRepository = vendorRepository;
            this._planSettings = planSettings;
        }

        #endregion

        #region Methods

        #region Products

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.Deleted = true;
            //delete product
            UpdateProduct(product);
        }

        /// <summary>
        /// Gets all products displayed on the home page
        /// </summary>
        /// <returns>Product collection</returns>
        public virtual IList<Product> GetAllProductsDisplayedOnHomePage()
        {
            var query = from p in _productRepository.Table
                        orderby p.DisplayOrder, p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnHomePage
                        select p;
            var products = query.ToList();
            return products;
        }

        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual Product GetProductById(int productId)
        {
            if (productId == 0)
                return null;

            string key = string.Format(PRODUCTS_BY_ID_KEY, productId);
            return _cacheManager.Get(key, () => _productRepository.GetById(productId));
        }

        /// <summary>
        /// Get products by identifiers
        /// </summary>
        /// <param name="productIds">Product identifiers</param>
        /// <returns>Products</returns>
        public virtual IList<Product> GetProductsByIds(int[] productIds)
        {
            if (productIds == null || productIds.Length == 0)
                return new List<Product>();

            var query = from p in _productRepository.Table
                        where productIds.Contains(p.Id)
                        select p;
            var products = query.ToList();
            //sort by passed identifiers
            var sortedProducts = new List<Product>();
            foreach (int id in productIds)
            {
                var product = products.Find(x => x.Id == id);
                if (product != null)
                    sortedProducts.Add(product);
            }
            return sortedProducts;
        }

        /// <summary>
        /// Inserts a product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //insert
            _productRepository.Insert(product);

            //clear cache
            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(product);
        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //update
            _productRepository.Update(product);

            //cache
            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(product);
        }

        /// <summary>
        /// Get (visible) product number in certain category
        /// </summary>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <returns>Product number</returns>
        public virtual int GetCategoryProductNumber(IList<int> categoryIds = null, int storeId = 0)
        {
            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            var query = _productRepository.Table;
            query = query.Where(p => !p.Deleted && p.Published && p.VisibleIndividually);

            //category filtering
            if (categoryIds != null && categoryIds.Count > 0)
            {
                query = from p in query
                        from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                        select p;
            }

            if (!_catalogSettings.IgnoreAcl)
            {
                //Access control list. Allowed customer roles
                var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                    .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

                query = from p in query
                        join acl in _aclRepository.Table
                        on new { c1 = p.Id, c2 = "Product" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into p_acl
                        from acl in p_acl.DefaultIfEmpty()
                        where !p.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                        select p;
            }

            if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
            {
                query = from p in query
                        join sm in _storeMappingRepository.Table
                        on new { c1 = p.Id, c2 = "Product" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into p_sm
                        from sm in p_sm.DefaultIfEmpty()
                        where !p.LimitedToStores || storeId == sm.StoreId
                        select p;
            }

            //only distinct products
            var result = query.Select(p => p.Id).Distinct().Count();
            return result;
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="warehouseId">Warehouse identifier; 0 to load all records</param>
        /// <param name="parentGroupedProductId">Parent product identifier (used with grouped products); 0 to load all records</param>
        /// <param name="productType">Product type; 0 to load all records</param>
        /// <param name="visibleIndividuallyOnly">A values indicating whether to load only products marked as "visible individually"; "false" to load all records; "true" to load "visible individually" only</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="productTagId">Product tag identifier; 0 to load all records</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="searchDescriptions">A value indicating whether to search by a specified "keyword" in product descriptions</param>
        /// <param name="searchSku">A value indicating whether to search by a specified "keyword" in product SKU</param>
        /// <param name="searchProductTags">A value indicating whether to search by a specified "keyword" in product tags</param>
        /// <param name="languageId">Language identifier (search for text searching)</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual IPagedList<Product> SearchProducts(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            int parentGroupedProductId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? published = null,
            int? specialCategoryId = null,
            int? orderBySpecialCategoryId = null,
           int? stateProvinceId = null,
            bool? leftFeatured = null,
             bool? sold = null,
            bool hidden = false,
            bool? showOnHomePage = null,
            bool? showOnSliders = null,
            bool? showOnSocialNetworks = null)
        {
            Dictionary<int, int> filterableSpecificationAttributeOptionCount;
            return SearchProducts(
                out filterableSpecificationAttributeOptionCount, false,
                pageIndex, pageSize, categoryIds, manufacturerId,
                storeId, vendorId, warehouseId,
                parentGroupedProductId, productType, visibleIndividuallyOnly, featuredProducts,
                priceMin, priceMax, productTagId, keywords, searchDescriptions, searchSku,
                searchProductTags, languageId, filteredSpecs, orderBy, showHidden, published, 
                specialCategoryId, orderBySpecialCategoryId, stateProvinceId, leftFeatured, sold, hidden,
                showOnHomePage, showOnSliders, showOnSocialNetworks);
        }


        public virtual IPagedList<Product> SearchProducts(
            out Dictionary<int, int> filterableSpecificationAttributeOptionCount,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            int parentGroupedProductId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? published = null,
            int? specialCategoryId = null,
            int? orderBySpecialCategoryId = null,
           int? stateProvinceId = null,
            bool? leftFeatured = null,
             bool? sold = null,
            bool hidden = false,
            bool? showOnHomePage = null,
            bool? showOnSliders = null,
            bool? showOnSocialNetworks = null)
        {
            Dictionary<int, int> filterableCategoryCount;
            Dictionary<int, int> filterableStateProvinceCount;
            Dictionary<int, int> filterableManufacturerCount;
            Dictionary<int, int> filterableSpecialCategoryCount;
            Tuple<int, int> minMaxPrice;
            return SearchProducts(
                out filterableSpecificationAttributeOptionCount,
                out filterableCategoryCount,
                out filterableStateProvinceCount,
                out filterableManufacturerCount,
                out filterableSpecialCategoryCount,
                out minMaxPrice,
                loadFilterableSpecificationAttributeOptionIds,
                false, false, false, false,
                pageIndex, pageSize, categoryIds, manufacturerId,
                storeId, vendorId, warehouseId,
                parentGroupedProductId, productType, visibleIndividuallyOnly, featuredProducts,
                priceMin, priceMax, productTagId, keywords, searchDescriptions, searchSku,
                searchProductTags, languageId, filteredSpecs, orderBy, showHidden, published,
                stateProvinceId, specialCategoryId, orderBySpecialCategoryId, false, leftFeatured, sold, hidden,
                showOnHomePage, showOnSliders, showOnSocialNetworks);
        }


        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="filterableSpecificationAttributeOptionIds">The specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="loadFilterableSpecificationAttributeOptionIds">A value indicating whether we should load the specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="filterableCategoryCount">Listado de categorias que se encontraron en el filtro con el numero de productos por cada una</param>
        /// <param name="filterableSpecificationAttributeOptionCount">Especificaciones que se econtraron seg�n el filtro y el conteo respectivo de cada una</param>
        /// <param name="filterableStateProvinceCount">Ciudades que se encontraron en el filtro y el conteo respectivo de cada una de los prodcutos segpun el filtro</param>
        /// <param name="filterableManufacturerCount">Marcas que se encontraron en el filtro y el conteo respectivo de cada una de las marcas seg�n filotro</param>
        /// <param name="filterableSpecialCategoryCount">Categorias especiales que se encontraron en el filtro y el conteo respectivo de cada una de las categorias especiales seg�n filotro</param>
        /// <param name="loadFilterableManufacturerIds"></param>
        /// <param name="loadFilterableCategoryIds">True: contar categorias</param>
        /// <param name="loadFilterableStateProvinceIds">True: Contar ciudades</param>
        /// <param name="loadPriceRange">True: Carga el menor y el mayor precio del filtro</param>
        /// <param name="minMaxPrice">Tupple con el menor (obj0) y el mayor(obj1) precio</param>
        /// <param name="stateProvinceId">filtro de ciudad</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="warehouseId">Warehouse identifier; 0 to load all records</param>
        /// <param name="parentGroupedProductId">Parent product identifier (used with grouped products); 0 to load all records</param>
        /// <param name="productType">Product type; 0 to load all records</param>
        /// <param name="visibleIndividuallyOnly">A values indicating whether to load only products marked as "visible individually"; "false" to load all records; "true" to load "visible individually" only</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="productTagId">Product tag identifier; 0 to load all records</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="searchDescriptions">A value indicating whether to search by a specified "keyword" in product descriptions</param>
        /// <param name="searchSku">A value indicating whether to search by a specified "keyword" in product SKU</param>
        /// <param name="searchProductTags">A value indicating whether to search by a specified "keyword" in product tags</param>
        /// <param name="languageId">Language identifier (search for text searching)</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="published">Si viene null no filtra por el campo Published. Si no viene null si filtra por el campo dependiendo de su valor</param>
        /// <param name="hidden">Campo Hidden de la tabla Product</param>
        /// <returns>Products</returns>
        public virtual IPagedList<Product> SearchProducts(
            out Dictionary<int, int> filterableSpecificationAttributeOptionCount,
            out Dictionary<int, int> filterableCategoryCount,
            out Dictionary<int, int> filterableStateProvinceCount,
            out Dictionary<int, int> filterableManufacturerCount,
            out Dictionary<int, int> filterableSpecialCategoryCount,
            out Tuple<int, int> minMaxPrice,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            bool loadFilterableCategoryIds = false,
            bool loadFilterableStateProvinceIds = false,
            bool loadFilterableManufacturerIds = false,
            bool loadFilterableSpecialCategoryIds = false,
            int pageIndex = 0,
            int pageSize = 2147483647,  //Int32.MaxValue
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            int parentGroupedProductId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? published = null,
            int? stateProvinceId = null,
            int? specialCategoryId = null,
            int? orderBySpecialCategoryId = null,
            bool? loadPriceRange = false,
            bool? leftFeatured = null,
             bool? sold = null,
            bool hidden = false,
            bool? showOnHomePage = null,
            bool? showOnSliders = null,
            bool? showOnSocialNetworks = null)
        {
            filterableSpecificationAttributeOptionCount = new Dictionary<int, int>();
            filterableCategoryCount = new Dictionary<int, int>();
            filterableStateProvinceCount = new Dictionary<int, int>();
            filterableManufacturerCount = new Dictionary<int, int>();
            filterableSpecialCategoryCount = new Dictionary<int, int>();

            //search by keyword
            bool searchLocalizedValue = false;
            if (languageId > 0)
            {
                if (showHidden)
                {
                    searchLocalizedValue = true;
                }
                else
                {
                    //ensure that we have at least two published languages
                    var totalPublishedLanguages = _languageService.GetAllLanguages(storeId: _storeContext.CurrentStore.Id).Count;
                    searchLocalizedValue = totalPublishedLanguages >= 2;
                }
            }

            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            //Access control list. Allowed customer roles
            var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

            //if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
            //{
            //stored procedures are enabled and supported by the database. 
            //It's much faster than the LINQ implementation below 

            #region Use stored procedure

            //pass category identifiers as comma-delimited string
            string commaSeparatedCategoryIds = "";
            if (categoryIds != null)
            {
                for (int i = 0; i < categoryIds.Count; i++)
                {
                    commaSeparatedCategoryIds += categoryIds[i].ToString();
                    if (i != categoryIds.Count - 1)
                    {
                        commaSeparatedCategoryIds += ",";
                    }
                }
            }


            //pass customer role identifiers as comma-delimited string
            string commaSeparatedAllowedCustomerRoleIds = "";
            for (int i = 0; i < allowedCustomerRolesIds.Count; i++)
            {
                commaSeparatedAllowedCustomerRoleIds += allowedCustomerRolesIds[i].ToString();
                if (i != allowedCustomerRolesIds.Count - 1)
                {
                    commaSeparatedAllowedCustomerRoleIds += ",";
                }
            }


            //pass specification identifiers as comma-delimited string
            string commaSeparatedSpecIds = "";
            if (filteredSpecs != null)
            {
                ((List<int>)filteredSpecs).Sort();
                for (int i = 0; i < filteredSpecs.Count; i++)
                {
                    commaSeparatedSpecIds += filteredSpecs[i].ToString();
                    if (i != filteredSpecs.Count - 1)
                    {
                        commaSeparatedSpecIds += ",";
                    }
                }
            }

            //some databases don't support int.MaxValue
            if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            //prepare parameters
            var pCategoryIds = _dataProvider.GetParameter();
            pCategoryIds.ParameterName = "CategoryIds";
            pCategoryIds.Value = commaSeparatedCategoryIds != null ? (object)commaSeparatedCategoryIds : DBNull.Value;
            pCategoryIds.DbType = DbType.String;

            var pManufacturerId = _dataProvider.GetParameter();
            pManufacturerId.ParameterName = "ManufacturerId";
            pManufacturerId.Value = manufacturerId;
            pManufacturerId.DbType = DbType.Int32;

            var pStoreId = _dataProvider.GetParameter();
            pStoreId.ParameterName = "StoreId";
            pStoreId.Value = !_catalogSettings.IgnoreStoreLimitations ? storeId : 0;
            pStoreId.DbType = DbType.Int32;

            var pVendorId = _dataProvider.GetParameter();
            pVendorId.ParameterName = "VendorId";
            pVendorId.Value = vendorId;
            pVendorId.DbType = DbType.Int32;

            var pWarehouseId = _dataProvider.GetParameter();
            pWarehouseId.ParameterName = "WarehouseId";
            pWarehouseId.Value = warehouseId;
            pWarehouseId.DbType = DbType.Int32;

            var pParentGroupedProductId = _dataProvider.GetParameter();
            pParentGroupedProductId.ParameterName = "ParentGroupedProductId";
            pParentGroupedProductId.Value = parentGroupedProductId;
            pParentGroupedProductId.DbType = DbType.Int32;

            var pProductTypeId = _dataProvider.GetParameter();
            pProductTypeId.ParameterName = "ProductTypeId";
            pProductTypeId.Value = productType.HasValue ? (object)productType.Value : DBNull.Value;
            pProductTypeId.DbType = DbType.Int32;

            var pVisibleIndividuallyOnly = _dataProvider.GetParameter();
            pVisibleIndividuallyOnly.ParameterName = "VisibleIndividuallyOnly";
            pVisibleIndividuallyOnly.Value = visibleIndividuallyOnly;
            pVisibleIndividuallyOnly.DbType = DbType.Int32;

            var pProductTagId = _dataProvider.GetParameter();
            pProductTagId.ParameterName = "ProductTagId";
            pProductTagId.Value = productTagId;
            pProductTagId.DbType = DbType.Int32;

            var pFeaturedProducts = _dataProvider.GetParameter();
            pFeaturedProducts.ParameterName = "FeaturedProducts";
            pFeaturedProducts.Value = featuredProducts.HasValue ? (object)featuredProducts.Value : DBNull.Value;
            pFeaturedProducts.DbType = DbType.Boolean;

            var pPriceMin = _dataProvider.GetParameter();
            pPriceMin.ParameterName = "PriceMin";
            pPriceMin.Value = priceMin.HasValue ? (object)priceMin.Value : DBNull.Value;
            pPriceMin.DbType = DbType.Decimal;

            var pPriceMax = _dataProvider.GetParameter();
            pPriceMax.ParameterName = "PriceMax";
            pPriceMax.Value = priceMax.HasValue ? (object)priceMax.Value : DBNull.Value;
            pPriceMax.DbType = DbType.Decimal;

            var pKeywords = _dataProvider.GetParameter();
            pKeywords.ParameterName = "Keywords";
            pKeywords.Value = keywords != null ? (object)keywords : DBNull.Value;
            pKeywords.DbType = DbType.String;

            var pSearchDescriptions = _dataProvider.GetParameter();
            pSearchDescriptions.ParameterName = "SearchDescriptions";
            pSearchDescriptions.Value = searchDescriptions;
            pSearchDescriptions.DbType = DbType.Boolean;

            var pSearchSku = _dataProvider.GetParameter();
            pSearchSku.ParameterName = "SearchSku";
            pSearchSku.Value = searchSku;
            pSearchSku.DbType = DbType.Boolean;

            var pSearchProductTags = _dataProvider.GetParameter();
            pSearchProductTags.ParameterName = "SearchProductTags";
            pSearchProductTags.Value = searchProductTags;
            pSearchProductTags.DbType = DbType.Boolean;

            var pUseFullTextSearch = _dataProvider.GetParameter();
            pUseFullTextSearch.ParameterName = "UseFullTextSearch";
            pUseFullTextSearch.Value = _commonSettings.UseFullTextSearch;
            pUseFullTextSearch.DbType = DbType.Boolean;

            var pFullTextMode = _dataProvider.GetParameter();
            pFullTextMode.ParameterName = "FullTextMode";
            pFullTextMode.Value = (int)_commonSettings.FullTextMode;
            pFullTextMode.DbType = DbType.Int32;

            var pFilteredSpecs = _dataProvider.GetParameter();
            pFilteredSpecs.ParameterName = "FilteredSpecs";
            pFilteredSpecs.Value = commaSeparatedSpecIds != null ? (object)commaSeparatedSpecIds : DBNull.Value;
            pFilteredSpecs.DbType = DbType.String;

            var pLanguageId = _dataProvider.GetParameter();
            pLanguageId.ParameterName = "LanguageId";
            pLanguageId.Value = searchLocalizedValue ? languageId : 0;
            pLanguageId.DbType = DbType.Int32;

            var pOrderBy = _dataProvider.GetParameter();
            pOrderBy.ParameterName = "OrderBy";
            //Si viene para ordenar por categoria especial el orden cambia de tipo
            pOrderBy.Value = orderBySpecialCategoryId.HasValue ? (int)ProductSortingEnum.SpecialCategory : (int)orderBy;
            pOrderBy.DbType = DbType.Int32;

            var pAllowedCustomerRoleIds = _dataProvider.GetParameter();
            pAllowedCustomerRoleIds.ParameterName = "AllowedCustomerRoleIds";
            pAllowedCustomerRoleIds.Value = commaSeparatedAllowedCustomerRoleIds;
            pAllowedCustomerRoleIds.DbType = DbType.String;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            var pShowHidden = _dataProvider.GetParameter();
            pShowHidden.ParameterName = "ShowHidden";
            pShowHidden.Value = showHidden;
            pShowHidden.DbType = DbType.Boolean;


            var pHidden = _dataProvider.GetParameter();
            pHidden.ParameterName = "hidden";
            pHidden.Value = hidden;
            pHidden.DbType = DbType.Boolean;


            var pShowOnHomePage = _dataProvider.GetParameter();
            pShowOnHomePage.ParameterName = "showOnHomePage";
            pShowOnHomePage.Value = showOnHomePage.HasValue ? (object)showOnHomePage : DBNull.Value;
            pShowOnHomePage.DbType = DbType.Boolean;

            var pShowOnSliders = _dataProvider.GetParameter();
            pShowOnSliders.ParameterName = "showOnSliders";
            pShowOnSliders.Value = showOnSliders.HasValue ? (object)showOnSliders : DBNull.Value;
            pShowOnSliders.DbType = DbType.Boolean;

            var pShowOnSocialNetworks = _dataProvider.GetParameter();
            pShowOnSocialNetworks.ParameterName = "ShowOnSocialNetworks";
            pShowOnSocialNetworks.Value = showOnSocialNetworks.HasValue ? (object)showOnSocialNetworks : DBNull.Value;
            pShowOnSocialNetworks.DbType = DbType.Boolean;


            var pPublished = _dataProvider.GetParameter();
            pPublished.ParameterName = "Published";
            pPublished.Value = published != null ? (object)published : DBNull.Value;
            pPublished.DbType = DbType.Boolean;


            var pSold= _dataProvider.GetParameter();
            pSold.ParameterName = "Sold";
            pSold.Value = sold != null ? (object)sold : DBNull.Value;
            pSold.DbType = DbType.Boolean;


            var pStateProvinceId = _dataProvider.GetParameter();
            pStateProvinceId.ParameterName = "StateProvinceId";
            pStateProvinceId.Value = stateProvinceId != null ? (object)stateProvinceId : DBNull.Value;
            pStateProvinceId.DbType = DbType.Int32;


            var pSpecialCategoryId = _dataProvider.GetParameter();
            pSpecialCategoryId.ParameterName = "SpecialCategoryId";
            pSpecialCategoryId.Value = specialCategoryId != null ? (object)specialCategoryId : DBNull.Value;
            pSpecialCategoryId.DbType = DbType.Int32;

            var pOrderBySpecialCategoryId = _dataProvider.GetParameter();
            pOrderBySpecialCategoryId.ParameterName = "OrderBySpecialCategoryId";
            pOrderBySpecialCategoryId.Value = orderBySpecialCategoryId != null ? (object)orderBySpecialCategoryId : DBNull.Value;
            pOrderBySpecialCategoryId.DbType = DbType.Int32;


            var pLoadPriceRange = _dataProvider.GetParameter();
            pLoadPriceRange.ParameterName = "LoadPriceRange";
            pLoadPriceRange.Value = loadPriceRange;
            pLoadPriceRange.DbType = DbType.Boolean;


            var pLoadFilterableCategoryIds = _dataProvider.GetParameter();
            pLoadFilterableCategoryIds.ParameterName = "LoadFilterableCategoryIds";
            pLoadFilterableCategoryIds.Value = loadFilterableCategoryIds;
            pLoadFilterableCategoryIds.DbType = DbType.Boolean;


            var pLoadFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
            pLoadFilterableSpecificationAttributeOptionIds.ParameterName = "LoadFilterableSpecificationAttributeOptionIds";
            pLoadFilterableSpecificationAttributeOptionIds.Value = loadFilterableSpecificationAttributeOptionIds;
            pLoadFilterableSpecificationAttributeOptionIds.DbType = DbType.Boolean;

            var pLoadFilterableStateProvinceIds = _dataProvider.GetParameter();
            pLoadFilterableStateProvinceIds.ParameterName = "LoadFilterableStateProvinceIds";
            pLoadFilterableStateProvinceIds.Value = loadFilterableStateProvinceIds;
            pLoadFilterableStateProvinceIds.DbType = DbType.Boolean;

            var pLoadFilterableManufacturerIds = _dataProvider.GetParameter();
            pLoadFilterableManufacturerIds.ParameterName = "LoadFilterableManufacturerIds";
            pLoadFilterableManufacturerIds.Value = loadFilterableManufacturerIds;
            pLoadFilterableManufacturerIds.DbType = DbType.Boolean;

            var pLoadFilterableSpecialCategoryIds = _dataProvider.GetParameter();
            pLoadFilterableSpecialCategoryIds.ParameterName = "LoadFilterableSpecialCategoryIds";
            pLoadFilterableSpecialCategoryIds.Value = loadFilterableSpecialCategoryIds;
            pLoadFilterableSpecialCategoryIds.DbType = DbType.Boolean;

            var pLeftFeatured = _dataProvider.GetParameter();
            pLeftFeatured.ParameterName = "LeftFeatured";
            pLeftFeatured.Value = leftFeatured.HasValue ? (object)leftFeatured : DBNull.Value;
            pLeftFeatured.DbType = DbType.Boolean;


            var pFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
            pFilterableSpecificationAttributeOptionIds.ParameterName = "FilterableSpecificationAttributeOptionIds";
            pFilterableSpecificationAttributeOptionIds.Direction = ParameterDirection.Output;
            pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
            pFilterableSpecificationAttributeOptionIds.DbType = DbType.String;

            var pFilterableCategoryIds = _dataProvider.GetParameter();
            pFilterableCategoryIds.ParameterName = "FilterableCategoryIds";
            pFilterableCategoryIds.Direction = ParameterDirection.Output;
            pFilterableCategoryIds.Size = int.MaxValue - 1;
            pFilterableCategoryIds.DbType = DbType.String;

            var pFilterableStateProvinceIds = _dataProvider.GetParameter();
            pFilterableStateProvinceIds.ParameterName = "FilterableStateProvinceIds";
            pFilterableStateProvinceIds.Direction = ParameterDirection.Output;
            pFilterableStateProvinceIds.Size = int.MaxValue - 1;
            pFilterableStateProvinceIds.DbType = DbType.String;

            var pFilterableManufacturerIds = _dataProvider.GetParameter();
            pFilterableManufacturerIds.ParameterName = "FilterableManufacturerIds";
            pFilterableManufacturerIds.Direction = ParameterDirection.Output;
            pFilterableManufacturerIds.Size = int.MaxValue - 1;
            pFilterableManufacturerIds.DbType = DbType.String;

            var pFilterableSpecialCategoryIds = _dataProvider.GetParameter();
            pFilterableSpecialCategoryIds.ParameterName = "FilterableSpecialCategoryIds";
            pFilterableSpecialCategoryIds.Direction = ParameterDirection.Output;
            pFilterableSpecialCategoryIds.Size = int.MaxValue - 1;
            pFilterableSpecialCategoryIds.DbType = DbType.String;

            var pMinPrice = _dataProvider.GetParameter();
            pMinPrice.ParameterName = "MinPrice";
            pMinPrice.Direction = ParameterDirection.Output;
            pMinPrice.Size = int.MaxValue - 1;
            pMinPrice.DbType = DbType.Int32;

            var pMaxPrice = _dataProvider.GetParameter();
            pMaxPrice.ParameterName = "MaxPrice";
            pMaxPrice.Direction = ParameterDirection.Output;
            pMaxPrice.Size = int.MaxValue - 1;
            pMaxPrice.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecords";
            pTotalRecords.Direction = ParameterDirection.Output;
            pTotalRecords.DbType = DbType.Int32;


            //invoke stored procedure
            var products = _dbContext.ExecuteStoredProcedureList<Product>(
                "ProductLoadAllPaged",
                pCategoryIds,
                pManufacturerId,
                pStoreId,
                pVendorId,
                pWarehouseId,
                pParentGroupedProductId,
                pProductTypeId,
                pVisibleIndividuallyOnly,
                pProductTagId,
                pFeaturedProducts,
                pPriceMin,
                pPriceMax,
                pKeywords,
                pSearchDescriptions,
                pSearchSku,
                pSearchProductTags,
                pUseFullTextSearch,
                pFullTextMode,
                pFilteredSpecs,
                pLanguageId,
                pOrderBy,
                pAllowedCustomerRoleIds,
                pPageIndex,
                pPageSize,
                pShowHidden,
                pHidden,
                pShowOnHomePage,
                pShowOnSliders,
                pShowOnSocialNetworks,
                pPublished,
                pSold,
                pStateProvinceId,
                pSpecialCategoryId,
                pLeftFeatured,
                pLoadPriceRange,
                pLoadFilterableSpecificationAttributeOptionIds,
                pLoadFilterableCategoryIds,
                pLoadFilterableStateProvinceIds,
                pLoadFilterableManufacturerIds,
                pLoadFilterableSpecialCategoryIds,
                pOrderBySpecialCategoryId,
                pFilterableSpecialCategoryIds,
                pFilterableManufacturerIds,
                pFilterableStateProvinceIds,
                pFilterableCategoryIds,
                pFilterableSpecificationAttributeOptionIds,
                pMinPrice,
                pMaxPrice,
                pTotalRecords);

            //Funcion que convierte un string separado por comas y con el conteo en un Diccionario de enteros
            //La variable que llega de base de datos es algo como 1-20,2-30,1-40   Antes del gui�n significa el id del registro, despu�s del gui�n es el conteo
            Func<bool, System.Data.Common.DbParameter, Dictionary<int, int>> ConvertStringCountToDictionary = delegate(bool filter, System.Data.Common.DbParameter counterStr)
            {
                var result = new Dictionary<int, int>();

                //get filterable specification attribute option identifier
                string filterableIdsStr = (counterStr.Value != DBNull.Value) ? (string)counterStr.Value : string.Empty;
                if (filter &&
                    !string.IsNullOrWhiteSpace(filterableIdsStr))
                {
                    //Separa todas las especificaciones
                    var count = filterableIdsStr
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //Recorre las especificaciones para obtener la cantidad de productos encontrados por cada uno
                    foreach (var spec in count)
                    {
                        var specVsCount = spec.Split(new char[] { '-' });
                        //Posici�n 0 es el id de la especificacion, posicion 2 es la cantidad de productos
                        result.Add(Convert.ToInt32(specVsCount[0]), Convert.ToInt32(specVsCount[1]));
                    }
                }

                return result;
            };

            //Intenta cargar las especificaciones con el respectivo conteo
            filterableSpecificationAttributeOptionCount = ConvertStringCountToDictionary(loadFilterableSpecificationAttributeOptionIds, pFilterableSpecificationAttributeOptionIds);

            //Intenta cargar las categorias con el respectivo conteo
            filterableCategoryCount = ConvertStringCountToDictionary(loadFilterableCategoryIds, pFilterableCategoryIds);

            //Intenta cargar las ciudades con el respectivo conteo
            filterableStateProvinceCount = ConvertStringCountToDictionary(loadFilterableStateProvinceIds, pFilterableStateProvinceIds);

            //Intenta cargar las marcas con el respectivo conteo
            filterableManufacturerCount = ConvertStringCountToDictionary(loadFilterableManufacturerIds, pFilterableManufacturerIds);

            //Intenta cargar las categorias especiales con el respectivo conteo
            filterableSpecialCategoryCount = ConvertStringCountToDictionary(loadFilterableSpecialCategoryIds, pFilterableSpecialCategoryIds);

            //Carga el mayor y menor precio dependiendo de los valores devueltos
            minMaxPrice = Tuple.Create<int, int>(pMinPrice.Value != DBNull.Value ? Convert.ToInt32(pMinPrice.Value) : 0,
                                                 pMaxPrice.Value != DBNull.Value ? Convert.ToInt32(pMaxPrice.Value) : 0);

            //return products
            int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return new PagedList<Product>(products, pageIndex, pageSize, totalRecords);

            #endregion

            #region Codigo Eliminado
            //}

            //else
            //{
            //    //stored procedures aren't supported. Use LINQ
            //    #region Search products

            //    //products
            //    var query = _productRepository.Table;
            //    query = query.Where(p => !p.Deleted);
            //    if (!showHidden)
            //    {
            //        query = query.Where(p => p.Published);
            //    }
            //    if (parentGroupedProductId > 0)
            //    {
            //        query = query.Where(p => p.ParentGroupedProductId == parentGroupedProductId);
            //    }
            //    if (visibleIndividuallyOnly)
            //    {
            //        query = query.Where(p => p.VisibleIndividually);
            //    }
            //    if (productType.HasValue)
            //    {
            //        var productTypeId = (int) productType.Value;
            //        query = query.Where(p => p.ProductTypeId == productTypeId);
            //    }

            //    //The function 'CurrentUtcDateTime' is not supported by SQL Server Compact. 
            //    //That's why we pass the date value
            //    var nowUtc = DateTime.UtcNow;
            //    if (priceMin.HasValue)
            //    {
            //        //min price
            //        query = query.Where(p =>
            //                            //special price (specified price and valid date range)
            //                            ((p.SpecialPrice.HasValue &&
            //                              ((!p.SpecialPriceStartDateTimeUtc.HasValue ||
            //                                p.SpecialPriceStartDateTimeUtc.Value < nowUtc) &&
            //                               (!p.SpecialPriceEndDateTimeUtc.HasValue ||
            //                                p.SpecialPriceEndDateTimeUtc.Value > nowUtc))) &&
            //                             (p.SpecialPrice >= priceMin.Value))
            //                            ||
            //                            //regular price (price isn't specified or date range isn't valid)
            //                            ((!p.SpecialPrice.HasValue ||
            //                              ((p.SpecialPriceStartDateTimeUtc.HasValue &&
            //                                p.SpecialPriceStartDateTimeUtc.Value > nowUtc) ||
            //                               (p.SpecialPriceEndDateTimeUtc.HasValue &&
            //                                p.SpecialPriceEndDateTimeUtc.Value < nowUtc))) &&
            //                             (p.Price >= priceMin.Value)));
            //    }
            //    if (priceMax.HasValue)
            //    {
            //        //max price
            //        query = query.Where(p =>
            //                            //special price (specified price and valid date range)
            //                            ((p.SpecialPrice.HasValue &&
            //                              ((!p.SpecialPriceStartDateTimeUtc.HasValue ||
            //                                p.SpecialPriceStartDateTimeUtc.Value < nowUtc) &&
            //                               (!p.SpecialPriceEndDateTimeUtc.HasValue ||
            //                                p.SpecialPriceEndDateTimeUtc.Value > nowUtc))) &&
            //                             (p.SpecialPrice <= priceMax.Value))
            //                            ||
            //                            //regular price (price isn't specified or date range isn't valid)
            //                            ((!p.SpecialPrice.HasValue ||
            //                              ((p.SpecialPriceStartDateTimeUtc.HasValue &&
            //                                p.SpecialPriceStartDateTimeUtc.Value > nowUtc) ||
            //                               (p.SpecialPriceEndDateTimeUtc.HasValue &&
            //                                p.SpecialPriceEndDateTimeUtc.Value < nowUtc))) &&
            //                             (p.Price <= priceMax.Value)));
            //    }
            //    if (!showHidden)
            //    {
            //        //available dates
            //        query = query.Where(p =>
            //            (!p.AvailableStartDateTimeUtc.HasValue || p.AvailableStartDateTimeUtc.Value < nowUtc) &&
            //            (!p.AvailableEndDateTimeUtc.HasValue || p.AvailableEndDateTimeUtc.Value > nowUtc));
            //    }

            //    //searching by keyword
            //    if (!String.IsNullOrWhiteSpace(keywords))
            //    {
            //        query = from p in query
            //                join lp in _localizedPropertyRepository.Table on p.Id equals lp.EntityId into p_lp
            //                from lp in p_lp.DefaultIfEmpty()
            //                from pt in p.ProductTags.DefaultIfEmpty()
            //                where (p.Name.Contains(keywords)) ||
            //                      (searchDescriptions && p.ShortDescription.Contains(keywords)) ||
            //                      (searchDescriptions && p.FullDescription.Contains(keywords)) ||
            //                      (searchProductTags && pt.Name.Contains(keywords)) ||
            //                      //localized values
            //                      (searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "Name" && lp.LocaleValue.Contains(keywords)) ||
            //                      (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "ShortDescription" && lp.LocaleValue.Contains(keywords)) ||
            //                      (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "FullDescription" && lp.LocaleValue.Contains(keywords))
            //                select p;
            //    }

            //    if (!showHidden && !_catalogSettings.IgnoreAcl)
            //    {
            //        //ACL (access control list)
            //        query = from p in query
            //                join acl in _aclRepository.Table
            //                on new { c1 = p.Id, c2 = "Product" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into p_acl
            //                from acl in p_acl.DefaultIfEmpty()
            //                where !p.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
            //                select p;
            //    }

            //    if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
            //    {
            //        //Store mapping
            //        query = from p in query
            //                join sm in _storeMappingRepository.Table
            //                on new { c1 = p.Id, c2 = "Product" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into p_sm
            //                from sm in p_sm.DefaultIfEmpty()
            //                where !p.LimitedToStores || storeId == sm.StoreId
            //                select p;
            //    }

            //    //search by specs
            //    if (filteredSpecs != null && filteredSpecs.Count > 0)
            //    {
            //        query = from p in query
            //                where !filteredSpecs
            //                           .Except(
            //                               p.ProductSpecificationAttributes.Where(psa => psa.AllowFiltering).Select(
            //                                   psa => psa.SpecificationAttributeOptionId))
            //                           .Any()
            //                select p;
            //    }

            //    //category filtering
            //    if (categoryIds != null && categoryIds.Count > 0)
            //    {
            //        query = from p in query
            //                from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
            //                where (!featuredProducts.HasValue || featuredProducts.Value == pc.IsFeaturedProduct)
            //                select p;
            //    }

            //    //manufacturer filtering
            //    if (manufacturerId > 0)
            //    {
            //        query = from p in query
            //                from pm in p.ProductManufacturers.Where(pm => pm.ManufacturerId == manufacturerId)
            //                where (!featuredProducts.HasValue || featuredProducts.Value == pm.IsFeaturedProduct)
            //                select p;
            //    }

            //    //vendor filtering
            //    if (vendorId > 0)
            //    {
            //        query = query.Where(p => p.VendorId == vendorId);
            //    }

            //    //warehouse filtering
            //    if (warehouseId > 0)
            //    {
            //        var manageStockInventoryMethodId = (int)ManageInventoryMethod.ManageStock;
            //        query = query.Where(p =>
            //            //"Use multiple warehouses" enabled
            //            //we search in each warehouse
            //            (p.ManageInventoryMethodId == manageStockInventoryMethodId &&
            //             p.UseMultipleWarehouses &&
            //             p.ProductWarehouseInventory.Any(pwi => pwi.WarehouseId == warehouseId))
            //            ||
            //            //"Use multiple warehouses" disabled
            //            //we use standard "warehouse" property
            //            ((p.ManageInventoryMethodId != manageStockInventoryMethodId ||
            //              !p.UseMultipleWarehouses) &&
            //              p.WarehouseId == warehouseId));
            //    }

            //    //related products filtering
            //    //if (relatedToProductId > 0)
            //    //{
            //    //    query = from p in query
            //    //            join rp in _relatedProductRepository.Table on p.Id equals rp.ProductId2
            //    //            where (relatedToProductId == rp.ProductId1)
            //    //            select p;
            //    //}

            //    //tag filtering
            //    if (productTagId > 0)
            //    {
            //        query = from p in query
            //                from pt in p.ProductTags.Where(pt => pt.Id == productTagId)
            //                select p;
            //    }

            //    //only distinct products (group by ID)
            //    //if we use standard Distinct() method, then all fields will be compared (low performance)
            //    //it'll not work in SQL Server Compact when searching products by a keyword)
            //    query = from p in query
            //            group p by p.Id
            //            into pGroup
            //            orderby pGroup.Key
            //            select pGroup.FirstOrDefault();

            //    //sort products
            //    if (orderBy == ProductSortingEnum.Position && categoryIds != null && categoryIds.Count > 0)
            //    {
            //        //category position
            //        var firstCategoryId = categoryIds[0];
            //        query = query.OrderBy(p => p.ProductCategories.FirstOrDefault(pc => pc.CategoryId == firstCategoryId).DisplayOrder);
            //    }
            //    else if (orderBy == ProductSortingEnum.Position && manufacturerId > 0)
            //    {
            //        //manufacturer position
            //        query = 
            //            query.OrderBy(p => p.ProductManufacturers.FirstOrDefault(pm => pm.ManufacturerId == manufacturerId).DisplayOrder);
            //    }
            //    else if (orderBy == ProductSortingEnum.Position && parentGroupedProductId > 0)
            //    {
            //        //parent grouped product specified (sort associated products)
            //        query = query.OrderBy(p => p.DisplayOrder);
            //    }
            //    else if (orderBy == ProductSortingEnum.Position)
            //    {
            //        //otherwise sort by name
            //        query = query.OrderBy(p => p.Name);
            //    }
            //    else if (orderBy == ProductSortingEnum.NameAsc)
            //    {
            //        //Name: A to Z
            //        query = query.OrderBy(p => p.Name);
            //    }
            //    else if (orderBy == ProductSortingEnum.NameDesc)
            //    {
            //        //Name: Z to A
            //        query = query.OrderByDescending(p => p.Name);
            //    }
            //    else if (orderBy == ProductSortingEnum.PriceAsc)
            //    {
            //        //Price: Low to High
            //        query = query.OrderBy(p => p.Price);
            //    }
            //    else if (orderBy == ProductSortingEnum.PriceDesc)
            //    {
            //        //Price: High to Low
            //        query = query.OrderByDescending(p => p.Price);
            //    }
            //    else if (orderBy == ProductSortingEnum.CreatedOn)
            //    {
            //        //creation date
            //        query = query.OrderByDescending(p => p.CreatedOnUtc);
            //    }
            //    else
            //    {
            //        //actually this code is not reachable
            //        query = query.OrderBy(p => p.Name);
            //    }

            //    var products = new PagedList<Product>(query, pageIndex, pageSize);

            //    //get filterable specification attribute option identifier
            //    //Se comenta ya que es codigo LINQ que no va ir
            //    //if (loadFilterableSpecificationAttributeOptionIds)
            //    //{
            //    //    var querySpecs = from p in query
            //    //                     join psa in _productSpecificationAttributeRepository.Table on p.Id equals psa.ProductId
            //    //                     where psa.AllowFiltering
            //    //                     select psa.SpecificationAttributeOptionId;
            //    //    //only distinct attributes
            //    //    filterableSpecificationAttributeOptionIds = querySpecs
            //    //        .Distinct()
            //    //        .ToList();
            //    //}
            //    minMaxPrice = Tuple.Create<int, int>(0, 0);
            //    //return products
            //    return products;

            //    #endregion
            //}
            #endregion

        }

        /// <summary>
        /// Gets associated products
        /// </summary>
        /// <param name="parentGroupedProductId">Parent product identifier (used with grouped products)</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual IList<Product> GetAssociatedProducts(int parentGroupedProductId,
            int storeId = 0, bool showHidden = false)
        {
            var query = _productRepository.Table;
            query = query.Where(x => x.ParentGroupedProductId == parentGroupedProductId);
            if (!showHidden)
            {
                query = query.Where(x => x.Published);
            }
            if (!showHidden)
            {
                //The function 'CurrentUtcDateTime' is not supported by SQL Server Compact. 
                //That's why we pass the date value
                var nowUtc = DateTime.UtcNow;
                //available dates
                query = query.Where(p =>
                    (!p.AvailableStartDateTimeUtc.HasValue || p.AvailableStartDateTimeUtc.Value < nowUtc) &&
                    (!p.AvailableEndDateTimeUtc.HasValue || p.AvailableEndDateTimeUtc.Value > nowUtc));
            }
            query = query.Where(x => !x.Deleted);
            query = query.OrderBy(x => x.DisplayOrder);

            var products = query.ToList();

            //ACLmapping
            if (!showHidden)
            {
                products = products.Where(x => _aclService.Authorize(x)).ToList();
            }
            //Store mapping
            if (!showHidden && storeId > 0)
            {
                products = products.Where(x => _storeMappingService.Authorize(x, storeId)).ToList();
            }

            return products;
        }

        /// <summary>
        /// Update product review totals
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateProductReviewTotals(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            int approvedRatingSum = 0;
            int notApprovedRatingSum = 0;
            int approvedTotalReviews = 0;
            int notApprovedTotalReviews = 0;
            var reviews = product.ProductReviews;
            foreach (var pr in reviews)
            {
                if (pr.IsApproved)
                {
                    approvedRatingSum += pr.Rating;
                    approvedTotalReviews++;
                }
                else
                {
                    notApprovedRatingSum += pr.Rating;
                    notApprovedTotalReviews++;
                }
            }

            product.ApprovedRatingSum = approvedRatingSum;
            product.NotApprovedRatingSum = notApprovedRatingSum;
            product.ApprovedTotalReviews = approvedTotalReviews;
            product.NotApprovedTotalReviews = notApprovedTotalReviews;
            UpdateProduct(product);
        }

        /// <summary>
        /// Valida si un cliente tiene pendiente realizar un review sobre un producto
        /// </summary>
        /// <param name="customerId">cliente</param>
        /// <param name="productId">producto</param>
        /// <param name="orderItemId">Retorna el id de la orden pendiente de calificar, si no hay pendiente simplemente devuelve 0</param>
        /// <returns>True: Tiene pendiente review. False: No tiene review Pendiente</returns>
        public virtual bool CustomerHasPendingReviewByProductId(int customerId, int productId, out int orderItemId)
        {
            orderItemId = 0;

            //trae todas las ordenes del usuario sobre ese proyecto
            var orderItem = _orderService.GetAllOrderItems(productId: productId,
                customerId: customerId,
                orderStatus: OrderStatus.Complete)
                .FirstOrDefault();

            //Valida que tenga la orden y que tenga un review asociado
            if (orderItem != null)
            {
                //consulta los reviews existentes para esa orden
                var review = GetAllProductReviews(orderItemId: orderItem.Id).FirstOrDefault();

                if (review == null)
                    orderItemId = orderItem.Id;

                return review == null;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Trae la mejor y la peor calificaci�n de un producto
        /// </summary>
        /// <param name="productId">Id del producto</param>
        /// <param name="bestRating">Variable de salida con la mejor calificacion</param>
        /// <param name="worstRating">Varibale de salida con la peor calificaci�n</param>
        public void GetBestWorstRating(int productId, out int bestRating, out int worstRating)
        {
            var maxMin = _productReviewRepository.Table.Where(r => r.ProductId == productId)
                .GroupBy(r => r.ProductId)
                .Select(r => new { Best = r.Max(g => g.Rating), Worst = r.Min(g => g.Rating) });

            var result = maxMin.FirstOrDefault();

            bestRating = result != null ? result.Best : 0;
            worstRating = result != null ? result.Worst : 0;

        }

        /// <summary>
        /// Get low stock products
        /// </summary>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="products">Low stock products</param>
        /// <param name="combinations">Low stock attribute combinations</param>
        public virtual void GetLowStockProducts(int vendorId,
            out IList<Product> products,
            out IList<ProductAttributeCombination> combinations)
        {
            //Track inventory for product
            var query1 = from p in _productRepository.Table
                         orderby p.MinStockQuantity
                         where !p.Deleted &&
                         p.ManageInventoryMethodId == (int)ManageInventoryMethod.ManageStock &&
                             //ignore grouped products
                         p.ProductTypeId != (int)ProductType.GroupedProduct &&
                         p.MinStockQuantity >= (
                            p.UseMultipleWarehouses ?
                            p.ProductWarehouseInventory.Sum(pwi => pwi.StockQuantity - pwi.ReservedQuantity) :
                            p.StockQuantity) &&
                         (vendorId == 0 || p.VendorId == vendorId)
                         select p;
            products = query1.ToList();

            //Track inventory for product by product attributes
            var query2 = from p in _productRepository.Table
                         from c in p.ProductAttributeCombinations
                         where !p.Deleted &&
                         p.ManageInventoryMethodId == (int)ManageInventoryMethod.ManageStockByAttributes &&
                         c.StockQuantity <= 0 &&
                         (vendorId == 0 || p.VendorId == vendorId)
                         select c;
            combinations = query2.ToList();
        }

        /// <summary>
        /// Gets a product by SKU
        /// </summary>
        /// <param name="sku">SKU</param>
        /// <returns>Product</returns>
        public virtual Product GetProductBySku(string sku)
        {
            if (String.IsNullOrEmpty(sku))
                return null;

            sku = sku.Trim();

            var query = from p in _productRepository.Table
                        orderby p.Id
                        where !p.Deleted &&
                        p.Sku == sku
                        select p;
            var product = query.FirstOrDefault();
            return product;
        }

        /// <summary>
        /// Update HasTierPrices property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateHasTierPricesProperty(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.HasTierPrices = product.TierPrices.Count > 0;
            UpdateProduct(product);
        }

        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateHasDiscountsApplied(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.HasDiscountsApplied = product.AppliedDiscounts.Count > 0;
            UpdateProduct(product);
        }

        #endregion

        #region Inventory management methods

        /// <summary>
        /// Adjust inventory
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantityToChange">Quantity to increase or descrease</param>
        /// <param name="attributesXml">Attributes in XML format</param>
        public virtual void AdjustInventory(Product product, int quantityToChange, string attributesXml = "")
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //var prevStockQuantity = product.GetTotalStockQuantity();

            if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
            {
                //update stock quantity
                if (product.UseMultipleWarehouses)
                {
                    //use multiple warehouses
                    if (quantityToChange < 0)
                        ReserveInventory(product, quantityToChange);
                    else
                        UnblockReservedInventory(product, quantityToChange);
                }
                else
                {
                    //do not use multiple warehouses
                    //simple inventory management
                    product.StockQuantity += quantityToChange;
                    UpdateProduct(product);
                }

                //check if minimum quantity is reached
                if (quantityToChange < 0 && product.MinStockQuantity >= product.GetTotalStockQuantity())
                {
                    switch (product.LowStockActivity)
                    {
                        case LowStockActivity.DisableBuyButton:
                            product.DisableBuyButton = true;
                            product.DisableWishlistButton = true;
                            UpdateProduct(product);
                            break;
                        case LowStockActivity.Unpublish:
                            product.Published = false;
                            UpdateProduct(product);
                            break;
                        default:
                            break;
                    }
                }

                //send email notification
                if (quantityToChange < 0 && product.GetTotalStockQuantity() < product.NotifyAdminForQuantityBelow)
                {
                    _workflowMessageService.SendQuantityBelowStoreOwnerNotification(product, _localizationSettings.DefaultAdminLanguageId);
                }
            }

            if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStockByAttributes)
            {
                var combination = _productAttributeParser.FindProductAttributeCombination(product, attributesXml);
                if (combination != null)
                {
                    combination.StockQuantity += quantityToChange;
                    _productAttributeService.UpdateProductAttributeCombination(combination);

                    //send email notification
                    if (quantityToChange < 0 && combination.StockQuantity < combination.NotifyAdminForQuantityBelow)
                    {
                        _workflowMessageService.SendQuantityBelowStoreOwnerNotification(combination, _localizationSettings.DefaultAdminLanguageId);
                    }
                }
            }


            //bundled products
            var attributeValues = _productAttributeParser.ParseProductAttributeValues(attributesXml);
            foreach (var attributeValue in attributeValues)
            {
                if (attributeValue.AttributeValueType == AttributeValueType.AssociatedToProduct)
                {
                    //associated product (bundle)
                    var associatedProduct = GetProductById(attributeValue.AssociatedProductId);
                    if (associatedProduct != null)
                    {
                        AdjustInventory(associatedProduct, quantityToChange * attributeValue.Quantity);
                    }
                }
            }

            //TODO send back in stock notifications?
            //also do not forget to uncomment some code above ("prevStockQuantity")
            //if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
            //    product.BackorderMode == BackorderMode.NoBackorders &&
            //    product.AllowBackInStockSubscriptions &&
            //    product.GetTotalStockQuantity() > 0 &&
            //    prevStockQuantity <= 0 &&
            //    product.Published &&
            //    !product.Deleted)
            //{
            //    //_backInStockSubscriptionService.SendNotificationsToSubscribers(product);
            //}
        }

        /// <summary>
        /// Reserve the given quantity in the warehouses.
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity, must be negative</param>
        public virtual void ReserveInventory(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (quantity >= 0)
                throw new ArgumentException("Value must be negative.", "quantity");

            var qty = -quantity;

            var productInventory = product.ProductWarehouseInventory
                .OrderByDescending(pwi => pwi.StockQuantity - pwi.ReservedQuantity)
                .ToList();

            if (productInventory.Count <= 0)
                return;

            Action pass = () =>
            {
                foreach (var item in productInventory)
                {
                    var selectQty = Math.Min(item.StockQuantity - item.ReservedQuantity, qty);
                    item.ReservedQuantity += selectQty;
                    qty -= selectQty;

                    if (qty <= 0)
                        break;
                }
            };

            // 1st pass: Applying reserved
            pass();

            if (qty > 0)
            {
                // 2rd pass: Booking negative stock!
                var pwi = productInventory[0];
                pwi.ReservedQuantity += qty;
            }

            this.UpdateProduct(product);
        }

        /// <summary>
        /// Unblocks the given quantity reserved items in the warehouses
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity, must be positive</param>
        public virtual void UnblockReservedInventory(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (quantity < 0)
                throw new ArgumentException("Value must be positive.", "quantity");

            var productInventory = product.ProductWarehouseInventory
                .OrderByDescending(pwi => pwi.ReservedQuantity)
                .ThenByDescending(pwi => pwi.StockQuantity)
                .ToList();

            if (productInventory.Count <= 0)
                return;

            var qty = quantity;

            foreach (var item in productInventory)
            {
                var selectQty = Math.Min(item.ReservedQuantity, qty);
                item.ReservedQuantity -= selectQty;
                qty -= selectQty;

                if (qty <= 0)
                    break;
            }

            if (qty > 0)
            {
                var pwi = productInventory[0];
                pwi.StockQuantity += qty;
            }

            UpdateProduct(product);
        }

        /// <summary>
        /// Book the reserved quantity
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="warehouseId">Warehouse identifier</param>
        /// <param name="quantity">Quantity, must be negative</param>
        public virtual void BookReservedInventory(Product product, int warehouseId, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (quantity >= 0)
                throw new ArgumentException("Value must be negative.", "quantity");

            //only products with "use multiple warehouses" are handled this way
            if (product.ManageInventoryMethod != ManageInventoryMethod.ManageStock)
                return;
            if (!product.UseMultipleWarehouses)
                return;

            var pwi = product.ProductWarehouseInventory.FirstOrDefault(pi => pi.WarehouseId == warehouseId);
            if (pwi == null)
                return;

            pwi.ReservedQuantity = Math.Max(pwi.ReservedQuantity + quantity, 0);
            pwi.StockQuantity += quantity;
            UpdateProduct(product);

            //TODO add support for bundled products (AttributesXml)
        }

        /// <summary>
        /// Reverse booked inventory (if acceptable)
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="shipmentItem">Shipment item</param>
        /// <returns>Quantity reversed</returns>
        public virtual int ReverseBookedInventory(Product product, ShipmentItem shipmentItem)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (shipmentItem == null)
                throw new ArgumentNullException("shipmentItem");

            //only products with "use multiple warehouses" are handled this way
            if (product.ManageInventoryMethod != ManageInventoryMethod.ManageStock)
                return 0;
            if (!product.UseMultipleWarehouses)
                return 0;

            var pwi = product.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == shipmentItem.WarehouseId);
            if (pwi == null)
                return 0;

            var shipment = shipmentItem.Shipment;

            //not shipped yet? hence "BookReservedInventory" method was not invoked
            if (!shipment.ShippedDateUtc.HasValue)
                return 0;

            var qty = shipmentItem.Quantity;

            pwi.StockQuantity += qty;
            pwi.ReservedQuantity += qty;
            UpdateProduct(product);

            //TODO add support for bundled products (AttributesXml)

            return qty;
        }

        #endregion

        #region Related products

        /// <summary>
        /// Deletes a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        public virtual void DeleteRelatedProduct(RelatedProduct relatedProduct)
        {
            if (relatedProduct == null)
                throw new ArgumentNullException("relatedProduct");

            _relatedProductRepository.Delete(relatedProduct);

            //event notification
            _eventPublisher.EntityDeleted(relatedProduct);
        }

        /// <summary>
        /// Gets a related product collection by product identifier
        /// </summary>
        /// <param name="productId1">The first product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Related product collection</returns>
        public virtual IList<RelatedProduct> GetRelatedProductsByProductId1(int productId1, bool showHidden = false)
        {
            var query = from rp in _relatedProductRepository.Table
                        join p in _productRepository.Table on rp.ProductId2 equals p.Id
                        where rp.ProductId1 == productId1 &&
                        !p.Deleted &&
                        (showHidden || p.Published)
                        orderby rp.DisplayOrder
                        select rp;
            var relatedProducts = query.ToList();

            return relatedProducts;
        }

        /// <summary>
        /// Gets a related product
        /// </summary>
        /// <param name="relatedProductId">Related product identifier</param>
        /// <returns>Related product</returns>
        public virtual RelatedProduct GetRelatedProductById(int relatedProductId)
        {
            if (relatedProductId == 0)
                return null;

            return _relatedProductRepository.GetById(relatedProductId);
        }

        /// <summary>
        /// Inserts a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        public virtual void InsertRelatedProduct(RelatedProduct relatedProduct)
        {
            if (relatedProduct == null)
                throw new ArgumentNullException("relatedProduct");

            _relatedProductRepository.Insert(relatedProduct);

            //event notification
            _eventPublisher.EntityInserted(relatedProduct);
        }

        /// <summary>
        /// Updates a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        public virtual void UpdateRelatedProduct(RelatedProduct relatedProduct)
        {
            if (relatedProduct == null)
                throw new ArgumentNullException("relatedProduct");

            _relatedProductRepository.Update(relatedProduct);

            //event notification
            _eventPublisher.EntityUpdated(relatedProduct);
        }

        #endregion

        #region Cross-sell products

        /// <summary>
        /// Deletes a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell identifier</param>
        public virtual void DeleteCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            if (crossSellProduct == null)
                throw new ArgumentNullException("crossSellProduct");

            _crossSellProductRepository.Delete(crossSellProduct);

            //event notification
            _eventPublisher.EntityDeleted(crossSellProduct);
        }

        /// <summary>
        /// Gets a cross-sell product collection by product identifier
        /// </summary>
        /// <param name="productId1">The first product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Cross-sell product collection</returns>
        public virtual IList<CrossSellProduct> GetCrossSellProductsByProductId1(int productId1, bool showHidden = false)
        {
            var query = from csp in _crossSellProductRepository.Table
                        join p in _productRepository.Table on csp.ProductId2 equals p.Id
                        where csp.ProductId1 == productId1 &&
                        !p.Deleted &&
                        (showHidden || p.Published)
                        orderby csp.Id
                        select csp;
            var crossSellProducts = query.ToList();
            return crossSellProducts;
        }

        /// <summary>
        /// Gets a cross-sell product
        /// </summary>
        /// <param name="crossSellProductId">Cross-sell product identifier</param>
        /// <returns>Cross-sell product</returns>
        public virtual CrossSellProduct GetCrossSellProductById(int crossSellProductId)
        {
            if (crossSellProductId == 0)
                return null;

            return _crossSellProductRepository.GetById(crossSellProductId);
        }

        /// <summary>
        /// Inserts a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell product</param>
        public virtual void InsertCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            if (crossSellProduct == null)
                throw new ArgumentNullException("crossSellProduct");

            _crossSellProductRepository.Insert(crossSellProduct);

            //event notification
            _eventPublisher.EntityInserted(crossSellProduct);
        }

        /// <summary>
        /// Updates a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell product</param>
        public virtual void UpdateCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            if (crossSellProduct == null)
                throw new ArgumentNullException("crossSellProduct");

            _crossSellProductRepository.Update(crossSellProduct);

            //event notification
            _eventPublisher.EntityUpdated(crossSellProduct);
        }

        /// <summary>
        /// Gets a cross-sells
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="numberOfProducts">Number of products to return</param>
        /// <returns>Cross-sells</returns>
        public virtual IList<Product> GetCrosssellProductsByShoppingCart(IList<ShoppingCartItem> cart, int numberOfProducts)
        {
            var result = new List<Product>();

            if (numberOfProducts == 0)
                return result;

            if (cart == null || cart.Count == 0)
                return result;

            var cartProductIds = new List<int>();
            foreach (var sci in cart)
            {
                int prodId = sci.ProductId;
                if (!cartProductIds.Contains(prodId))
                    cartProductIds.Add(prodId);
            }

            foreach (var sci in cart)
            {
                var crossSells = GetCrossSellProductsByProductId1(sci.ProductId);
                foreach (var crossSell in crossSells)
                {
                    //validate that this product is not added to result yet
                    //validate that this product is not in the cart
                    if (result.Find(p => p.Id == crossSell.ProductId2) == null &&
                        !cartProductIds.Contains(crossSell.ProductId2))
                    {
                        var productToAdd = GetProductById(crossSell.ProductId2);
                        //validate product
                        if (productToAdd == null || productToAdd.Deleted || !productToAdd.Published)
                            continue;

                        //add a product to result
                        result.Add(productToAdd);
                        if (result.Count >= numberOfProducts)
                            return result;
                    }
                }
            }
            return result;
        }
        #endregion

        #region Tier prices

        /// <summary>
        /// Deletes a tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        public virtual void DeleteTierPrice(TierPrice tierPrice)
        {
            if (tierPrice == null)
                throw new ArgumentNullException("tierPrice");

            _tierPriceRepository.Delete(tierPrice);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(tierPrice);
        }

        /// <summary>
        /// Gets a tier price
        /// </summary>
        /// <param name="tierPriceId">Tier price identifier</param>
        /// <returns>Tier price</returns>
        public virtual TierPrice GetTierPriceById(int tierPriceId)
        {
            if (tierPriceId == 0)
                return null;

            return _tierPriceRepository.GetById(tierPriceId);
        }

        /// <summary>
        /// Inserts a tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        public virtual void InsertTierPrice(TierPrice tierPrice)
        {
            if (tierPrice == null)
                throw new ArgumentNullException("tierPrice");

            _tierPriceRepository.Insert(tierPrice);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(tierPrice);
        }

        /// <summary>
        /// Updates the tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        public virtual void UpdateTierPrice(TierPrice tierPrice)
        {
            if (tierPrice == null)
                throw new ArgumentNullException("tierPrice");

            _tierPriceRepository.Update(tierPrice);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(tierPrice);
        }

        #endregion

        #region Product pictures

        /// <summary>
        /// Deletes a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void DeleteProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException("productPicture");

            _productPictureRepository.Delete(productPicture);

            //event notification
            _eventPublisher.EntityDeleted(productPicture);
        }

        /// <summary>
        /// Gets a product pictures by product identifier
        /// </summary>
        /// <param name="productId">The product identifier</param>
        /// <returns>Product pictures</returns>
        public virtual IList<ProductPicture> GetProductPicturesByProductId(int productId)
        {
            var query = from pp in _productPictureRepository.Table
                        where pp.ProductId == productId
                        orderby pp.DisplayOrder
                        select pp;
            var productPictures = query.ToList();
            return productPictures;
        }

        /// <summary>
        /// Gets a product picture
        /// </summary>
        /// <param name="productPictureId">Product picture identifier</param>
        /// <returns>Product picture</returns>
        public virtual ProductPicture GetProductPictureById(int productPictureId)
        {
            if (productPictureId == 0)
                return null;

            return _productPictureRepository.GetById(productPictureId);
        }

        /// <summary>
        /// Inserts a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void InsertProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException("productPicture");

            _productPictureRepository.Insert(productPicture);

            //event notification
            _eventPublisher.EntityInserted(productPicture);
        }

        /// <summary>
        /// Inserta una nueva imagen al prodcuto apartir del objeto de Picture NO creado
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="picture"></param>
        public virtual ProductPicture InsertProductPicture(int productId, byte[] pictureBinary, string mimeType, string seoFilename, bool isNew, bool validateBinary = true, int displayOrder = 0, bool active = false)
        {
           //Inserta la imagen
            var picture = _pictureService.InsertPicture(pictureBinary, mimeType, seoFilename, isNew, validateBinary);


            //Relaciona la imagen
            var productPicture = new ProductPicture();
            productPicture.ProductId = productId;
            productPicture.PictureId = picture.Id;
            productPicture.DisplayOrder = displayOrder;
            productPicture.Active = active;

            //Guarda
            InsertProductPicture(productPicture);

            return productPicture;
        }

        /// <summary>
        /// Updates a product 
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void UpdateProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException("productPicture");

            _productPictureRepository.Update(productPicture);

            //event notification
            _eventPublisher.EntityUpdated(productPicture);
        }

        #endregion

        #region Product reviews

        /// <summary>
        /// Gets all product reviews
        /// </summary>
        /// <param name="customerId">Customer identifier; 0 to load all records</param>
        /// <param name="approved">A value indicating whether to content is approved; null to load all records</param> 
        /// <param name="fromUtc">Item creation from; null to load all records</param>
        /// <param name="toUtc">Item item creation to; null to load all records</param>
        /// <param name="message">Search title or review text; null to load all records</param>
        /// <returns>Reviews</returns>
        public virtual IList<ProductReview> GetAllProductReviews(int? customerId = null, bool? approved = null,
            DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = null, int? orderItemId = null)
        {
            var query = _productReviewRepository.Table;
            if (approved.HasValue)
                query = query.Where(c => c.IsApproved == approved);
            if (customerId > 0)
                query = query.Where(c => c.CustomerId == customerId);
            if (fromUtc.HasValue)
                query = query.Where(c => fromUtc.Value <= c.CreatedOnUtc);
            if (toUtc.HasValue)
                query = query.Where(c => toUtc.Value >= c.CreatedOnUtc);
            if (!String.IsNullOrEmpty(message))
                query = query.Where(c => c.Title.Contains(message) || c.ReviewText.Contains(message));
            if (orderItemId.HasValue)
                query = query.Where(c => c.OrderItemId == orderItemId.Value);

            query = query.OrderBy(c => c.CreatedOnUtc);
            var content = query.ToList();
            return content;
        }

        /// <summary>
        /// Gets product review
        /// </summary>
        /// <param name="productReviewId">Product review identifier</param>
        /// <returns>Product review</returns>
        public virtual ProductReview GetProductReviewById(int productReviewId)
        {
            if (productReviewId == 0)
                return null;

            return _productReviewRepository.GetById(productReviewId);
        }

        /// <summary>
        /// Deletes a product review
        /// </summary>
        /// <param name="productReview">Product review</param>
        public virtual void DeleteProductReview(ProductReview productReview)
        {
            if (productReview == null)
                throw new ArgumentNullException("productReview");

            _productReviewRepository.Delete(productReview);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);
        }

        #endregion

        #region Product warehouse inventory

        /// <summary>
        /// Deletes a ProductWarehouseInventory
        /// </summary>
        /// <param name="pwi">ProductWarehouseInventory</param>
        public virtual void DeleteProductWarehouseInventory(ProductWarehouseInventory pwi)
        {
            if (pwi == null)
                throw new ArgumentNullException("pwi");

            _productWarehouseInventoryRepository.Delete(pwi);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);
        }

        #endregion

        #region Public Product External User
        /// <summary>
        /// Permite publicar un producto de un usuario externo.
        /// </summary>
        /// <param name="customerId">Id del usuario que crea el contenido</param>
        /// <param name="product">Datos del producto</param>
        public void PublishProduct(Product product)
        {

            //Busca cual es el plan gratis y cara el valor DisplayOrder por defecto
            var freePlan = GetPlanById(_planSettings.PlanProductsFree);
            
            var rootCategory = _categoryService.GetRootCategoryByCategoryId(product.ProductCategories.FirstOrDefault().CategoryId);
            if (rootCategory == null)
                throw new NopException(CodeNopException.CategoryDoesntExist);
            else if (rootCategory.Id == _tuilsSettings.productBaseTypes_service && _workContext.CurrentVendor.VendorType != Core.Domain.Vendors.VendorType.Market)
                throw new NopException(CodeNopException.UserTypeNotAllowedPublishProductType, _localizationService.GetResource("publishProduct.error.publishInvalidCategoryService"));

            //Si tiene imagenes temporales por cargar las crea
            if (product.TempFiles.Count > 0)
            {

                var pictures = _pictureService.InsertPicturesFromTempFiles(product.TempFiles.ToArray());
                for (int i = 0; i < pictures.Count; i++)
                {
                    product.ProductPictures.Add(new ProductPicture()
                    {
                        PictureId = pictures[i].Id,
                        DisplayOrder = i,
                        //Solo deja activas las fotos que cumplen con la especificaci�n del plan
                        Active = i < freePlan.NumPictures
                    });
                }
            }
            else
            {
                //Carga la imagen por defecto de los servicios
                product.ProductPictures.Add(new ProductPicture()
                {
                    PictureId = _catalogSettings.DefaultServicePicture,
                    DisplayOrder = 0,
                    Active = true
                });
            }



           #region DisplayOrder

            
            product.DisplayOrder = freePlan.DisplayOrder;

            #endregion

            product.ProductTypeId = (int)ProductType.SimpleProduct;
            product.VisibleIndividually = true;
            product.ProductTemplateId = 1; //TODO:Revisar si puede ir quemado
            product.ShowOnHomePage = false;
            product.AllowCustomerReviews = true;
            product.CreatedOnUtc = product.UpdatedOnUtc = DateTime.UtcNow;
            product.OrderMaximumQuantity = 1;
            product.OrderMaximumQuantity = 1;
            product.StockQuantity = _tuilsSettings.defaultStockQuantity;

            var vendor = _workContext.CurrentVendor;


            //Ya que al publicar todav�a no se cuenta con la compra del plan se selecciona el plan gratis
            var selectedPlan = GetPlanById(vendor.VendorType != VendorType.Market ? _planSettings.PlanProductsFree : _planSettings.PlanStoresFree);

            //Si el producto es de una tienda, realiza validaciones de las fechas de expiraci�n
            if (vendor.VendorType == VendorType.Market)
            {
                //Si el vendor tiene un plan activo, deja la fecha de expiraci�n del producto igual a la expiraci�n del plan
                if (vendor.CurrentOrderPlanId.HasValue && vendor.PlanExpiredOnUtc > DateTime.UtcNow)
                {
                    product.AvailableEndDateTimeUtc = vendor.PlanExpiredOnUtc;
                }
                else
                {
                    product.AvailableEndDateTimeUtc = DateTime.UtcNow.AddDaysToPlan(selectedPlan.DaysPlan);
                }
            }
            else
            {
                product.AvailableEndDateTimeUtc = DateTime.UtcNow.AddDaysToPlan(selectedPlan.DaysPlan);
            }

            //Guarda los datos del producto, intenta enviar el correo
            InsertProduct(product);

            try
            {
                //Despues de insertar env�a la notificaci�n
                if (product.Id > 0)
                    _workflowMessageService.SendProductPublishedNotificationMessage(product, _workContext.WorkingLanguage.Id);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
            }

        }
        #endregion

        #region Questions

        /// <summary>
        /// Retorna el listado de preguntas de acuerdo al filtro enviado
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="vendorId"></param>
        /// <param name="status"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<ProductQuestion> GetProductQuestions(int? productId = null,
            int? vendorId = null,
            QuestionStatus? status = null,
            int? customerId = null)
        {
            var query = _productQuestionRepository.Table;

            if (productId.HasValue)
                query = query.Where(p => p.ProductId == productId.Value);

            if (vendorId.HasValue)
                query = query.Where(p => p.Product.VendorId == vendorId.Value);

            if (status.HasValue)
                query = query.Where(p => p.StatusId == (int)status.Value);

            if (customerId.HasValue)
                query = query.Where(p => p.CustomerId == customerId.Value);

            return query.ToList();
        }

        /// <summary>
        /// Retorna un producto por el ID
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public ProductQuestion GetProductQuestionById(int questionId)
        {

            if (questionId > 0)
            {
                return _productQuestionRepository.GetById(questionId);
            }

            return new ProductQuestion();

        }

        /// <summary>
        /// Actualiza los datos de una pregunta
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public bool UpdateProductQuestion(ProductQuestion question)
        {
            return _productQuestionRepository.Update(question) > 0;
        }

        /// <summary>
        /// Actualiza una respuesta como respondida y actualiza el acumulado del producto
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public bool AnswerQuestion(ProductQuestion question)
        {

            if (string.IsNullOrEmpty(question.AnswerText))
                throw new ArgumentNullException("AnswerText");

            question.Status = QuestionStatus.Answered;
            question.AnsweredOnUtc = DateTime.UtcNow;
            question.CustomerAnswerId = _workContext.CurrentCustomer.Id;

            if (UpdateProductQuestion(question))
            {
                //Actualiza el numero de preguntas sin responder de un producto
                UpdateUnansweredQuestionsByProductId(question.ProductId);

                //Envia la notificacion de que fue respondida la pregunta
                _workflowMessageService.SendQuestionAnsweredNotificationMessage(question, _workContext.WorkingLanguage.Id);

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Actualiza el numero de preguntas sin responder de un producto en especifico y actualiza las del vendedor
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public void UpdateUnansweredQuestionsByProductId(int productId)
        {
            //consulta las preguntas sin respuesta
            var questions = GetProductQuestions(productId: productId, status: QuestionStatus.Created);
            var product = GetProductById(productId);
            product.UnansweredQuestions = questions.Count;
            //Actualiza el n�mero de pregundas pendientes
            UpdateProduct(product);

            //Despues actualiza el numero de preguntas sin responder de un vendedor
            var vendor = _vendorRepository.GetById(product.VendorId);
            vendor.UnansweredQuestions = CountUnansweredQuestionsByVendorId(product.VendorId);
            _vendorRepository.Update(vendor);
        }


        /// <summary>
        /// Inserta una pregunta relacionada a un product
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public void InsertQuestion(ProductQuestion question)
        {
            question.CreatedOnUtc = DateTime.UtcNow;
            question.Status = QuestionStatus.Created;
            _productQuestionRepository.Insert(question);
            //Envia la notificacion de que fue respondida la pregunta
            _workflowMessageService.SendNewQuestionNotificationMessage(question, _workContext.WorkingLanguage.Id);
            //Actualiza el numero de preguntas sin responder por producto
            UpdateUnansweredQuestionsByProductId(question.ProductId);
        }
        #endregion

        #region Orders
        /// <summary>
        /// Actualiza el n�mero de ventas de un prodcuto
        /// </summary>
        /// <param name="productId"></param>
        public void UpdateTotalSalesByProductId(int productId)
        {
            if (productId <= 0)
                return;

            var product = GetProductById(productId);

            if (product != null)
            {
                //Cuenta el numero de ordenes que hay del producto y actualiza el valor
                var countOrders = _orderService.GetAllOrderItems(productId: productId, orderStatus: OrderStatus.Complete).Count;
                product.TotalSales = countOrders;
                UpdateProduct(product);
            }
        }
        #endregion

        #region Vendor
        /// <summary>
        /// Cuenta cuantos productos activos tiene un vendor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public int CountActiveProductsByVendorId(int vendorId)
        {
            if (vendorId <= 0)
                return 0;

            var query = _productRepository.Table
                .Where(p => p.VendorId == vendorId &&
                    p.Published &&
                    p.AvailableEndDateTimeUtc > DateTime.UtcNow &&
                    !p.Sold)
                .GroupBy(p => p.VendorId)
                .Select(group => group.Count());

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Cuenta todas las preguntas que no han sido contestadas de los productos de un vendedor
        /// </summary>
        /// <returns></returns>
        public int CountUnansweredQuestionsByVendorId(int vendorId)
        {
            if (vendorId <= 0)
                return 0;
            try
            {
                //Suma todas las preguntas sin responder de los productos del vendedor
                return _productRepository.Table
                    .Where(p => p.VendorId == vendorId)
                    .Sum(p => p.UnansweredQuestions);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
                return 0;
            }
        }


        /// <summary>
        /// Cuenta la cantidad de lugares que le quedan disponibles a un vendedor dependiendo del plan seleccionado
        /// para destacar sus productos
        /// </summary>
        /// <param name="product">
        ///     Producto producto que se intenta agregar. Sirve para saber si el producto se debe contar o no en la lista.
        ///     internamente contiene el Id del vendor
        /// </param>
        /// <param name="validatePlan">True: Debe validar que el plan este activo. Si no debe validar el parametro order no puede venir nulo</param>
        /// <param name="order">Cuando no se valida el plan directamente contra la base de datos es el plan que seleccion� el usuario</param>
        /// <returns>
        /// Listado de datos que le quedan al usuario
        /// </returns>
        public LeftFeaturedByVendorModel CountLeftFeaturedPlacesByVendor(Product product, bool validatePlan, Order order = null)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //Si seleccion� que no se valide el plan directamente debe venir la orden con la solicitud del plan del usuario
            if(!validatePlan && order == null)
                throw new ArgumentNullException("order");

            var leftProducts = new LeftFeaturedByVendorModel();

            //El plan del vendedor debe estar activo
            if ((product.Vendor.CurrentOrderPlanId.HasValue && product.Vendor.PlanExpiredOnUtc > DateTime.UtcNow) || !validatePlan)
            {
                leftProducts = CountLeftFeaturedPlacesByVendor(!validatePlan && !product.Vendor.CurrentOrderPlanId.HasValue ? order : product.Vendor.CurrentOrderPlan, product.VendorId);
            }

            return leftProducts;
        }

        /// <summary>
        /// Realiza el conteo de los productos que le quedan al usuario por publicar
        /// </summary>
        /// <param name="order"></param>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public LeftFeaturedByVendorModel CountLeftFeaturedPlacesByVendor(Order order, int vendorId)
        {
            var leftProducts = new Dictionary<int, int[]>();
            
            //Busca el plan seleccionado del vendedor
            //Si no tiene plan pago todav�a lo busca en el enviado
            var selectedPlan = GetPlanById(order.OrderItems.FirstOrDefault().Product.Id);
            //var attributesPlan = order.OrderItems.FirstOrDefault().Product.ProductSpecificationAttributes;

            //Consulta los productos activos del vendor
            var products = SearchProducts(vendorId: vendorId, sold:false);

            var model = new LeftFeaturedByVendorModel();

            //Cuenta los destacados en las bandas rotativas y cuantos hay por el plan y saca las diferencias
            //int productsFeaturedOnSlider = products.Where(p => p.FeaturedForSliders && p.Id != product.Id).Count();
            int productsFeaturedOnSlider = products.Where(p => p.FeaturedForSliders).Count();
            int productsFeaturedOnSliderByPlan = selectedPlan.NumProductsOnSliders;
            model.SlidersLeft = productsFeaturedOnSliderByPlan - productsFeaturedOnSlider;
            model.SlidersByPlan = productsFeaturedOnSliderByPlan;
            //leftProducts.Add(_planSettings.SpecificationAttributeIdProductsFeaturedOnSliders, new int[] { productsFeaturedOnSliderByPlan - productsFeaturedOnSlider, productsFeaturedOnSliderByPlan });

            //Cuenta los destacados en el home y cuantos hay por el plan y saca las diferencias
            int productsOnHome = products.Where(p => p.ShowOnHomePage).Count();
            int productsOnHomeByPlan = selectedPlan.NumProductsOnHome;
            model.HomePageLeft = productsOnHomeByPlan - productsOnHome;
            model.HomePageByPlan = productsOnHomeByPlan;
            //leftProducts.Add(_planSettings.SpecificationAttributeIdProductsOnHomePage, new int[] { productsOnHomeByPlan - productsOnHome, productsOnHomeByPlan });

            //Cuenta los destacados en el home y cuantos hay por el plan y saca las diferencias
            int productsOnSocialNetworks = products.Where(p => p.SocialNetworkFeatured).Count();
            int productsOnSocialNetworksByPlan = selectedPlan.NumProductsOnSocialNetworks;
            model.SocialNetworkLeft = productsOnSocialNetworksByPlan - productsOnSocialNetworks;
            model.SocialNetworkByPlan = productsOnSocialNetworksByPlan;
            //leftProducts.Add(_planSettings.SpecificationAttributeIdProductsOnSocialNetworks, new int[] { productsOnSocialNetworksByPlan - productsOnSocialNetworks, productsOnSocialNetworksByPlan });

            //Cuenta los productos que hacen parte del plan
            int productsPublished = products.Count;
            int productsPublishedByPlan = selectedPlan.NumProducts;
            model.ProductsLeft = productsPublishedByPlan - productsPublished;
            model.ProductsByPlan = productsPublishedByPlan;
            //leftProducts.Add(_planSettings.SpecificationAttributeIdLimitProducts, new int[] { productsPublishedByPlan - productsPublished, productsPublishedByPlan });

            model.DisplayOrder = selectedPlan.DisplayOrder;

            return model;
        }


        #endregion

        

        #region SpecialCategories
        public IList<SpecialCategoryProduct> GetSpecialCategoriesByProductId(int productId)
        {
            if(productId <= 0)
                return new List<SpecialCategoryProduct>();

            return _specialCategoryProductRepository.Table
                .IncludeProperties(sc => sc.Category)
                .Where(sc => sc.ProductId == productId)
                .ToList();
        }

        public void InsertSpecialCategoryProduct(SpecialCategoryProduct specialCategory)
        {
            _specialCategoryProductRepository.Insert(specialCategory);
        }

        public SpecialCategoryProduct GetSpecialCategoryProductById(int specialCategoryProductId)
        {
            if (specialCategoryProductId == 0)
                throw new ArgumentNullException("specialCategoryProductId");

            return _specialCategoryProductRepository.GetById(specialCategoryProductId);
        }

        public void UpdateSpecialCategoryProduct(SpecialCategoryProduct specialCategory)
        {
            _specialCategoryProductRepository.Update(specialCategory);
        }

        public void DeleteSpecialCategoryProduct(SpecialCategoryProduct specialCategory)
        {
            _specialCategoryProductRepository.Delete(specialCategory);
        }


        #endregion

        #endregion


        /// <summary>
        /// Valida si un vendedor ha alcanzado el limite d eproductos para vender
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public bool HasReachedLimitOfProducts(Vendor vendor, out int limit)
        {
            if (_planSettings.PlanProductsFree == 0)
            {
                limit = 0;
                return false;
            }
                
            
            var products = SearchProducts(vendorId: vendor.Id, sold:false);
            
            if (vendor.VendorType == VendorType.Market)
            {
                //Consulta el plan seleccionado por la tienda o sino el gratis
                var selectedPlan = vendor.GetCurrentPlan(this, _planSettings);
                limit = selectedPlan.NumProducts;
                return products.TotalCount >= limit;
            }
            else
            {
                int totalPublishedWiouthPaying = products.Where(p => !p.OrderPlanId.HasValue).Count();
                limit = GetPlanById(_planSettings.PlanProductsFree).NumProducts;
                //Si hay limite realiza la validaci�n
                if (limit > 0)
                {
                    return totalPublishedWiouthPaying >= limit;
                }
            }
            
            return false;
        }

        #region Publishing almost finished and finished
        public IList<Product> GetProductsAlmostToFinishPublishing(int daysBefore, bool? withMessageSent)
        {
            if (daysBefore <= 0)
                throw new ArgumentNullException("daysBefore");

            //Calcula la fecha desde las que debe tomar los correos sin enviar
            var lastDate = DateTime.UtcNow.AddDays(daysBefore);

            var query = _productRepository.Table.Where(p => p.Published
                //Productos publicados que su fecha de vencimiento sea mayor a hoy 
                //y menor  a los siguientes 5 dias
                && p.AvailableEndDateTimeUtc > DateTime.UtcNow 
                && p.AvailableEndDateTimeUtc < lastDate
                && !p.Sold);

            if (withMessageSent.HasValue)
                query = query.Where(p => p.ExpirationMessageSent == withMessageSent.Value);

            return query.ToList();
        }
        
        /// <summary>
        /// Trae todos los productos que finalizaron publicacci�n y que el correo no ha sido enviado
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetProductsFinishedPublishing()
        {
            var query = _productRepository.Table.Where(p => p.Published &&
                p.AvailableEndDateTimeUtc < DateTime.UtcNow &&
                !p.PublishingFinishedMessageSent &&
                !p.Sold);
            return query.ToList();
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="order"></param>
        /// <param name="planId"></param>
        public void AddPlanToProduct(int productId, Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var product = GetProductById(productId);
            if (product == null)
                throw new ArgumentNullException("productId");


            //La orden y el producto deben ser del mismo usuario
            if (order.Customer.VendorId != product.VendorId)
                throw new NopException("El cliente de la orden no corresponde con el del vendedor del producto");

            //Agrega las caracteristicas del plan al producto
            var selectedPlan = order.OrderItems.FirstOrDefault().Product;
            foreach (var attributeValues in selectedPlan.ProductSpecificationAttributes)
            {
                //Recorre todos los atributos asignados al PLAN para despues asignar las propiedades al producto
                var attribute = attributeValues.SpecificationAttributeOption.SpecificationAttribute;

                var valueAttributeSelected = attributeValues.SpecificationAttributeOption.Name;

                //valida si el atributo es el n�mero de d�as que va estar la publicaci�n activa
                if (attribute.Id == _planSettings.SpecificationAttributePlanDays)
                {
                    //Actualiza la fecha de cierre del producto de acuerdo al valor en dias del plan
                    product.AvailableEndDateTimeUtc = DateTime.UtcNow.AddDaysToPlan(Convert.ToInt32(valueAttributeSelected));
                }
                //si el atributo es el n�mero de fotograf�as las habilita
                else if (attribute.Id == _planSettings.SpecificationAttributeIdPictures)
                {
                    int maxNum = Convert.ToInt32(valueAttributeSelected);
                    //Activa el numero de fotograf�as correspondientes al plan
                    product.ProductPictures.Take(maxNum)
                            .ToList()
                            .ForEach(p =>
                            {
                                if (!p.Active)
                                {
                                    p.Active = true;
                                    _productPictureRepository.Update(p);
                                }
                            });
                }
                //si el atributo es la exposici�n que tiene el producto lo actualiza
                else if (attribute.Id == _planSettings.SpecificationAttributeIdDisplayOrder)
                {
                    //Actualiza la propiedad del orden en el que se deben mostrar los productos
                    product.DisplayOrder = Convert.ToInt32(valueAttributeSelected);
                }
                //Si es banda rotativa actualiza los valores correspondientes
                else if (attribute.Id == _planSettings.SpecificationAttributeIdSliders)
                {
                    //So el valor asignado es el de categorias, destaca las categor�as
                    if (attributeValues.SpecificationAttributeOptionId == _planSettings.OptionAttributeFeaturedCategories)
                    {
                        //Busca las categorias del producto para destacarlas
                        foreach (var category in product.ProductCategories)
                        {
                            category.IsFeaturedProduct = true;
                            _eventPublisher.EntityUpdated(category);
                        }
                    }
                    //So el valor asignado es el de marcas, destaca las marcas
                    else if (attributeValues.SpecificationAttributeOptionId == _planSettings.OptionAttributeFeaturedManufacturers)
                    {
                        //Busca las marcas del producto para destacarlas
                        foreach (var manufacturer in product.ProductManufacturers)
                        {
                            manufacturer.IsFeaturedProduct = true;
                            _eventPublisher.EntityUpdated(manufacturer);
                        }
                    }
                    else if (attributeValues.SpecificationAttributeOptionId == _planSettings.OptionAttributeFeaturedLeft)
                    {
                        product.LeftFeatured = true;
                    }
                    //IMPORTANTE. Para los destacados como relacionados se va dejar el mismo OptionAttributeFeaturedCategories
                    //al filtrar los productos de la misma categor�a del producto que se est� mostrando
                    
                    product.FeaturedForSliders = true;
                }
                //Destaca los del home
                else if (attribute.Id == _planSettings.SpecificationAttributeIdHomePage)
                {
                    product.ShowOnHomePage = true;
                }
                //Destaca los de las redes sociales
                else if (attribute.Id == _planSettings.SpecificationAttributeIdSocialNetworks)
                {
                    product.SocialNetworkFeatured = true;
                }
            }

            product.OrderPlanId = order.Id;

            //Actualiza el producto con las acciones realizadas
            UpdateProduct(product);


        }

        /// <summary>
        /// Trae el modelo de un plan dependiendo del id enviado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlanModel GetPlanById(int id)
        {
            string cacheKey = string.Format(PRODUCTS_PLAN_BY_ID_KEY, id);
            return _cacheManager.Get(cacheKey, () => {
                var product = GetProductById(id);
                var model = new PlanModel();
                model.ProductId = product.Id;
                model.Name = product.Name;

                bool isPlanForStores = product.ProductCategories.FirstOrDefault().CategoryId == _planSettings.CategoryStorePlansId;


                foreach (var spec in product.ProductSpecificationAttributes)
                {
                    var option = spec.SpecificationAttributeOption;

                    if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdLimitProducts)
                        model.NumProducts = Convert.ToInt32(option.Name);
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdProductsOnHomePage)
                        model.NumProductsOnHome = Convert.ToInt32(option.Name);
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdProductsOnSocialNetworks)
                        model.NumProductsOnSocialNetworks = Convert.ToInt32(option.Name);
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdProductsFeaturedOnSliders)
                        model.NumProductsOnSliders = Convert.ToInt32(option.Name);
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdHomePage)
                        model.ShowOnHomePage = true;
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdSliders)
                        model.ShowOnSliders = true;
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdSocialNetworks)
                        model.ShowOnSocialNetworks = true;
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributePlanDays)
                        model.DaysPlan = Convert.ToInt32(option.Name);
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdDisplayOrder)
                        model.DisplayOrder = Convert.ToInt32(option.Name);
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdPictures)
                        model.NumPictures = Convert.ToInt32(option.Name);
                    else if (option.SpecificationAttributeId == _planSettings.SpecificationAttributeIdMostExpensivePlan)
                        model.IsMostExpensive = true;
                }

                return model;

            });
        }

        /// <summary>
        /// Inactiva los productos de un vendor de acuerdo a un plan seleccionado
        /// </summary>
        /// <param name="vendor"></param>
        public void ValidateProductLimitsByVendorPlan(Vendor vendor)
        {

            //Si el plan ya expir�, carga los datos del plan gratis
            //int planId = vendor.PlanExpiredOnUtc > DateTime.UtcNow ? vendor.CurrentOrderPlan.OrderItems.FirstOrDefault().ProductId : _planSettings.PlanStoresFree;
            var selectedPlan = vendor.GetCurrentPlan(Nop.Core.Infrastructure.EngineContext.Current.Resolve<IProductService>(), _planSettings);
            //Si el plan no se le ha vencido al vendor la fecha es la del vencimiento
            //Si el plan ya se vencio, la fecha es la del plan gratis
            var newExpirationDate = vendor.PlanExpiredOnUtc > DateTime.UtcNow ? vendor.PlanExpiredOnUtc : DateTime.UtcNow.AddDays(selectedPlan.DaysPlan);
            
            int iProduct = 0;
            //Conteo de los productos con las caracteristicas seleccionadas
            int iProductHome = 0;
            int iProductSliders = 0;
            int iProductSocialNetworks = 0;

            //Consulta todos los productos 
            var products = _productRepository.Table
                .Where(p => p.VendorId == vendor.Id && p.Published)
                .OrderByDescending(p => p.UpdatedOnUtc)
                .ToList();

            //Trae todos los productos existentes y le aumenta el tiempo de muestra 
            //solo al m�ximo de productos permitidos por el plan
            foreach (var product in products)
            {
                bool updateProduct = false;
                
                //Valida si el producto est� dentro de las caracteristicas mostradas 
                if (product.IsTotallyAvailable() && product.ShowOnHomePage)
                    iProductHome++;
                if (product.IsTotallyAvailable() && product.FeaturedForSliders)
                    iProductSliders++;
                if (product.IsTotallyAvailable() && product.SocialNetworkFeatured)
                    iProductSocialNetworks++;
                
                //Actualiza la fecha de cierre a la fecha del plan
                if (iProduct < selectedPlan.NumProducts && !product.Sold)
                {
                    product.AvailableEndDateTimeUtc = newExpirationDate;
                    product.DisplayOrder = selectedPlan.DisplayOrder;
                    updateProduct = true;
                    iProduct++;
                }
                else
                {
                    if (product.AvailableEndDateTimeUtc > DateTime.UtcNow)
                    {
                        var freePlan = GetPlanById(_planSettings.PlanStoresFree);
                        //Los dem�s productos los desactiva por fecha siempre y cuando no esten vencidos ya
                        product.AvailableEndDateTimeUtc = DateTime.UtcNow;
                        product.SocialNetworkFeatured = false;
                        product.ShowOnHomePage = false;
                        product.FeaturedForSliders = false;
                        updateProduct = true;
                        product.DisplayOrder = freePlan.DisplayOrder;
                    }
                }

                //Si se pasa del numero permitido en el plan lso empieza a desactivar
                if (iProductSocialNetworks > selectedPlan.NumProductsOnSocialNetworks)
                {
                    product.SocialNetworkFeatured = false;
                    updateProduct = true;
                }
                if (iProductHome > selectedPlan.NumProductsOnHome)
                {
                    product.ShowOnHomePage = false;
                    updateProduct = true;
                }
                if (iProductSliders > selectedPlan.NumProductsOnSliders)
                {
                    product.FeaturedForSliders = false;
                    product.LeftFeatured = false;
                    updateProduct = true;
                    foreach (var category in product.ProductCategories)
                    {
                        category.IsFeaturedProduct = false;
                    }
                    foreach (var manufacturer in product.ProductManufacturers)
                    {
                        manufacturer.IsFeaturedProduct = false;
                    }
                }

                //Si se realiz� alg�n cambio en el producto lo actualiza en Base de datos
                if (updateProduct)
                    UpdateProduct(product);
                
            }

        }

        /// <summary>
        /// Realiza las validaciones necesarias para habilitar nuevamente un producto que fue desactivado por el usuario
        /// </summary>
        /// <param name="forceEnable">Fuerza la habilitaci�n del producto</param>
        /// <param name="product"></param>
        public void EnableProduct(Product product, bool forceEnable = false, bool updateProduct = true)
        {
            var vendor = product.Vendor;

            //Bandera para controlar si se debe actualizar el producto deshabilitando las caracteristicas
            //de destacado
            bool activateDisablingFeatured = false;

            //realiza validaciones para usuarios tipo persona
            if (vendor.VendorType == VendorType.User)
            {
                activateDisablingFeatured = EnableProductUser(product, vendor, forceEnable);
            }
            else
            {
                activateDisablingFeatured = EnableProductStore(product, vendor, forceEnable);
            }

            if (activateDisablingFeatured)
            { 
                //Actualiza las caracteristcas del plan como activo
                //Pero elimina todas las caracteristicas que pueden ser para destacar el producto
                product.Sold = false;
                product.AvailableEndDateTimeUtc = DateTime.UtcNow.AddDays(GetPlanById(vendor.VendorType != VendorType.Market ? _planSettings.PlanProductsFree : _planSettings.PlanStoresFree).DaysPlan);
                product.OrderPlanId = null;

                product.LeftFeatured = false;
                product.FeaturedForSliders = false;
                product.DisplayOrder = 3; //TODO: Quitar quemado
                product.ShowOnHomePage = false;
                product.SocialNetworkFeatured = false;

                //Quita el destacado en categoarias y marcas
                foreach (var category in product.ProductCategories)
                {
                    category.IsFeaturedProduct = false;
                }
                foreach (var manufacturer in product.ProductManufacturers)
                {
                    manufacturer.IsFeaturedProduct = false;
                }
            }
            
            //Actualiza los datos
            if(updateProduct)
                UpdateProduct(product);
        }

        /// <summary>
        /// Realiza las validaciones a un producto que va ser republicado por un usuario simple.
        /// NO actualiza datos
        /// </summary>
        /// <param name="product">Datos del producto</param>
        /// <param name="vendor">Datos del vendedor</param>
        /// <returns>true: Debe desactivar destacados del producto False: Solo debe actualizar el producto</returns>
        private bool EnableProductUser(Product product, Vendor vendor, bool forceEnable = false)
        {
            //Si tiene plan y todavia est� activo reactiva el producto
            if (product.OrderPlanId.HasValue && product.AvailableEndDateTimeUtc > DateTime.UtcNow)
            {
                product.Sold = false;
                return false;
            }
            else
            {
                //Cuenta los productos publicados por el vendedor actualmente
                //Que no tienen plan
                var numProductsVendor = _productRepository.Table
                       .Where(p =>
                           p.VendorId == vendor.Id &&
                           p.Published && !p.Sold &&
                           p.AvailableEndDateTimeUtc > DateTime.UtcNow &&
                           !p.OrderPlanId.HasValue)
                       .Count();

                int productLimitPublished = GetPlanById(_planSettings.PlanProductsFree).NumProducts;
                //Si el numero de productos publicados por el usuario es mayor o igual que el cupo que se tiene gratis
                //el usuario no puede reactivarlo
                if (!forceEnable && numProductsVendor >= productLimitPublished)
                {
                    throw new NopException("Ha alcanzado el limite de publicaciones gratis");
                }
                
                return true;
            }
        }

        /// <summary>
        /// Realiza las validaciones a un producto que va ser republicado por un usuario Tienda.
        /// NO actualiza datos
        /// </summary>
        /// <param name="product">Datos del producto</param>
        /// <param name="vendor">Datos del vendedor</param>
        /// <param name="forceEnable">Fuerza la habilitaci�n del producto sin lanzar la excepci�n</param>
        /// <returns>true: Debe desactivar destacados del producto False: Solo debe actualizar el producto</returns>
        private bool EnableProductStore(Product product, Vendor vendor, bool forceEnable = false)
        {
            //Si el vendor tiene plan activo, activa la publicacion con fecha limite como la del plan
            if (vendor.CurrentOrderPlanId.HasValue && vendor.PlanExpiredOnUtc > DateTime.UtcNow)
            {
                var productsCounted = CountLeftFeaturedPlacesByVendor(product, false, vendor.CurrentOrderPlan);


                if (!forceEnable && productsCounted.ProductsLeft <= 0)
                    throw new NopException("No se pueden habilitar m�s productos para este plan");

                product.Sold = false;
                product.AvailableEndDateTimeUtc = vendor.PlanExpiredOnUtc;

                //Si el producto est� publicado en el home y no tiene mas espacios lo deshabilita
                if (product.ShowOnHomePage && productsCounted.HomePageLeft <= 0)
                    product.ShowOnHomePage = false;

                if (product.SocialNetworkFeatured && productsCounted.SocialNetworkLeft <= 0)
                    product.SocialNetworkFeatured = false;

                if (product.FeaturedForSliders && productsCounted.SlidersLeft <= 0)
                {
                    product.FeaturedForSliders = false;

                    //Quita el destacado en categoarias y marcas
                    foreach (var category in product.ProductCategories)
                    {
                        category.IsFeaturedProduct = false;
                    }
                    foreach (var manufacturer in product.ProductManufacturers)
                    {
                        manufacturer.IsFeaturedProduct = false;
                    }
                }

                product.DisplayOrder = productsCounted.DisplayOrder;

                return false;
            }
            else
            {
                var numProductsVendor = _productRepository.Table
                       .Where(p =>
                           p.VendorId == vendor.Id &&
                           p.Published && !p.Sold &&
                           p.AvailableEndDateTimeUtc > DateTime.UtcNow)
                       .Count();

                var basicPlan = GetPlanById(_planSettings.PlanStoresFree);

                //Si el numero de productos publicados gratis por la tienda es mayor o igual que el cupo que se tiene gratis
                //el usuario no puede reactivarlo
                if (numProductsVendor >= basicPlan.NumProducts)
                {
                    throw new NopException("Ha alcanzado el limite de publicaciones gratis");
                }

                return true;
            }
        }
    }
}
