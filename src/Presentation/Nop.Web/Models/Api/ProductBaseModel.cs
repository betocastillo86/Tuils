using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    /// <summary>
    /// Modelo de Producto usado para trabajarcon API
    /// </summary>
    public class ProductBaseModel : LinkableModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FullDescription { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int ManufacturerId { get; set; }

        public bool IsShipEnabled { get; set; }

        [RequiredIf("IsShipEnabled", true)]
        public decimal AdditionalShippingCharge { get; set; }

        [Required]
        public decimal Price { get; set; }

        public List<SpecialCategoryProduct> SpecialCategories { get; set; }

        public int Color { get; set; }

        public int Condition { get; set; }
        public string CarriagePlate { get; set; }

        public int Year { get; set; }

        public int Kms { get; set; }

        public List<int> Negotiation { get; set; }

        public List<int> Accesories { get; set; }

        public List<string> TempFiles { get; set; }

        public bool IsNew { get; set; }

        public int StateProvince { get; set; }

        public int ProductTypeId { get; set; }

        public string DetailShipping { get; set; }

        public bool IncludeSupplies { get; set; }

        public List<int> Supplies { get; set; }

        public int SuppliesValue { get; set; }

    }
}