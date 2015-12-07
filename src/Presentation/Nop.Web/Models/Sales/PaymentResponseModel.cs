using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class PaymentResponseModel
    {
        public string PluginActionResponse { get; set; }

        public string PluginControllerResponse { get; set; }

        public System.Web.Routing.RouteValueDictionary PluginRouteValuesResponse { get; set; }
    }
}