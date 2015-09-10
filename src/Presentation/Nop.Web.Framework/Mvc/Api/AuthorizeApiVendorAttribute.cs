using Nop.Core;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Framework.Mvc.Api
{
    /// <summary>
    /// Atributo que retorna las autorizaciones con codigo 403 en lugar de 401
    /// Solo 
    /// </summary>
    
    public class AuthorizeApiVendorAttribute: AuthorizeApiAttribute
    {
        
        public AuthorizeApiVendorAttribute()
            : base()
        {
            
        }

        /// <summary>
        /// Sobreescribe la respuesta pa solicitudes que solo puede hacer vendedores
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext) && _workContext.CurrentVendor != null;
        }

    }
}
