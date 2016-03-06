using Nop.Core.Infrastructure;
using Nop.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Web.Framework
{
    /// <summary>
    /// Valida y redirecciona si un usuario intenta acceder desde el navegador de facebook el cual genera algunos problemas
    /// </summary>
    public class CheckFacebookBrowserAttribute : ActionFilterAttribute
    {
        public CheckFacebookBrowserAttribute()
        {
            
        }
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string browser = filterContext.RequestContext.HttpContext.Request.UserAgent;
            
            //Si la expresión regular corresponde actualiza la vista que va a rederizar
            var regex = new Regex("(?=.*Android)(?=.*FBAV)", RegexOptions.IgnoreCase);

            if (regex.IsMatch(browser))
            {
                var view = (ViewResultBase) (filterContext.Result);
                view.ViewName = "_ErrorFacebookBrowser";
                filterContext.Result = view;
                //Se inicializa acá debido a
                //http://www.nopcommerce.com/boards/t/22318/exception-thrown-when-actionfilter-calls-another-class-using-ilogger.aspx
                var _logger = EngineContext.Current.Resolve<ILogger>();
                _logger.Warning(string.Format("Navegador de facebook detectado con user agent------>{0}", browser));
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
