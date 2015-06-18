using System.Web.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;
using Nop.Web.Framework.Seo;
using System.Web.Mvc;

namespace Nop.Web.Infrastructure
{
    public partial class GenericUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapGenericPathRoute("ProductGenericUrl",
                                     "p/{generic_se_name}",
                                     new { controller = "Product", action = "ProductDetails" },
                                     new[] { "Nop.Web.Controllers" });

            routes.MapGenericPathRoute("VendorGenericUrl",
                         "v/{generic_se_name}",
                         new { controller = "Catalog", action = "Vendor" },
                         new[] { "Nop.Web.Controllers" });


            routes.MapGenericPathRoute("ManufacturerGenericUrl",
                         "m/{generic_se_name}",
                         new { controller = "Catalog", action = "Manufacturer" },
                         new[] { "Nop.Web.Controllers" });

            routes.MapGenericPathRoute("CategoryGenericUrl",
                         "c/{generic_se_name}/{specsFilter}",
                         new { controller = "Catalog", action = "Category", specificationFilter = UrlParameter.Optional },
                         new[] { "Nop.Web.Controllers" });
            
            //generic URLs
            routes.MapGenericPathRoute("GenericUrl",
                                       "{generic_se_name}",
                                       new {controller = "Common", action = "GenericUrl"},
                                       new[] {"Nop.Web.Controllers"});

            //define this routes to use in UI views (in case if you want to customize some of them later)
            routes.MapLocalizedRoute("Product",
                                     "p/{SeName}",
                                     new { controller = "Product", action = "ProductDetails" },
                                     new[] {"Nop.Web.Controllers"});

            routes.MapLocalizedRoute("Category",
                            "c/{SeName}/{specsFilter}",
                            new { controller = "Catalog", action = "Category", query = System.Web.Mvc.UrlParameter.Optional },
                            new[] { "Nop.Web.Controllers" });

            routes.MapLocalizedRoute("Manufacturer",
                            "m/{SeName}",
                            new { controller = "Catalog", action = "Manufacturer" },
                            new[] { "Nop.Web.Controllers" });

            routes.MapLocalizedRoute("Vendor",
                            "v/{SeName}",
                            new { controller = "Catalog", action = "Vendor" },
                            new[] { "Nop.Web.Controllers" });
            
            routes.MapLocalizedRoute("NewsItem",
                            "{SeName}",
                            new { controller = "News", action = "NewsItem" },
                            new[] { "Nop.Web.Controllers" });

            routes.MapLocalizedRoute("BlogPost",
                            "{SeName}",
                            new { controller = "Blog", action = "BlogPost" },
                            new[] { "Nop.Web.Controllers" });

            routes.MapLocalizedRoute("Topic",
                            "{SeName}",
                            new { controller = "Topic", action = "TopicDetails" },
                            new[] { "Nop.Web.Controllers" });



            //the last route. it's used when none of registered routes could be used for the current request
            //but it this case we cannot process non-registered routes (/controller/action)
            //routes.MapLocalizedRoute(
            //    "PageNotFound-Wildchar",
            //    "{*url}",
            //    new { controller = "Common", action = "PageNotFound" },
            //    new[] { "Nop.Web.Controllers" });
        }

        public int Priority
        {
            get
            {
                //it should be the last route
                //we do not set it to -int.MaxValue so it could be overridden (if required)
                return -1000000;
            }
        }
    }
}
