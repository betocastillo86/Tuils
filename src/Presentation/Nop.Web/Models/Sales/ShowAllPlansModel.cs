using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class ShowAllPlansModel
    {
        public SelectPlanModel UserPlans { get; set; }

        public SelectPlanModel MarketPlans { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}