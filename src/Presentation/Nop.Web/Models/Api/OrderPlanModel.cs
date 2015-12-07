using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class OrderPlanModel
    {
        public int? ProductId { get; set; }

        [Required]
        public int PlanId { get; set; }

        public int AddressId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int StateProvinceId { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}