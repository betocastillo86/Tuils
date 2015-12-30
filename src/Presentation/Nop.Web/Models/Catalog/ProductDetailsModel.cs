using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;

namespace Nop.Web.Models.Catalog
{
    public partial class ProductDetailsModel : BaseNopEntityModel, IComparableModel, IWishableModel
    {
        public ProductDetailsModel()
        {
            DefaultPictureModel = new PictureModel();
            PictureModels = new List<PictureModel>();
            GiftCard = new GiftCardModel();
            ProductPrice = new ProductPriceModel();
            AddToCart = new AddToCartModel();
            ProductAttributes = new List<ProductAttributeModel>();
            AssociatedProducts = new List<ProductDetailsModel>();
            VendorModel = new VendorBriefInfoModel();
            Breadcrumb = new ProductBreadcrumbModel();
            ProductTags = new List<ProductTagModel>();
            ProductSpecifications= new List<ProductSpecificationModel>();
            ProductManufacturers = new List<ManufacturerModel>();
            ProductReviewOverview = new ProductReviewOverviewModel();
            TierPrices = new List<TierPriceModel>();
            SpecialCategories = new List<SpecialCategoryProductModel>();
        }

        //picture(s)
        public bool DefaultPictureZoomEnabled { get; set; }
        public PictureModel DefaultPictureModel { get; set; }
        public IList<PictureModel> PictureModels { get; set; }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string ProductTemplateViewPath { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        public bool ShowSku { get; set; }
        public string Sku { get; set; }

        public bool ShowManufacturerPartNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }

        public bool ShowGtin { get; set; }
        public string Gtin { get; set; }

        public bool ShowVendor { get; set; }
        public VendorBriefInfoModel VendorModel { get; set; }

        public bool ShowProductsOfVendor { get; set; }

        public bool HasSampleDownload { get; set; }

        #region Versus
        public CategorySimpleModel Category { get; set; }

        public int Year { get; set; }

        public bool ShowVersus { get; set; }

        public List<CategoryBaseModel> VersusCategories { get; set; }
        #endregion
        

        public GiftCardModel GiftCard { get; set; }

        public bool IsShipEnabled { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool FreeShippingNotificationEnabled { get; set; }
        public string DeliveryDate { get; set; }
        /// <summary>
        /// Solo muestra el boton de comprar si
        /// - No ha sido comprado por el mismo por el mismo usuario previamente
        /// - Esta disponible
        /// - No está vendido
        /// </summary>
        public bool ShowButtonMoreInfo { get { return !this.ProductAlreadyBought && this.IsAvailable && !this.Sold; } }

        public bool Sold { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsRental { get; set; }
        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }

        public string StockAvailability { get; set; }

        public bool DisplayBackInStockSubscription { get; set; }

        public bool EmailAFriendEnabled { get; set; }
        public bool CompareProductsEnabled { get; set; }
        
        /// <summary>
        /// Valida si el usuario autenticado ya compró este producto previamente
        /// </summary>
        public bool ProductAlreadyBought { get; set; }

        public string PageShareCode { get; set; }

        public ProductPriceModel ProductPrice { get; set; }

        public AddToCartModel AddToCart { get; set; }

        public ProductBreadcrumbModel Breadcrumb { get; set; }

        public int CategoryId { get; set; }

        public IList<ProductTagModel> ProductTags { get; set; }

        public IList<ProductAttributeModel> ProductAttributes { get; set; }

        public IList<ProductSpecificationModel> ProductSpecifications { get; set; }

        public IList<ManufacturerModel> ProductManufacturers { get; set; }

        public ProductReviewOverviewModel ProductReviewOverview { get; set; }

        public IList<TierPriceModel> TierPrices { get; set; }

        public IList<SpecialCategoryProductModel> SpecialCategories { get; set; }

        //a list of associated products. For example, "Grouped" products could have several child "simple" products
        public IList<ProductDetailsModel> AssociatedProducts { get; set; }
        
        /// <summary>
        /// Nombre de la ciudad donde se vende el producto
        /// </summary>
        public string StateProvinceName { get; set; }

        /// <summary>
        /// Detalles del envío
        /// </summary>
        public string DetailShipping { get; set; }

        public bool DisableWishlistButton { get; set; }

        public bool IncludeSupplies { get; set; }

        /// <summary>
        /// Valor de los insumos
        /// </summary>
        public int SuppliesValue { get; set; }

        public string SuppliesValueStr { get; set; }

		#region Nested Classes

        public partial class ProductBreadcrumbModel : BaseNopModel
        {
            public ProductBreadcrumbModel()
            {
                CategoryBreadcrumb = new List<CategorySimpleModel>();
            }

            public bool Enabled { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductSeName { get; set; }
            public IList<CategorySimpleModel> CategoryBreadcrumb { get; set; }
        }

        public partial class AddToCartModel : BaseNopModel
        {
            public AddToCartModel()
            {
                this.AllowedQuantities = new List<SelectListItem>();
            }
            public int ProductId { get; set; }

            [NopResourceDisplayName("Products.Qty")]
            public int EnteredQuantity { get; set; }

            [NopResourceDisplayName("Products.EnterProductPrice")]
            public bool CustomerEntersPrice { get; set; }
            [NopResourceDisplayName("Products.EnterProductPrice")]
            public decimal CustomerEnteredPrice { get; set; }
            public String CustomerEnteredPriceRange { get; set; }

            public bool DisableBuyButton { get; set; }
            public bool DisableWishlistButton { get; set; }
            public List<SelectListItem> AllowedQuantities { get; set; }

            //rental
            public bool IsRental { get; set; }

            //pre-order
            public bool AvailableForPreOrder { get; set; }
            public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

            //updating existing shopping cart item?
            public int UpdatedShoppingCartItemId { get; set; }
        }

        public partial class ProductPriceModel : BaseNopModel
        {
            /// <summary>
            /// The currency (in 3-letter ISO 4217 format) of the offer price 
            /// </summary>
            public string CurrencyCode { get; set; }

            public string OldPrice { get; set; }

            public string Price { get; set; }
            public string PriceWithDiscount { get; set; }

            public decimal PriceValue { get; set; }
            public decimal PriceWithDiscountValue { get; set; }

            public bool CustomerEntersPrice { get; set; }

            public bool CallForPrice { get; set; }

            public int ProductId { get; set; }

            public bool HidePrices { get; set; }

            public bool ShowSnippets { get; set; }

            //rental
            public bool IsRental { get; set; }
            public string RentalPrice { get; set; }

            /// <summary>
            /// A value indicating whether we should display tax/shipping info (used in Germany)
            /// </summary>
            public bool DisplayTaxShippingInfo { get; set; }
        }

        public partial class GiftCardModel : BaseNopModel
        {
            public bool IsGiftCard { get; set; }

            [NopResourceDisplayName("Products.GiftCard.RecipientName")]
            [AllowHtml]
            public string RecipientName { get; set; }
            [NopResourceDisplayName("Products.GiftCard.RecipientEmail")]
            [AllowHtml]
            public string RecipientEmail { get; set; }
            [NopResourceDisplayName("Products.GiftCard.SenderName")]
            [AllowHtml]
            public string SenderName { get; set; }
            [NopResourceDisplayName("Products.GiftCard.SenderEmail")]
            [AllowHtml]
            public string SenderEmail { get; set; }
            [NopResourceDisplayName("Products.GiftCard.Message")]
            [AllowHtml]
            public string Message { get; set; }

            public GiftCardType GiftCardType { get; set; }
        }

        public partial class TierPriceModel : BaseNopModel
        {
            public string Price { get; set; }

            public int Quantity { get; set; }
        }

        public partial class ProductAttributeModel : BaseNopEntityModel
        {
            public ProductAttributeModel()
            {
                AllowedFileExtensions = new List<string>();
                Values = new List<ProductAttributeValueModel>();
            }

            public int ProductId { get; set; }

            public int ProductAttributeId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string TextPrompt { get; set; }

            public bool IsRequired { get; set; }

            /// <summary>
            /// Default value for textboxes
            /// </summary>
            public string DefaultValue { get; set; }
            /// <summary>
            /// Selected day value for datepicker
            /// </summary>
            public int? SelectedDay { get; set; }
            /// <summary>
            /// Selected month value for datepicker
            /// </summary>
            public int? SelectedMonth { get; set; }
            /// <summary>
            /// Selected year value for datepicker
            /// </summary>
            public int? SelectedYear { get; set; }

            /// <summary>
            /// Allowed file extensions for customer uploaded files
            /// </summary>
            public IList<string> AllowedFileExtensions { get; set; }

            public AttributeControlType AttributeControlType { get; set; }

            public IList<ProductAttributeValueModel> Values { get; set; }

        }

        public partial class ProductAttributeValueModel : BaseNopEntityModel
        {
            public string Name { get; set; }

            public string ColorSquaresRgb { get; set; }

            public string PriceAdjustment { get; set; }

            public decimal PriceAdjustmentValue { get; set; }

            public bool IsPreSelected { get; set; }

            public int PictureId { get; set; }
            public string PictureUrl { get; set; }
            public string FullSizePictureUrl { get; set; }
        }

		#endregion



        
    }
}