using Nop.Web.Framework.UI.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class QuestionsPaginFilteringModel : BasePageableModel
    {
        /// <summary>
        /// Id del producto por el que se desea filtrar
        /// </summary>
        public int p { get; set; }
    }
}