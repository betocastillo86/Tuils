using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Preproducts
{
    /// <summary>
    /// Se duplica la clase de presentación ya que no se cuenta con ella en esta etapa
    /// </summary>
    public class ProductBaseModel 
    {

        public int Id { get; set; }

        public string Name { get; set; }

        
        public string FullDescription { get; set; }

        
        public int CategoryId { get; set; }

        public int ManufacturerId { get; set; }

        public bool IsShipEnabled { get; set; }

        //[RequiredIf("IsShipEnabled", true)]
        public decimal AdditionalShippingCharge { get; set; }

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

        public IList<int> _arrayCategories { get; set; }

    }
}