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

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public class LinkFilter
        {
            public string Url { get; set; }

            public bool Active { get; set; }
        }
    }
}