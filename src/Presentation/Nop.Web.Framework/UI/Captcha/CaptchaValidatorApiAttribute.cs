using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Nop.Core.Infrastructure;

namespace Nop.Web.Framework.UI.Captcha
{
    
    /// <summary>
    /// Filtro hecho para validar el captcha en solicitudes REST
    /// </summary>
    public class CaptchaValidatorApiAttribute : ActionFilterAttribute
    {
        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool valid = true;

            ///A diferencia de cuando el llamado es Web, para servicios rest toma el valor de los headers
            string captchaChallengeValue = actionContext.Request.Headers.GetValues(CHALLENGE_FIELD_KEY).FirstOrDefault();
            string captchaResponseValue = actionContext.Request.Headers.GetValues(RESPONSE_FIELD_KEY).FirstOrDefault();

            if (!string.IsNullOrEmpty(captchaChallengeValue) && !string.IsNullOrEmpty(captchaResponseValue))
            {
                var captchaSettings = EngineContext.Current.Resolve<CaptchaSettings>();
                if (captchaSettings.Enabled)
                {
                    //validate captcha
                    var captchaValidtor = new Recaptcha.RecaptchaValidator
                    {
                        PrivateKey = captchaSettings.ReCaptchaPrivateKey,
                        RemoteIP = GetClientIp(actionContext.Request),
                        Challenge = captchaChallengeValue,
                        Response = captchaResponseValue
                    };

                    var recaptchaResponse = captchaValidtor.Validate();
                    valid = recaptchaResponse.IsValid;
                }
            }

            ////this will push the result value into a parameter in our Action  
            actionContext.ControllerContext.RouteData.Values["captchaValid"] = valid;

            base.OnActionExecuting(actionContext);
        }

        private string GetClientIp(System.Net.Http.HttpRequestMessage request = null)
        {
 
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((System.Web.HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name))
            {
                System.ServiceModel.Channels.RemoteEndpointMessageProperty prop = (System.ServiceModel.Channels.RemoteEndpointMessageProperty)request.Properties[System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }

    }
}
