using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class SpecialCategoryProductModel
    {
        public string CategoryName { get; set; }

        public string CategorySeName { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public SpecialCategoryProductType SpecialType { get; set; }
    }
}