using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Admin.Controllers
{
    public class CategoryOrganizationHomeMenuModel
    {
        public int MaxColumns { get; set; }

        public List<CategoryOrganizationHomeMenu> Organization { get; set; }

        public string JsonObject { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}