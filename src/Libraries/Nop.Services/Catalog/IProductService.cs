using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Product service
    /// </summary>
    public partial interface IProductService
    {
        #region Products

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product</param>
        void DeleteProduct(Product product);

        /// <summary>
        /// Gets all products displayed on the home page
        /// </summary>
        /// <returns>Product collection</returns>
        IList<Product> GetAllProductsDisplayedOnHomePage();
        
        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        Product GetProductById(int productId);
        
        /// <summary>
        /// Gets products by identifier
        /// </summary>
        /// <param name="productIds">Product identifiers</param>
        /// <returns>Products</returns>
        IList<Product> GetProductsByIds(int[] productIds);

        /// <summary>
        /// Inserts a product
        /// </summary>
        /// <param name="product">Product</param>
        void InsertProduct(Product product);

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Get (visible) product number in certain category
        /// </summary>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <returns>Product number</returns>
        int GetCategoryProductNumber(IList<int> categoryIds = null, int storeId = 0);

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
        /// <param name="orderBySpecialCategoryId">Organiza los resultados por los que pertenecen a una categoria especial enviada (Usualmente la marca de moto del usuario en sesion)</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        IPagedList<Product> SearchProducts(
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
            bool? showOnSocialNetworks = null);




        IPagedList<Product> SearchProducts(
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
            bool? showOnSocialNetworks = null);


        /// <summary>
        /// Realiza un conteo de los productos activos de un vendedor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        int CountActiveProductsByVendorId(int vendorId);

        /// <summary>
        /// Cuenta todas las preguntas que no han sido contestadas de los productos de un vendedor
        /// </summary>
        /// <returns></returns>
        int CountUnansweredQuestionsByVendorId(int vendorId);

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="filterableSpecificationAttributeOptionIds">The specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="loadFilterableSpecificationAttributeOptionIds">A value indicating whether we should load the specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="filterableCategoryCount">Listado de categorias que se encontraron en el filtro con el numero de productos por cada una</param>
        /// <param name="filterableSpecificationAttributeOptionCount">Especificaciones que se econtraron según el filtro y el conteo respectivo de cada una</param>
        /// <param name="filterableStateProvinceCount">Ciudades que se encontraron en el filtro y el conteo respectivo de cada una de los prodcutos segpun el filtro</param>
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
        /// <param name="orderBySpecialCategoryId">Organiza los resultados por los que pertenecen a una categoria especial enviada (Usualmente la marca de moto del usuario en sesion)</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="published">Si viene null no filtra por el campo Published. Si no viene null si filtra por el campo dependiendo de su valor</param>
        /// <returns>Products</returns>
        IPagedList<Product> SearchProducts(
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
            bool? showOnSocialNetworks = null);


        /// <summary>
        /// Gets associated products
        /// </summary>
        /// <param name="parentGroupedProductId">Parent product identifier (used with grouped products)</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        IList<Product> GetAssociatedProducts(int parentGroupedProductId,
            int storeId = 0, bool showHidden = false);

        /// <summary>
        /// Update product review totals
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateProductReviewTotals(Product product);

        /// <summary>
        /// Valida si un usuario tiene un review pendiente en un producto especifico consultado las ordenes existentes
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <param name="orderItemId">Retorna el id de la orden pendiente de calificar, si no hay pendiente simplemente devuelve 0</param>
        /// <returns></returns>
        bool CustomerHasPendingReviewByProductId(int customerId, int productId, out int orderItemId);

        /// <summary>
        /// Get low stock products
        /// </summary>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="products">Low stock products</param>
        /// <param name="combinations">Low stock attribute combinations</param>
        void GetLowStockProducts(int vendorId,
            out IList<Product> products,
            out IList<ProductAttributeCombination> combinations);

        /// <summary>
        /// Gets a product by SKU
        /// </summary>
        /// <param name="sku">SKU</param>
        /// <returns>Product</returns>
        Product GetProductBySku(string sku);

        /// <summary>
        /// Update HasTierPrices property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateHasTierPricesProperty(Product product);

        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateHasDiscountsApplied(Product product);

        #endregion

        #region Inventory management methods

        /// <summary>
        /// Adjust inventory
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantityToChange">Quantity to increase or descrease</param>
        /// <param name="attributesXml">Attributes in XML format</param>
        void AdjustInventory(Product product, int quantityToChange, string attributesXml = "");

        /// <summary>
        /// Reserve the given quantity in the warehouses.
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity, must be negative</param>
        void ReserveInventory(Product product, int quantity);

        /// <summary>
        /// Unblocks the given quantity reserved items in the warehouses
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity, must be positive</param>
        void UnblockReservedInventory(Product product, int quantity);

        /// <summary>
        /// Book the reserved quantity
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="warehouseId">Warehouse identifier</param>
        /// <param name="quantity">Quantity, must be negative</param>
        void BookReservedInventory(Product product, int warehouseId, int quantity);

        /// <summary>
        /// Reverse booked inventory (if acceptable)
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="shipmentItem">Shipment item</param>
        /// <returns>Quantity reversed</returns>
        int ReverseBookedInventory(Product product, ShipmentItem shipmentItem);

        #endregion

        #region Related products

        /// <summary>
        /// Deletes a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        void DeleteRelatedProduct(RelatedProduct relatedProduct);

        /// <summary>
        /// Gets a related product collection by product identifier
        /// </summary>
        /// <param name="productId1">The first product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Related product collection</returns>
        IList<RelatedProduct> GetRelatedProductsByProductId1(int productId1, bool showHidden = false);

        /// <summary>
        /// Gets a related product
        /// </summary>
        /// <param name="relatedProductId">Related product identifier</param>
        /// <returns>Related product</returns>
        RelatedProduct GetRelatedProductById(int relatedProductId);

        /// <summary>
        /// Inserts a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        void InsertRelatedProduct(RelatedProduct relatedProduct);

        /// <summary>
        /// Updates a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        void UpdateRelatedProduct(RelatedProduct relatedProduct);

        #endregion

        #region Cross-sell products

        /// <summary>
        /// Deletes a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell</param>
        void DeleteCrossSellProduct(CrossSellProduct crossSellProduct);

        /// <summary>
        /// Gets a cross-sell product collection by product identifier
        /// </summary>
        /// <param name="productId1">The first product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Cross-sell product collection</returns>
        IList<CrossSellProduct> GetCrossSellProductsByProductId1(int productId1, bool showHidden = false);

        /// <summary>
        /// Gets a cross-sell product
        /// </summary>
        /// <param name="crossSellProductId">Cross-sell product identifier</param>
        /// <returns>Cross-sell product</returns>
        CrossSellProduct GetCrossSellProductById(int crossSellProductId);

        /// <summary>
        /// Inserts a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell product</param>
        void InsertCrossSellProduct(CrossSellProduct crossSellProduct);

        /// <summary>
        /// Updates a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell product</param>
        void UpdateCrossSellProduct(CrossSellProduct crossSellProduct);
        
        /// <summary>
        /// Gets a cross-sells
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="numberOfProducts">Number of products to return</param>
        /// <returns>Cross-sells</returns>
        IList<Product> GetCrosssellProductsByShoppingCart(IList<ShoppingCartItem> cart, int numberOfProducts);

        #endregion
        
        #region Tier prices

        /// <summary>
        /// Deletes a tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        void DeleteTierPrice(TierPrice tierPrice);

        /// <summary>
        /// Gets a tier price
        /// </summary>
        /// <param name="tierPriceId">Tier price identifier</param>
        /// <returns>Tier price</returns>
        TierPrice GetTierPriceById(int tierPriceId);

        /// <summary>
        /// Inserts a tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        void InsertTierPrice(TierPrice tierPrice);

        /// <summary>
        /// Updates the tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        void UpdateTierPrice(TierPrice tierPrice);

        #endregion

        #region Product pictures

        /// <summary>
        /// Deletes a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        void DeleteProductPicture(ProductPicture productPicture);

        /// <summary>
        /// Gets a product pictures by product identifier
        /// </summary>
        /// <param name="productId">The product identifier</param>
        /// <returns>Product pictures</returns>
        IList<ProductPicture> GetProductPicturesByProductId(int productId);

        /// <summary>
        /// Gets a product picture
        /// </summary>
        /// <param name="productPictureId">Product picture identifier</param>
        /// <returns>Product picture</returns>
        ProductPicture GetProductPictureById(int productPictureId);

        /// <summary>
        /// Inserts a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        void InsertProductPicture(ProductPicture productPicture);

        /// <summary>
        /// Inserta una nueva imagen al prodcuto apartir del objeto de Picture NO creado
        /// </summary>
        /// <param name="productId"></param>
        ///<param name="pictureBinary">Datos del archivo</param>
        ProductPicture InsertProductPicture(int productId, byte[] pictureBinary, string mimeType, string seoFilename, bool isNew, bool validateBinary = true, int displayOrder = 0);

        /// <summary>
        /// Updates a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        void UpdateProductPicture(ProductPicture productPicture);

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
        /// <param name="orderItemId">Filtra por orden a las calificaciones de un producto</param>
        /// <returns>Reviews</returns>
        IList<ProductReview> GetAllProductReviews(int? customerId= null, bool? approved = null,
            DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = null, int? orderItemId = null);

        /// <summary>
        /// Gets product review
        /// </summary>
        /// <param name="productReviewId">Product review identifier</param>
        /// <returns>Product review</returns>
        ProductReview GetProductReviewById(int productReviewId);

        /// <summary>
        /// Deletes a product review
        /// </summary>
        /// <param name="productReview">Product review</param>
        void DeleteProductReview(ProductReview productReview);

        #endregion

        #region Product warehouse inventory

        /// <summary>
        /// Deletes a ProductWarehouseInventory
        /// </summary>
        /// <param name="pwi">ProductWarehouseInventory</param>
        void DeleteProductWarehouseInventory(ProductWarehouseInventory pwi);

        #endregion

        #region Publish Product external user
        void PublishProduct(Product product);
        #endregion

        #region ProductQuestions
        /// <summary>
        /// Retorna el listado de preguntas de acuerdo al filtro
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="vendorId"></param>
        /// <param name="status"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<ProductQuestion> GetProductQuestions(int? productId = null,
            int? vendorId = null,
            QuestionStatus? status = null,
            int? customerId = null);

        /// <summary>
        /// Retorna una pregunta por el Id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        ProductQuestion GetProductQuestionById(int questionId);

        /// <summary>
        /// Actualiza los datos de una pregunta
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        bool UpdateProductQuestion(ProductQuestion question);


        bool AnswerQuestion(ProductQuestion question);

        void InsertQuestion(ProductQuestion question);
        
        /// <summary>
        /// Actualiza el numero de preguntas sin responder de un producto
        /// </summary>
        /// <param name="productId"></param>
        void UpdateUnansweredQuestionsByProductId(int productId);
        #endregion

        /// <summary>
        /// Actualiza el número de ventas que se ha hecho de un producto
        /// </summary>
        /// <param name="productId"></param>
        void UpdateTotalSalesByProductId(int productId);


        IList<SpecialCategoryProduct> GetSpecialCategoriesByProductId(int productId);

        /// <summary>
        /// Inserta una relacion de producto con cartegorias especiales
        /// </summary>
        /// <param name="specialCategory"></param>
        void InsertSpecialCategoryProduct(SpecialCategoryProduct specialCategory);

        SpecialCategoryProduct GetSpecialCategoryProductById(int specialCategoryProductId);

        void UpdateSpecialCategoryProduct(SpecialCategoryProduct specialCategory);

        void DeleteSpecialCategoryProduct(SpecialCategoryProduct specialCategory);

        bool HasReachedLimitOfProducts(Vendor vendor, out int limit);
        
        /// <summary>
        /// Cuenta la cantidad de lugares que le quedan disponibles a un vendedor dependiendo del plan seleccionado
        /// para destacar sus productos
        /// </summary>
        /// <param name="product">
        ///     Producto producto que se intenta agregar. Sirve para saber si el producto se debe contar o no en la lista.
        ///     internamente contiene el Id del vendor
        /// </param>
        /// <param name="validatePlan">True: Debe validar que el plan este activo. Si no debe validar el parametro order no puede venir nulo</param>
        /// <param name="order">Cuando no se valida el plan directamente contra la base de datos es el plan que seleccionó el usuario</param>
        /// <returns>
        /// Diccionario con la siguiente estructure:
        ///     Llave: ID del SpecificationAttribute relacionado del plan (Ej: SpecificationAttributeId de Numero de productos publicados en el home)
        ///     Valor: Array en posicion 0: Conteo de los productos que le quedan disponibles al vendor
        ///            Array en posicion 1: Conteo de los productos que puede seleccionar en el plan
        /// </returns>
        Dictionary<int, int[]> CountLeftFeaturedPlacesByVendor(Product product, bool validatePlan, Order order = null);

        /// <summary>
        /// Cuenta la cantidad de lugares que le quedan disponibles a un vendedor dependiendo del plan seleccionado
        /// para destacar sus productos
        /// </summary>
        /// <param name="order">
        ///     Orden que sobre la que se quiere consultar el plan
        /// </param>
        /// <param name="vendorId">Id del vendor que consulta los datos</param>
        /// <returns>
        /// Diccionario con la siguiente estructure:
        ///     Llave: ID del SpecificationAttribute relacionado del plan (Ej: SpecificationAttributeId de Numero de productos publicados en el home)
        ///     Valor: Array en posicion 0: Conteo de los productos que le quedan disponibles al vendor
        ///            Array en posicion 1: Conteo de los productos que puede seleccionar en el plan
        /// </returns>
        Dictionary<int, int[]> CountLeftFeaturedPlacesByVendor(Order order, int vendorId);

        /// <summary>
        /// Trae los productos que están a punto de finalizar dependiendo de un numero de dias previos
        /// </summary>
        /// <param name="daysBefore"></param>
        /// <param name="withMessageSent">Si el mensaje de notificacion fue enviado o no</param>
        /// <returns></returns>
        IList<Product> GetProductsAlmostToFinishPublishing(int daysBefore, bool? withMessageSent);
        
        /// <summary>
        /// Trae los productos que ya finalizaron publicación pero que no se les ha enviado el correo
        /// </summary>
        /// <returns></returns>
        IList<Product> GetProductsFinishedPublishing();




        /// <summary>
        /// Trae la mejor y la peor calificación de un producto
        /// </summary>
        /// <param name="productId">Id del producto</param>
        /// <param name="bestRating">Variable de salida con la mejor calificacion</param>
        /// <param name="worstRating">Varibale de salida con la peor calificación</param>
        void GetBestWorstRating(int productId, out int bestRating, out int worstRating);

        /// <summary>
        /// Agrega las caracteristicas de un plan a un producto después de que este fue pago
        /// </summary>
        /// <param name="product">datos del producto a marcar</param>
        /// <param name="order">datos de la orden relacionada</param>
        void AddPlanToProduct(int productId, Order order);

        /// <summary>
        /// Inactiva caracteristicas destacadas en los productos de un vendor de acuerdo a un plan seleccionado
        /// </summary>
        /// <param name="vendor"></param>
        void ValidateProductLimitsByVendorPlan(Vendor vendor);


        /// <summary>
        /// Realiza las validaciones para habilitar un producto y lo actualiza
        /// </summary>
        /// <param name="product">Producto a actualizar</param>
        void EnableProduct(Product product);


        /// <summary>
        /// Trae el modelo de un plan dependiendo del id enviado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PlanModel GetPlanById(int id);
    }
}
