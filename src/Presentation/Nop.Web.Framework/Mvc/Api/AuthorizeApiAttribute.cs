using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Nop.Web.Framework.Mvc.Api
{
    /// <summary>
    /// Atributo que retorna las autorizaciones con codigo 403 en lugar de 401
    /// </summary>
    public class AuthorizeApiAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = new  HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            //base.HandleUnauthorizedRequest(actionContext);
        }
    }
}
