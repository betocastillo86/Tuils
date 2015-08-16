using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class MyOrdersModel
    {
        public MyOrdersModel()
        {
            Orders = new List<OrderItemModel>();
            PagingFilteringContext = new CatalogPagingFilteringModel();
        }
        
        public List<OrderItemModel> Orders { get; set; }

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public string ResorceMessageNoRows { get; set; }

        //public int TotalItems { get; set; }

    }
}