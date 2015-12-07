using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class PlanModel : BaseNopEntityModel
    {
        public PlanModel()
        {
            Specifications = new List<SpecificationPlanModel>();
        }
        public IList<SpecificationPlanModel> Specifications { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public decimal PriceDecimal { get; set; }

        public class SpecificationPlanModel
        {
            public int SpecificationAttributeId { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }

            public string Description { get; set; }

            /// <summary>
            /// True: En la interfaz muestra el check con el numero
            /// </summary>
            public bool ShowWithCheck { get; set; }
        }

    }
}