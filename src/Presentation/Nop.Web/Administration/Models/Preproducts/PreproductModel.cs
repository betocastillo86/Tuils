using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Preproducts
{
    public class PreproductModel : BaseNopEntityModel
    {
        public string ProductName { get; set; }

        public int CustomerId { get; set; }

        public int ProductTypeId { get; set; }

        public string CreatedOn { get; set; }

        public string UpdatedOn { get; set; }

        public string JsonObject { get; set; }

        public string CustomerEmail { get; set; }

        public string UserAgent { get; set; }
    }
}