using Nop.Web.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class PictureOrderedModel : PictureModel
    {
        public int DisplayOrder { get; set; }
    }
}