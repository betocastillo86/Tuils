using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Sales
{
    public class SelectPlanModel : BaseNopModel
    {
        public SelectPlanModel()
        {
            Plans = new List<PlanModel>();
            CustomerInformation = new CustomerInformationModel();
        }
        /// <summary>
        /// Listado de planes
        /// </summary>
        public List<PlanModel>  Plans { get; set; }


        public int SelectedPlan { get; set; }

        /// <summary>
        /// Informacion del usuario
        /// </summary>
        public CustomerInformationModel CustomerInformation { get; set; }


        public class PlanModel : BaseNopEntityModel
        {
            public PlanModel()
            {
                Specifications = new List<SpecificationPlan>();
            }
            public IList<SpecificationPlan> Specifications { get; set; }
        }

        public class SpecificationPlan
        {
            public int SpecificationAttributeId { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }
        }
        
        public class CustomerInformationModel
        {

            public int StateProvinceId { get; set; }

            public string Address { get; set; }
        }
    }
}