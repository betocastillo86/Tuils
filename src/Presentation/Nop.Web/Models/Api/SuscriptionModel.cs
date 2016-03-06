using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class SubscriptionModel
    {
        [Required]
        public string Name { get; set; }

        public string Company { get; set; }
        
        [Required]
        public string Type { get; set; }

        [Required]
        public string Phone { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}