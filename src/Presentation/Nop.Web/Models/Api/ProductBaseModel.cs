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

        //[RequiredIf("IsShipEnabled", true)]
        //public decimal AdditionalShippingCharge { get; set; }

        public decimal Price { get; set; }

        public string PriceFormatted { get; set; }

        public List<SpecialCategoryProduct> SpecialCategories { get; set; }

        public int? Color { get; set; }

        public int? Condition { get; set; }
        public string CarriagePlate { get; set; }

        public int Year { get; set; }

        public int Kms { get; set; }

        public List<int> Negotiation { get; set; }

        public List<int> Accesories { get; set; }

        public List<string> TempFiles { get; set; }

        public bool? IsNew { get; set; }

        public int StateProvince { get; set; }

        public int ProductTypeId { get; set; }

        public string DetailShipping { get; set; }

        public bool IncludeSupplies { get; set; }

        public List<int> Supplies { get; set; }

        public int SuppliesValue { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public bool CallForPrice { get; set; }

        /// <summary>
        /// Cuando viene en True no tiene en cuenta si el usuario a creado productos similares o no
        /// </summary>
        public bool OmitRepetedProduct { get; set; }
    }
}