using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Nop.Web.Framework.Security
{
    /// <summary>
    /// Atributo que valida basic authentication y devuelve los parametro username y password al controlador
    /// </summary>
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            string username = null;
            string password = null;

            if (actionContext.Request.Headers.Authorization != null && !string.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Parameter))
            {
                //Obtiene la llave en Base64 y contiene 
                string encodedString = System.Text.Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));
                if (encodedString != null && encodedString.IndexOf(':') != -1)
                {
                    username = encodedString.Split(new char[] { ':' })[0];
                    password = encodedString.Substring(encodedString.IndexOf(':') + 1);
                }
            }

            actionContext.ControllerContext.RouteData.Values["username"] = username;
            actionContext.ControllerContext.RouteData.Values["password"] = password;

            base.OnActionExecuting(actionContext);
        }
    }
}
