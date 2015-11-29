using Nop.Core.Domain.Vendors;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class MyProductsModel
    {

        public MyProductsModel()
        {
            PagingFilteringContext = new CatalogPagingFilteringModel();
        }

        public List<ProductOverviewModel> Products { get; set; }

        public LinkFilter UrlFilterByServices { get; set; }

        public LinkFilter UrlFilterByProducts { get; set; }

        public LinkFilter UrlFilterByBikes { get; set; }

        public string ResorceMessageNoRows { get; set; }

        /// <summary>
        /// True: Muestra el botón que permite destacar el producto por parte de un plan de un vendedor
        /// </summary>
        public bool ShowButtonFeatureByPlan { get; set; }

        public bool HasReachedLimitOfFeature { get; set; }

        public VendorType VendorType { get; set; }



        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public class LinkFilter
        {
            public string Url { get; set; }

            public bool Active { get; set; }
        }
    }
}