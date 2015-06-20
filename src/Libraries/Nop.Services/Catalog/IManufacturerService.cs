using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Manufacturer service
    /// </summary>
    public partial interface IManufacturerService
    {
        /// <summary>
        /// Deletes a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        void DeleteManufacturer(Manufacturer manufacturer);
        
        /// <summary>
        /// Gets all manufacturers
        /// </summary>
        /// <param name="manufacturerName">Manufacturer name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Manufacturers</returns>
        IPagedList<Manufacturer> GetAllManufacturers(string manufacturerName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false);

        /// <summary>
        /// Gets a manufacturer
        /// </summary>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>Manufacturer</returns>
        Manufacturer GetManufacturerById(int manufacturerId);


        /// <summary>
        /// retorna las marcas por los Ids enviados
        /// </summary>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>Manufacturer</returns>
        IList<Manufacturer> GetManufacturersByIds(int[] manufacturerIds);

        /// <summary>
        /// Inserts a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        void InsertManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Updates the manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        void UpdateManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Deletes a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        void DeleteProductManufacturer(ProductManufacturer productManufacturer);
        
        /// <summary>
        /// Gets product manufacturer collection
        /// </summary>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product manufacturer collection</returns>
        IPagedList<ProductManufacturer> GetProductManufacturersByManufacturerId(int manufacturerId,
            int pageIndex, int pageSize, bool showHidden = false);

        /// <summary>
        /// Gets a product manufacturer mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product manufacturer mapping collection</returns>
        IList<ProductManufacturer> GetProductManufacturersByProductId(int productId, bool showHidden = false);
        
        /// <summary>
        /// Gets a product manufacturer mapping 
        /// </summary>
        /// <param name="productManufacturerId">Product manufacturer mapping identifier</param>
        /// <returns>Product manufacturer mapping</returns>
        ProductManufacturer GetProductManufacturerById(int productManufacturerId);

        /// <summary>
        /// Inserts a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        void InsertProductManufacturer(ProductManufacturer productManufacturer);

        /// <summary>
        /// Updates the product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        void UpdateProductManufacturer(ProductManufacturer productManufacturer);

        /// <summary>
        /// Retonra todas las categorias asociadadas a una marca
        /// </summary>
        /// <param name="manufacturerId">id de la marca por la que se desea filtrar</param>
        /// <param name="showHidden"></param>
        /// <returns></returns>
        IList<ManufacturerCategory> GetCategoriesByManufacturerId(int manufacturerId, bool showHidden = false);

        /// <summary>
        /// Retorna todas las marcas con filtrados por la categoria que va por parametro
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="showHidden"></param>
        /// <returns>Listado de marcas</returns>
        IList<Manufacturer> GetManufacturersByCategoryId(int categoryId, bool showHidden = false);

        /// <summary>
        /// Inserta una relaci�n de una narca con una categoria
        /// </summary>
        /// <param name="manufacturerCategory">datos de la relaci�n</param>
        void InsertManufacturerCategory(ManufacturerCategory manufacturerCategory);

        /// <summary>
        /// Actualiza los datos de la relaci�n de la marca y la categoria
        /// </summary>
        /// <param name="manufacturerCategory">datos de la marca y categoria</param>
        void UpdateProductCategory(ManufacturerCategory manufacturerCategory);


        /// <summary>
        /// Elimina la relaci�n de una marca con una cateogoria
        /// </summary>
        /// <param name="manufacturerCategory">objeto a ser eliminado</param>
        void DeleteManufacturerCategory(ManufacturerCategory manufacturerCategory);

        /// <summary>
        /// Retorna la relaciona de una categoria con una marca
        /// </summary>
        /// <param name="manufacturerCategoryId">id de la relaci�n</param>
        /// <returns></returns>
        ManufacturerCategory GetManufacturerCategoryById(int manufacturerCategoryId);

        /// <summary>
        /// Retorna todas las marcas que deben salir en el home
        /// </summary>
        /// <returns></returns>
        IList<Manufacturer> GetManufacturersOnHomePage();


        
    }
}
