using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Web.Framework.Mvc.Api;
using Nop.Web.Framework.UI.Captcha;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Web.Extensions.Api;
using Nop.Services.Logging;

namespace Nop.Web.Controllers.Api
{
    [Route("api/questions")]
    public class QuestionsController : ApiController
    {

        #region Prop
        private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        private readonly CaptchaSettings _captchaSettings;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        #endregion
        #region Ctor

        public QuestionsController(IProductService productService, 
            IWorkContext workContext, 
            CaptchaSettings captchaSettings,
            ILocalizationService localizationService,
            ILogger logger)
        {
            this._productService = productService;
            this._workContext = workContext;
            this._captchaSettings = captchaSettings;
            this._localizationService = localizationService;
            this._logger = logger;
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

        [HttpPost]
        [AuthorizeApi]
        [CaptchaValidatorApi]
        [Route("api/questions")]
        public IHttpActionResult AddQuestion(ProductQuestionModel model)
        {
            var captchaValid = true;

            //Valida el captcha
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnProductQuestions)
            {
                captchaValid = (bool)Request.GetRouteData().Values["captchaValid"];
            }
            
            if (captchaValid)
            {
                var product = _productService.GetProductById(model.ProductId);
                if (product == null)
                    return BadRequest("No se encuentra el contenido");

                //Agrega la pregunta
                var question = model.ToEntity();
                question.CustomerId = _workContext.CurrentCustomer.Id;
               
                try
                {
                    _productService.InsertQuestion(question);
                    model.Id = question.Id;
                    return Ok(model);
                }
                catch (Exception e)
                {
                    _logger.Error(e.ToString(), e);
                    return InternalServerError(new Exception("No fue posible guardar"));
                }
            }
            else
            {
                ModelState.AddModelError("captcha", "Error de catpcha");
                ModelState.AddModelError("message", _localizationService.GetResource("Common.WrongCaptcha"));
                    //_localizationService.GetResource("Common.WrongCaptcha")
                return BadRequest(ModelState);
            }
        }
    }
}
