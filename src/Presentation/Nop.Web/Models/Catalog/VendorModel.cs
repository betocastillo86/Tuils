using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;
using Nop.Core.Domain.Vendors;

namespace Nop.Web.Models.Catalog
{
    public partial class VendorModel : BaseNopEntityModel
    {
        public VendorModel()
        {
            Products = new List<ProductOverviewModel>();
            PagingFilteringContext = new CatalogPagingFilteringModel();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        public string Email { get; set; }

        public double AvgRating { get; set; }

        public int TotalActiveProducts { get; set; }

        public int TotalSoldProducts { get; set; }

        public bool AllowEdit { get; set; }

        public bool EnableShipping { get; set; }

        public int? BackgroundPosition { get; set; }

        public bool EnableCreditCardPayment { get; set; }

        public PictureModel Picture { get; set; }

        public PictureModel BackgroundPicture { get; set; }

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public IList<ProductOverviewModel> Products { get; set; }

        public List<SpecialCategoryVendorModel> SpecialCategories { get; set; }
    }
}