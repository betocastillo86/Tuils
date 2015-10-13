using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Web.Framework
{
    /// <summary>
    /// Filtro que valida que el producto al que se quiera acceder sea del mismo usuario
    /// </summary>
    public class SameVendorProductAttribute : ActionFilterAttribute
    {

        #region Ctor
        private  IProductService _productService;
        private  IWorkContext _workContext;
        #endregion
        


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _productService = EngineContext.Current.Resolve<IProductService>();
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
            
            int productId = Convert.ToInt32(filterContext.RequestContext.RouteData.Values["id"]);


            //El producto debe existir
            if (productId <= 0 || _workContext.CurrentVendor == null)
            {
                filterContext.Result = new RedirectToRouteResult("PageNotFound", null);
                return;
            }
                
             var product = _productService.GetProductById(productId);
             //Valida que el producto pertenezca al usuario
             if (product != null && product.VendorId == _workContext.CurrentVendor.Id)
             {
                 filterContext.ActionParameters["product"] = product;
             }
             else
             {
                 filterContext.Result = new RedirectToRouteResult("PageNotFound", null);
             }

        }

        
    }
}
