using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class VendorServicesModel
    {
        public List<int> BikeReferences { get; set; }

        public List<int> SpecializedCategories { get; set; }

        public string BikeReferencesString { get; set; }

        public string SpecializedCategoriesString { get; set; }

        public string ConfirmMessage { get; set; }

        public string VendorSeName { get; set; }
    }
}