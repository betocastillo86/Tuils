using Nop.Core.Domain.Catalog;
using Nop.Web.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Sales
{
    public class PublishProductModel : BaseNopModel
    {


        public List<CategoryBaseModel> BikeBrands { get; set; }

        ///// <summary>
        ///// Colores posibles para un producto de tipo moto
        ///// </summary>
        public SelectList ColorOptions { get; set; }

        /// <summary>
        /// Condicion de la moto
        /// </summary>
        public SelectList ConditionOptions { get; set; }

        /// <summary>
        /// Condiciones de negociación de la moto
        /// </summary>
        public IList<SpecificationAttributeOption> NegotiationOptions { get; set; }

        /// <summary>
        /// Accesorios que acompañan la moto
        /// </summary>
        public IList<SpecificationAttributeOption> AccesoriesOptions { get; set; }

        /// <summary>
        /// Tipo de producto de publicación
        /// Producto
        /// Moto
        /// Servicio
        /// </summary>
        public ProductTypePublished ProductType { get; set; }

    }

    
}