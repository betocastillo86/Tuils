using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Sales
{
    public class SelectPlanModel : BaseNopModel
    {
        public SelectPlanModel()
        {
            Plans = new List<PlanModel>();
            CustomerAddressInformation = new CustomerAddress();
            CustomerInformation = new CustomerInfo();
            DisabledPlans = new List<int>();
            
        }
        /// <summary>
        /// Listado de planes
        /// </summary>
        public List<PlanModel>  Plans { get; set; }

        /// <summary>
        /// Planes que son deshabilitados en los casos de UPGRADE
        /// </summary>
        public List<int> DisabledPlans { get; set; }


        public SelectList StateProvinces { get; set; }

        /// <summary>
        /// Url a la que se envia el formulario
        /// </summary>
        public string RedirectUrl { get; set; }

        public int SelectedPlan { get; set; }

        public int ProductId { get; set; }

        /// <summary>
        /// Deshabilita el plan gratis
        /// </summary>
        public bool DisableFreePlan { get; set; }

        /// <summary>
        /// True: llamado de pruebas a PayU
        /// </summary>
        public bool IsTest { get; set; }

        /// <summary>
        /// Informacion del usuario
        /// </summary>
        public CustomerAddress CustomerAddressInformation { get; set; }

        public CustomerInfo CustomerInformation { get; set; }


        public class PlanModel : BaseNopEntityModel
        {
            public PlanModel()
            {
                Specifications = new List<SpecificationPlan>();
            }
            public IList<SpecificationPlan> Specifications { get; set; }

            public string Name { get; set; }

            public string Price { get; set; }

            public decimal PriceDecimal { get; set; }

        }

        public class SpecificationPlan
        {
            public int SpecificationAttributeId { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }
        }
        
        public class CustomerAddress
        {
            public int AddressId { get; set; }

            public int StateProvinceId { get; set; }

            public string Address { get; set; }

            public string PhoneNumber { get; set; }

            public string City { get; set; }
        }

        public class CustomerInfo
        {
            public string FullName { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }
        }

        
    }
}