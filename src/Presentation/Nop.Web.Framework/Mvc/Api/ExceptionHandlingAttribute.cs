using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Nop.Web.Framework.Mvc.Api
{
    /// <summary>
    /// Atributo que permite registrar todas las excepciones del web api
    /// http://stackoverflow.com/questions/15167927/how-do-i-log-all-exceptions-globally-for-a-c-sharp-mvc4-webapi-app
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {

        public ExceptionHandlingAttribute()
        {
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NopException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "Exception"
                });

            }

            var logger = EngineContext.Current.Resolve<ILogger>();
            logger.Error(context.Exception.Message, context.Exception);

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred, please try again or contact the administrator."),
                ReasonPhrase = "Critical Exception"
            });
        }
    }
}
