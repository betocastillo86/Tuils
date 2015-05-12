using Nop.Web.Framework.UI.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class MyOrdersPagingFilteringModel : BasePageableModel
    {
        public string Filter { get; set; }
    }
}