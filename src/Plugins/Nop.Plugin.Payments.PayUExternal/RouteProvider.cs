using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Payments.PayUExternal
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapRoute("Plugin.Payments.PayUExternal.Configure",
            //     "Plugins/PayUExternal/Configure",
            //     new { controller = "PayUExternal", action = "Configure" },
            //     new[] { "Nop.Plugin.Payments.PayUExternal.Controllers" }
            //);
            //routes.MapRoute("Plugin.Widgets.MegaMenu.PublicInfo",
            //     "Plugins/WidgetsMegaMenu/PublicInfo",
            //     new { controller = "WidgetsMegaMenu", action = "PublicInfo" },
            //     new[] { "Nop.Plugin.Widgets.MegaMenu.Controllers" }
            //);
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
