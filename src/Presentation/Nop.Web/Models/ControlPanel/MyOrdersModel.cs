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
            CurrentPlan = new CurrentPlanModel();
        }


        public int VendorId { get; set; }
        public bool ShowCurrentPlan { get; set; }

        public CurrentPlanModel CurrentPlan { get; set; }

        public List<OrderItemModel> Orders { get; set; }

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public string ResorceMessageNoRows { get; set; }

        //public int TotalItems { get; set; }
        public class CurrentPlanModel
        {
            public OrderItemModel Order { get; set; }


            public int NumProductsByPlan { get; set; }
            public int NumProductsLeft { get; set; }
            
            public int NumProductsOnHomeByPlan { get; set; }
            public int NumProductsOnHomeLeft { get; set; }
            public bool SelectOnHome { get; set; }

            public int NumProductsOnSlidersByPlan { get; set; }
            public int NumProductsOnSlidersLeft { get; set; }
            public bool SelectOnSliders { get; set; }

            public int NumProductsOnSocialNetworksByPlan { get; set; }
            public int NumProductsOnSocialNetworksLeft { get; set; }
            public bool SelectOnSocialNetworks { get; set; }

            public bool ShowRenovateButton { get; set; }

            public int NumDaysToExpirePlan { get; set; }

            public bool ShowUpgradeButton { get; set; }
        }
    }
}