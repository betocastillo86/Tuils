using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Services.Events;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Core.Domain.Common;
using Newtonsoft.Json.Linq;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Category service
    /// </summary>
    public partial class CategoryService : ICategoryService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// {1} : include subcategories
        /// {2} : exclude NotAllowedToPublishCategories
        /// </remarks>
        private const string CATEGORIES_BY_ID_KEY = "Nop.category.id-{0}-{1}-{2}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : parent category ID
        /// {1} : show hidden records?
        /// {2} : current customer ID
        /// {3} : store ID
        /// {4} : exclude NotAllowedToPublishCategories
        /// </remarks>
        private const string CATEGORIES_BY_PARENT_CATEGORY_ID_KEY = "Nop.category.byparent-{0}-{1}-{2}-{3}-{4}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : category ID
        /// {2} : page index
        /// {3} : page size
        /// {4} : current customer ID
        /// {5} : store ID
        /// </remarks>
        private const string PRODUCTCATEGORIES_ALLBYCATEGORYID_KEY = "Nop.productcategory.allbycategoryid-{0}-{1}-{2}-{3}-{4}-{5}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : product ID
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        private const string PRODUCTCATEGORIES_ALLBYPRODUCTID_KEY = "Nop.productcategory.allbyproductid-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CATEGORIES_PATTERN_KEY = "Nop.category.";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTCATEGORIES_PATTERN_KEY = "Nop.productcategory.";


        #endregion

        #region Fields

        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IAclService _aclService;
        private readonly CatalogSettings _catalogSettings;
        private readonly TuilsSettings _tuilsSettings;

        #endregion
        
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="categoryRepository">Category repository</param>
        /// <param name="productCategoryRepository">ProductCategory repository</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="aclRepository">ACL record repository</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="workContext">Work context</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="storeMappingService">Store mapping service</param>
        /// <param name="aclService">ACL service</param>
        /// <param name="catalogSettings">Catalog settings</param>
        public CategoryService(ICacheManager cacheManager,
            IRepository<Category> categoryRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<Product> productRepository,
            IRepository<AclRecord> aclRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IWorkContext workContext,
            IStoreContext storeContext,
            IEventPublisher eventPublisher,
            IStoreMappingService storeMappingService,
            IAclService aclService,
            CatalogSettings catalogSettings,
            TuilsSettings tuilsSettings)
        {
            this._cacheManager = cacheManager;
            this._categoryRepository = categoryRepository;
            this._productCategoryRepository = productCategoryRepository;
            this._productRepository = productRepository;
            this._aclRepository = aclRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._eventPublisher = eventPublisher;
            this._storeMappingService = storeMappingService;
            this._aclService = aclService;
            this._catalogSettings = catalogSettings;
            this._tuilsSettings = tuilsSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.Deleted = true;
            UpdateCategory(category);

            //reset a "Parent category" property of all child subcategories
            var subcategories = GetAllCategoriesByParentCategoryId(category.Id, true);
            foreach (var subcategory in subcategories)
            {
                subcategory.ParentCategoryId = 0;
                UpdateCategory(subcategory);
            }

            
        }
        
        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="includeInTopMenu">True: filtra las que se muestran en el menu principal, False: Trae las que no se muestran en el menu principal, Null: No filtra por el campo</param>
        /// <param name="parentCategoryId">Filtra por la categoria padre</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<Category> GetAllCategories(string categoryName = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? includeInTopMenu = null, int? parentCategoryId= null)
        {
            var query = _categoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Name.Contains(categoryName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);

            if (includeInTopMenu.HasValue)
                query = query.Where(c => c.IncludeInTopMenu == includeInTopMenu.Value);

            if (parentCategoryId.HasValue)
                query = query.Where(c => c.ParentCategoryId == parentCategoryId.Value);
            
            if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
            {
                if (!_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                        .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                    query = from c in query
                            join acl in _aclRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                            from acl in c_acl.DefaultIfEmpty()
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select c;
                }
                if (!_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    var currentStoreId = _storeContext.CurrentStore.Id;
                    query = from c in query
                            join sm in _storeMappingRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                            from sm in c_sm.DefaultIfEmpty()
                            where !c.LimitedToStores || currentStoreId == sm.StoreId
                            select c;
                }

                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);
            }
            
            var unsortedCategories = query.ToList();

            //sort categories
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedList<Category>(sortedCategories, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        public virtual IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false, bool includeSubcategories = false, bool excludeNotAllowedToPublishCategories = false)
        {
            string key = string.Format(CATEGORIES_BY_PARENT_CATEGORY_ID_KEY, parentCategoryId, showHidden, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id, excludeNotAllowedToPublishCategories);
            return _cacheManager.Get(key, () =>
            {
                var query = _categoryRepository.Table;
                if (!showHidden)
                    query = query.Where(c => c.Published);
                query = query.Where(c => c.ParentCategoryId == parentCategoryId);
                query = query.Where(c => !c.Deleted);
                query = query.OrderBy(c => c.DisplayOrder);

                if (excludeNotAllowedToPublishCategories)
                    query = query.Where(c => !c.NotAllowedToPublishProduct);

                if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        //ACL (access control list)
                        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                        query = from c in query
                                join acl in _aclRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                                from acl in c_acl.DefaultIfEmpty()
                                where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                                select c;
                    }
                    if (!_catalogSettings.IgnoreStoreLimitations)
                    {
                        //Store mapping
                        var currentStoreId = _storeContext.CurrentStore.Id;
                        query = from c in query
                                join sm in _storeMappingRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                                from sm in c_sm.DefaultIfEmpty()
                                where !c.LimitedToStores || currentStoreId == sm.StoreId
                                select c;
                    }
                    //only distinct categories (group by ID)
                    query = from c in query
                            group c by c.Id
                            into cGroup
                            orderby cGroup.Key
                            select cGroup.FirstOrDefault();
                    query = query.OrderBy(c => c.DisplayOrder);
                }

                var categories = query.ToList();

                //Si se incluyen las subcategorias hace llamado así mismo para cargarlas en las ya existentes
                if (includeSubcategories)
                    categories
                        .ForEach(c => c.SubCategories = GetAllCategoriesByParentCategoryId(c.Id, showHidden, includeSubcategories));

                return categories;
            });

        }

        /// <summary>
        /// Actualiza la columna ChildrenCategoriesStr de una categoria especifica, o de todas si el parametro parentCategoryId viene nulo
        /// </summary>
        /// <param name="parentCategoryId">categoria padre por la que se desea actualizar. Si viene nulo actualiza todas las categorias</param>
        /// <returns></returns>
        public virtual void UpdateChildrenCategoriesByParentCategoryId(int? parentCategoryId = null)
        {
            //Si viene parentCategory actualiza solo los hijos de esa categoria padre
            if (parentCategoryId.HasValue)
            {
                var category = GetCategoryById(parentCategoryId.Value);
                GetAndUpdateChildrenCategoriesIds(category);
            }
            else
            {
                //Si no viene parent category actualiza todo
                var categories = GetAllCategoriesByParentCategoryId(0);
                foreach (var category in categories)
                {
                    GetAndUpdateChildrenCategoriesIds(category);
                }
            }
        }

        /// <summary>
        /// Busca todas las categorias hijas anidadas de una categoria y actualiza su valor en Base de datos
        /// </summary>
        /// <param name="parentCategory"></param>
        /// <returns></returns>
        private List<int> GetAndUpdateChildrenCategoriesIds(Category parentCategory)
        {
            var categoriesIds = new List<int>();
            var categories = GetAllCategoriesByParentCategoryId(parentCategory.Id);
            foreach (var category in categories)
            {
                categoriesIds.Add(category.Id);
                categoriesIds.AddRange(GetAndUpdateChildrenCategoriesIds(category));
            }

            //Actualiza el valor de la categoria en Base de datos
            parentCategory.ChildrenCategoriesStr = categoriesIds.Count > 0 ? string.Join(",", categoriesIds) : null;
            _categoryRepository.Update(parentCategory);

            return categoriesIds;
        }
        
        /// <summary>
        /// Gets all categories displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false)
        {
            var query = from c in _categoryRepository.Table
                        orderby c.DisplayOrder
                        where c.Published &&
                        !c.Deleted && 
                        c.ShowOnHomePage
                        select c;

            var categories = query.ToList();
            if (!showHidden)
            {
                categories = categories
                    .Where(c => _aclService.Authorize(c) && _storeMappingService.Authorize(c))
                    .ToList();
            }

            return categories;
        }

        /// <summary>
        /// Lista las categorías que deben ser mostradas en el home ccon las marcas
        /// </summary>
        /// <returns></returns>
        public virtual IList<Category> GetAllCategoriesDisplayedWithManufacturers()
        {
            var query = from c in _categoryRepository.Table
                        orderby c.DisplayOrder
                        where c.Published &&
                        !c.Deleted &&
                        c.ShowWithManufacturers
                        select c;

            return query.ToList();
        }
                
        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="includeSubCategories">Permite cargar las subcategorias o no </param>
        /// <param name="excludeNotAllowedToPublishCategories">Excluye las categorias para ser publicadas</param>
        /// <returns>Category</returns>
        public virtual Category GetCategoryById(int categoryId, bool includeSubCategories = false, bool excludeNotAllowedToPublishCategories = false)
        {
            if (categoryId == 0)
                return null;

            string key = string.Format(CATEGORIES_BY_ID_KEY, categoryId, includeSubCategories, excludeNotAllowedToPublishCategories);
            return _cacheManager.Get(key, () => {
                var category = _categoryRepository.GetById(categoryId);
                if (category != null && includeSubCategories)
                    category.SubCategories = GetAllCategoriesByParentCategoryId(categoryId, false, excludeNotAllowedToPublishCategories: excludeNotAllowedToPublishCategories);
                return category;
            });
        }


        /// <summary>
        /// Trae las categorias por las que se está consultando
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IList<Category> GetCategoriesByIds(int[] ids)
        {
            var categories = new List<Category>();
            foreach (var id in ids)
            {
                categories.Add(GetCategoryById(id));
            }
            return categories;
        }


        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Insert(category);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            
            

            //event notification
            _eventPublisher.EntityInserted(category);
        }

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            //validate category hierarchy
            var parentCategory = GetCategoryById(category.ParentCategoryId);
            while (parentCategory != null)
            {
                if (category.Id == parentCategory.Id)
                {
                    category.ParentCategoryId = 0;
                    break;
                }
                parentCategory = GetCategoryById(parentCategory.ParentCategoryId);
            }

            _categoryRepository.Update(category);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(category);
        }

        /// <summary>
        /// Elimina del cache el patrón para poder ser actualizado de nuevo
        /// </summary>
        public virtual void RemoveCachePattern()
        {
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
        }
        
        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void UpdateHasDiscountsApplied(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.HasDiscountsApplied = category.AppliedDiscounts.Count > 0;
            UpdateCategory(category);
        }

        /// <summary>
        /// Deletes a product category mapping
        /// </summary>
        /// <param name="productCategory">Product category</param>
        public virtual void DeleteProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            _productCategoryRepository.Delete(productCategory);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(productCategory);
        }

        /// <summary>
        /// Gets product category mapping collection
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product a category mapping collection</returns>
        public virtual IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (categoryId == 0)
                return new PagedList<ProductCategory>(new List<ProductCategory>(), pageIndex, pageSize);

            string key = string.Format(PRODUCTCATEGORIES_ALLBYCATEGORYID_KEY, showHidden, categoryId, pageIndex, pageSize, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = from pc in _productCategoryRepository.Table
                            join p in _productRepository.Table on pc.ProductId equals p.Id
                            where pc.CategoryId == categoryId &&
                                  !p.Deleted &&
                                  (showHidden || p.Published)
                            orderby pc.DisplayOrder
                            select pc;

                if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        //ACL (access control list)
                        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                        query = from pc in query
                                join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                                join acl in _aclRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                                from acl in c_acl.DefaultIfEmpty()
                                where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                                select pc;
                    }
                    if (!_catalogSettings.IgnoreStoreLimitations)
                    {
                        //Store mapping
                        var currentStoreId = _storeContext.CurrentStore.Id;
                        query = from pc in query
                                join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                                join sm in _storeMappingRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                                from sm in c_sm.DefaultIfEmpty()
                                where !c.LimitedToStores || currentStoreId == sm.StoreId
                                select pc;
                    }
                    //only distinct categories (group by ID)
                    query = from c in query
                            group c by c.Id
                            into cGroup
                            orderby cGroup.Key
                            select cGroup.FirstOrDefault();
                    query = query.OrderBy(pc => pc.DisplayOrder);
                }

                var productCategories = new PagedList<ProductCategory>(query, pageIndex, pageSize);
                return productCategories;
            });
        }

        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden"> A value indicating whether to show hidden records</param>
        /// <returns> Product category mapping collection</returns>
        public virtual IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false)
        {
            return GetProductCategoriesByProductId(productId, _storeContext.CurrentStore.Id, showHidden);
        }
        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="storeId">Store identifier (used in multi-store environment). "showHidden" parameter should also be "true"</param>
        /// <param name="showHidden"> A value indicating whether to show hidden records</param>
        /// <returns> Product category mapping collection</returns>
        public virtual IList<ProductCategory> GetProductCategoriesByProductId(int productId, int storeId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductCategory>();

            string key = string.Format(PRODUCTCATEGORIES_ALLBYPRODUCTID_KEY, showHidden, productId, _workContext.CurrentCustomer.Id, storeId);
            return _cacheManager.Get(key, () =>
            {
                var query = from pc in _productCategoryRepository.Table
                            join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                            where pc.ProductId == productId &&
                                  !c.Deleted &&
                                  (showHidden || c.Published)
                            orderby pc.DisplayOrder
                            select pc;

                var allProductCategories = query.ToList();
                var result = new List<ProductCategory>();
                if (!showHidden)
                {
                    foreach (var pc in allProductCategories)
                    {
                        //ACL (access control list) and store mapping
                        var category = pc.Category;
                        if (_aclService.Authorize(category) && _storeMappingService.Authorize(category, storeId))
                            result.Add(pc);
                    }
                }
                else
                {
                    //no filtering
                    result.AddRange(allProductCategories);
                }
                return result;
            });
        }

        /// <summary>
        /// Gets a product category mapping 
        /// </summary>
        /// <param name="productCategoryId">Product category mapping identifier</param>
        /// <returns>Product category mapping</returns>
        public virtual ProductCategory GetProductCategoryById(int productCategoryId)
        {
            if (productCategoryId == 0)
                return null;

            return _productCategoryRepository.GetById(productCategoryId);
        }

        /// <summary>
        /// Inserts a product category mapping
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        public virtual void InsertProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");
            
            _productCategoryRepository.Insert(productCategory);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(productCategory);
        }

        /// <summary>
        /// Updates the product category mapping 
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        public virtual void UpdateProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            _productCategoryRepository.Update(productCategory);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(productCategory);
        }


        #endregion

        #region Brands

        /// <summary>
        /// Retorna todas las referencias de motocicletas existentes o filradas por marca. NO están anidadas
        /// </summary>
        /// <param name="categoryBrandId">Marca por defecto apra realizar un filtro. Ej: Todas las referencias de Kawasaki</param>
        /// <returns>Referencias de motocicletas</returns>
        public IList<Category> GetAllBikeReferences(int? categoryBrandId)
        {
            //Si viene de una categoria utiliza el metodo anterior
            if (categoryBrandId.HasValue)
                return GetCategoryById(categoryBrandId.Value, true).SubCategories.ToList();
            else
            {
                return GetAllCategoriesByParentCategoryId(_tuilsSettings.productBaseTypes_bike, includeSubcategories: true, showHidden: true);
            }
        }

        #endregion
        /// <summary>
        /// Retorna todos los productos de tipo servicio existentes
        /// </summary>
        /// <returns></returns>
        public IList<Category> GetAllServices()
        {
            return GetAllCategoriesByParentCategoryId(_tuilsSettings.productBaseTypes_service, includeSubcategories: true, showHidden: true);
        }

        /// <summary>
        /// Retorna la lista de Ids de categorias hijas
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <returns></returns>
        public List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var categoriesIds = new List<int>();
            var category = GetCategoryById(parentCategoryId);
            if (category != null && !string.IsNullOrEmpty(category.ChildrenCategoriesStr))
                categoriesIds = category.ChildrenCategoriesStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => Convert.ToInt32(c))
                    .ToList();

            return categoriesIds;
        }


        /// <summary>
        /// Retorna la categoria raíz de una categoría especifica
        /// </summary>
        /// <param name="categoryId">id de la categoría</param>
        /// <returns></returns>
        public Category GetRootCategoryByCategoryId(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            while (category != null && category.ParentCategoryId != 0)
            {
                category = GetCategoryById(category.ParentCategoryId);
            }
            return category;
        }

        /// <summary>
        /// Consulta una cadena de configuración y mapea la estructura en un objeto
        /// </summary>
        /// <returns></returns>
        public List<CategoryOrganizationHomeMenu> GetCategoryOrganizationHomeMenu()
        {
            var categories = new List<CategoryOrganizationHomeMenu>();
            if (!string.IsNullOrEmpty(_catalogSettings.CategoryOrganizationHomeMenu))
            {
                var jsonArray = JArray.Parse(_catalogSettings.CategoryOrganizationHomeMenu);

                foreach (var jsonObject in jsonArray)
                {
                    var category = new CategoryOrganizationHomeMenu();
                    category.ColumnId = (int)jsonObject["column"];
                    category.Order = (int)jsonObject["order"];
                    category.CategoryId = (int)jsonObject["categoryId"];
                    category.ChildrenCategories = new List<CategoryOrganizationHomeMenu>();

                    foreach (var children in jsonObject["children"].ToArray())
                    {
                        category.ChildrenCategories.Add(new CategoryOrganizationHomeMenu() { 
                            Order = children["order"] != null ? (int)children["order"] : 0,
                            CategoryId = (int)children["categoryId"]
                        });
                    }
                    categories.Add(category);
                }

            }
            
            return categories;
        }
    }
}
