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
    public class ProductBaseModel : BaseNopEntityModel
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

        public List<string> TempFiles { get; set; }

    }
}