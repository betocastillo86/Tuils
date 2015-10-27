using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class FeaturedAttributesByPlanRequest
    {
        public int? orderId { get; set; }
        
        public string sign { get; set; }

        public string from { get; set; }
    }
}