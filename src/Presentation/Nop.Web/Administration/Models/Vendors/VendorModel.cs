﻿using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Validators.Vendors;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Core.Domain.Vendors;

namespace Nop.Admin.Models.Vendors
{
    [Validator(typeof(VendorValidator))]
    public partial class VendorModel : BaseNopEntityModel, ILocalizedModel<VendorLocalizedModel>
    {
        public VendorModel()
        {
            if (PageSize < 1)
            {
                PageSize = 5;
            }
            Locales = new List<VendorLocalizedModel>();
            AssociatedCustomerEmails = new List<string>();
        }

        [NopResourceDisplayName("Admin.Vendors.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.AdminComment")]
        [AllowHtml]
        public string AdminComment { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Active")]
        public bool Active { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }
        

        [NopResourceDisplayName("Admin.Vendors.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.PageSize")]
        public int PageSize { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.AllowCustomersToSelectPageSize")]
        public bool AllowCustomersToSelectPageSize { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.PageSizeOptions")]
        public string PageSizeOptions { get; set; }

        public IList<VendorLocalizedModel> Locales { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.AssociatedCustomerEmails")]
        public IList<string> AssociatedCustomerEmails { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.HasPlan")]
        public bool HasPlan { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.PlanExpiredOnUtc")]
        public System.DateTime? PlanExpiredOnUtc { get; set; }


        [NopResourceDisplayName("Admin.Vendors.Fields.PlanName")]
        public string PlanName { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.PhoneNumber")]
        /// <summary>
        /// Numero de contacto principal del vendedor
        /// </summary>
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.VendorType")]
        public VendorType VendorType { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.CustomerId")]
        public int? CustomerId { get; set; }


        /// <summary>
        /// Numero de veces que ha sido calificado un producto vendido por el usuario
        /// </summary>
        //public int NumRatings { get; set; }

        /// <summary>
        /// Numero de contacto principal del vendedor
        /// </summary>
        //public string PhoneNumber { get; set; }

        /// <summary>
        /// Numero de preguntas sin contestar de un vendedor
        /// </summary>
        //public int UnansweredQuestions { get; set; }

    }

    public partial class VendorLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}