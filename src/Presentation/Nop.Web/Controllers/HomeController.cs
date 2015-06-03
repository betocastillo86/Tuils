using System.Web.Mvc;
using Nop.Web.Framework.Security;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Vista que unicamente cierra la ventana, usada para autenticaciones externas con ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Close()
        {
            return View();
        }
    }
}
