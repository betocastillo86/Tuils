using FluentValidation.Attributes;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Customer;
using Nop.Web.Validators.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.ControlPanel
{
    
    public partial class MyAccountModel : BaseNopModel
    {
        public MyAccountModel()
        {
            this.States = new List<StateProvince>();
        }

        [NopResourceDisplayName("Account.Fields.Email")]
        [AllowHtml]
        [Required]
        public string Email { get; set; }

        //form fields & properties
        [NopResourceDisplayName("Account.Fields.Gender")]
        public string Gender { get; set; }

        [NopResourceDisplayName("Account.Fields.FirstName")]
        [AllowHtml]
        [Required]
        public string FirstName { get; set; }

        [NopResourceDisplayName("Account.Fields.LastName")]
        [AllowHtml]
        [Required]
        public string LastName { get; set; }

        [Required]
        [NopResourceDisplayName("Account.Fields.StateProvince")]
        public int StateProvinceId { get; set; }

        [NopResourceDisplayName("Account.Fields.StateProvinceChild")]
        public int StateProvinceChildId { get; set; }
        
        [NopResourceDisplayName("Account.Fields.Phone")]
        [AllowHtml]
        public string Phone { get; set; }

        [Required]
        [NopResourceDisplayName("Account.Fields.Newsletter")]
        public bool Newsletter { get; set; }

        [Required]
        [NopResourceDisplayName("Account.Fields.DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        public IList<StateProvince> States { get; set; }


        [NopResourceDisplayName("Account.Fields.BikeBrand")]
        public int? BikeBrandId { get; set; }
        public IList<Category> BikeBrands { get; set; }


        [NopResourceDisplayName("Account.Fields.BikeReference")]
        public int? BikeReferenceId { get; set; }
        public IList<Category> BikeReferences { get; set; }

        [NopResourceDisplayName("Account.Fields.BikeYear")]
        public int? BikeYear { get; set; }


        [NopResourceDisplayName("Account.Fields.BikeCarriagePlate")]
        [MaxLength(7)]
        public string BikeCarriagePlate { get; set; }

        [NopResourceDisplayName("Account.Fields.BrandNewsletter")]
        public bool NewsletterBrand { get; set; }
        [NopResourceDisplayName("Account.Fields.ReferenceNewsletter")]
        public bool NewsletterReference { get; set; }



        
    }
}