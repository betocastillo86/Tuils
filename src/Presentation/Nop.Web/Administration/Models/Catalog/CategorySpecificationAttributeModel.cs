using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Catalog
{
    public partial class CategorySpecificationAttributeModel : BaseNopEntityModel
    {
        public string AttributeTypeName { get; set; }

        public string AttributeName { get; set; }

        public string ValueRaw { get; set; }

        public bool AllowFiltering { get; set; }

        public bool ShowOnCategoryPage { get; set; }

        public int DisplayOrder { get; set; }

        public int Year { get; set; }
    }
}