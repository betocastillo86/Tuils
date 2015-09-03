using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Category service interface
    /// </summary>
    public partial interface ICategoryService
    {
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        void DeleteCategory(Category category);

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
        IPagedList<Category> GetAllCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? includeInTopMenu = null,
            int? parentCategoryId= null);

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false, bool includeSubcategories = false, bool excludeNotAllowedToPublishCategories = false);

        /// <summary>
        /// Actualiza la columna ChildrenCategories de una categoria especifica, o de todas
        /// </summary>
        /// <param name="parentCategoryId">categoria padre por la que se desea actualizar. Si viene nulo actualiza todas las categorias</param>
        /// <returns></returns>
        void UpdateChildrenCategoriesByParentCategoryId(int? parentCategoryId = null);

        /// <summary>
        /// Gets all categories displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false);

        /// <summary>
        /// Lista las categorías que deben ser mostradas en el home ccon las marcas
        /// </summary>
        /// <returns></returns>
        IList<Category> GetAllCategoriesDisplayedWithManufacturers();
                
        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        Category GetCategoryById(int categoryId, bool includeSubcategories = false, bool excludeNotAllowedToPublishCategories = false);

        /// <summary>
        /// Retorna el listado de Ids de una categoria
        /// </summary>
        /// <param name="parentCategoryId">id de la categoria padre</param>
        /// <returns></returns>
        List<int> GetChildCategoryIds(int parentCategoryId);


        /// <summary>
        /// Retorna las categorias por los ids dados
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<Category> GetCategoriesByIds(int[] ids);

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        void InsertCategory(Category category);

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateHasDiscountsApplied(Category category);

        /// <summary>
        /// Deletes a product category mapping
        /// </summary>
        /// <param name="productCategory">Product category</param>
        void DeleteProductCategory(ProductCategory productCategory);

        /// <summary>
        /// Gets product category mapping collection
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product a category mapping collection</returns>
        IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId,
            int pageIndex, int pageSize, bool showHidden = false);

        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product category mapping collection</returns>
        IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false);
        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="storeId">Store identifier (used in multi-store environment). "showHidden" parameter should also be "true"</param>
        /// <param name="showHidden"> A value indicating whether to show hidden records</param>
        /// <returns> Product category mapping collection</returns>
        IList<ProductCategory> GetProductCategoriesByProductId(int productId, int storeId, bool showHidden = false);

        /// <summary>
        /// Gets a product category mapping 
        /// </summary>
        /// <param name="productCategoryId">Product category mapping identifier</param>
        /// <returns>Product category mapping</returns>
        ProductCategory GetProductCategoryById(int productCategoryId);

        /// <summary>
        /// Inserts a product category mapping
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        void InsertProductCategory(ProductCategory productCategory);

        /// <summary>
        /// Updates the product category mapping 
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        void UpdateProductCategory(ProductCategory productCategory);
        /// <summary>
        /// Retorna todas las referencias de motocicletas existentes en el sistema como categorias
        /// </summary>
        /// <param name="categoryBrandId"></param>
        /// <returns></returns>
        IList<Category> GetAllBikeReferences(int? categoryBrandId);
        /// <summary>
        /// Retorna todos los servicios existentes en el sistema
        /// </summary>
        /// <returns></returns>
        IList<Category> GetAllServices();

        /// <summary>
        /// Retorna la categoria de primer nivel padre de una categoria
        /// </summary>
        /// <param name="categoryId">Categoria que se desea buscar el padre</param>
        /// <returns></returns>
        Category GetRootCategoryByCategoryId(int categoryId);

        /// <summary>
        /// Retorna el orden en el que se deben mostrar las categorias en el home
        /// </summary>
        /// <returns></returns>
        List<CategoryOrganizationHomeMenu> GetCategoryOrganizationHomeMenu();
        /// <summary>
        /// Limpia el cache con el patron de categorias
        /// </summary>
        void RemoveCachePattern();

    }
}
