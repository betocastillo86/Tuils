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

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }

        public string JsonObject { get; set; }

        public string CustomerEmail { get; set; }
    }
}