using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class MvcHelperExtensions
    {
        private static ILocalizationService _localizationService;

        private static ILocalizationService localizationService
        {
            get { return _localizationService ?? (_localizationService = EngineContext.Current.Resolve<ILocalizationService>()); }
        }

        /// <summary>
        /// Crea un label con el texto de una llave registrada en los recursos
        /// </summary>
        /// <param name="helper">this</param>
        /// <param name="key">llave de configuración de texto</param>
        /// <returns>objeto </returns>
        public static MvcHtmlString LabelT(this HtmlHelper helper, string key)
        {
            return helper.Label(localizationService.GetResource(key));
        }
    }
}