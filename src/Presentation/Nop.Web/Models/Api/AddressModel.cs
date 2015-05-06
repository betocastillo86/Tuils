using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class AddressModel : BaseApiModel
    {
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public int StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Schedule { get; set; }

        public int DisplayOrder { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Required]
        public int VendorId { get; set; }

        public bool IsMainly { get; set; }
    }
}