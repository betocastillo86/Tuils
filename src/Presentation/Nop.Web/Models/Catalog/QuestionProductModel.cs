using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{

    public class QuestionsModel
    {
        public int ProductId { get; set; }
        public ProductQuestionModel NewQuestion { get; set; }
        public List<ProductQuestionModel> Questions { get; set; }
        public ProductOverviewModel Product { get; set; }

        public bool ShowCaptcha { get; set; }

    }

    
}