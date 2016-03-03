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
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string browser = filterContext.RequestContext.HttpContext.Request.UserAgent;

            var regex = new Regex("Android|FBAV", RegexOptions.IgnoreCase);
            if(regex.Matches(browser).Count >= 2)
                filterContext.Result = new RedirectResult("~/Content/Images/error_fb.png");

        }
    }
}
