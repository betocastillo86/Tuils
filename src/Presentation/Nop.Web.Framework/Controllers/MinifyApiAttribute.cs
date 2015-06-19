using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Web.Framework.Controllers
{
    /// <summary>
    /// Atributo creado para validar si un llamado debe retornar objetos minificados o no
    /// </summary>
    public class MinifyApiAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var headers = filterContext.RequestContext.HttpContext.Request.Headers;
            bool isMinify = false;

            //Valida que tenga en los headers el parametro min y que esté en true
            if (headers != null && headers.GetValues("min") != null)
            {
                bool.TryParse(headers.GetValues("min").FirstOrDefault(), out isMinify);
            }

            //Agrega el parametro minifyModel a la accion
            filterContext.ActionParameters["minifyModel"] = isMinify;

            base.OnActionExecuting(filterContext);
        }
    }
}
