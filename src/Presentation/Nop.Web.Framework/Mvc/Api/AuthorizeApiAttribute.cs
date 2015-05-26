using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;

namespace Nop.Web.Framework.Mvc.Api
{
    /// <summary>
    /// Atributo que retorna las autorizaciones con codigo 403 en lugar de 401
    /// </summary>
    public class AuthorizeApiAttribute : System.Web.Http.AuthorizeAttribute
    {
        private readonly IWorkContext _workContext;

        public AuthorizeApiAttribute() : base()
        {
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        //public AuthorizeApiAttribute(IWorkContext workContext)
        //{
        //    this._workContext = workContext;
        //}

        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //if (base.IsAuthorized(actionContext))
            //    return !_workContext.CurrentCustomer.IsGuest();
            //else
            //    return false;
            return base.IsAuthorized(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //Se sobreescribe el tipo de respuesta para que sea Forbiden
            //Principalmente porque el navegador pide usuario y clave cuando se devuelve un Unaithorized. La idea es evitar esto.
            base.HandleUnauthorizedRequest(actionContext);
            actionContext.Response = new  HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }
    }
}
