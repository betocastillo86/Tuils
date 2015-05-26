using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class ProductQuestionModel : BaseNopEntityModel

    {
        public string AnswerText { get; set; }
    }
}