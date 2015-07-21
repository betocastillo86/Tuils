using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class ProductQuestionModel : BaseNopEntityModel

    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public string QuestionText { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string CreatedOnStr { get; set; }

        public DateTime? AnsweredOnUtc { get; set; }

        public string AnsweredOnStr { get; set; }

        public string AnswerText { get; set; }

        public ProductOverviewModel Product { get; set; }
    }
}