using Nop.Core;
using Nop.Services.Catalog;
using Nop.Web.Framework.Mvc.Api;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nop.Web.Controllers.Api
{
    [Route("api/questions")]
    public class QuestionsController : ApiController
    {

        #region Prop
        private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        #endregion
        #region Ctor

        public QuestionsController(IProductService productService, IWorkContext workContext)
        {
            this._productService = productService;
            this._workContext = workContext;
        }
        #endregion

        [HttpPut]
        [AuthorizeApi]
        [Route("api/questions")]
        public IHttpActionResult SaveAnswer(ProductQuestionModel model)
        {
            if (model.Id > 0 && _workContext.CurrentVendor != null)
            {
                if (!string.IsNullOrEmpty(model.AnswerText))
                {
                    //Valida que el que este contestando la pregunta tenga permisos apra hacerlo
                    var question = _productService.GetProductQuestionById(model.Id);
                    if (question.Product.VendorId == _workContext.CurrentVendor.Id)
                    {
                        question.AnswerText = model.AnswerText;
                        _productService.AnswerQuestion(question);
                        return Ok(model);
                    }
                    else
                        return BadRequest();
                }
                else
                    return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
