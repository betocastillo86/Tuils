using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using Nop.Core.Infrastructure;
using Nop.Core.Domain.Common;
using Nop.Core;

namespace Nop.Web.Framework.Mvc.Api
{

    /// <summary>
    /// Filtro para cargar imagenes 
    /// </summary>
    public class AsyncFileUploadApiFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.Request.Content.IsMimeMultipartContent())
            {
                var provider = await actionContext.Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                System.Web.Mvc.FormCollection formData = provider.FormData;
                IList<HttpContent> fileContentList = provider.Files;
                var fileDataList = provider.GetFiles();
                var files = await fileDataList;
                var fileToUpload = files.FirstOrDefault();
                actionContext.Request.Properties["fileUploaded"] = fileToUpload;

                if (fileToUpload != null && fileToUpload.Size > EngineContext.Current.Resolve<TuilsSettings>().maxFileUploadSize)
                {
                    throw new NopException("El tamaño de carga es invalido");
                }

            }
            
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

    }
}
