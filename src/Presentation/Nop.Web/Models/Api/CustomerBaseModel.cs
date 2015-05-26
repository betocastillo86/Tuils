using Nop.Core.Domain.Vendors;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    /// <summary>
    /// Modelo base de los usuarios. Datos basicos de autenticación
    /// </summary>
    public class CustomerBaseModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string CompanyName { get; set; }

        public VendorType VendorType { get; set; }
    }
}