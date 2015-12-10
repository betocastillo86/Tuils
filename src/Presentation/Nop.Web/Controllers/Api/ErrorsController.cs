using Nop.Services.Logging;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web.Http;

namespace Nop.Web.Controllers.Api
{

    [Route("api/errors")]
    public class ErrorsController : ApiController
    {
        private readonly ILogger _logger;
        
        public ErrorsController(ILogger logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        [Route("api/errors")]
        public IHttpActionResult RegisterError(ErrorJavascriptModel model)
        {
            string error = string.Format("Error Javascript: {0},\n File:{1},\n Line: {2},\n Column: {3},\n Browser:{4}, \n URL:{5}", model.Message, model.File, model.Line, model.Column, Request.Headers.UserAgent, model.Url);
            _logger.Error(error);
            return Ok();
        }
            
    }
}
