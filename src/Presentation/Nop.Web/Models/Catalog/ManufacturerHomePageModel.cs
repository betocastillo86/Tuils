using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class ManufacturerHomePageModel
    {
        public bool Enable { get; set; }

        public List<ManufacturerModel> Manufacturers { get; set; }
    }
}