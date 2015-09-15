using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    /// <summary>
    /// Modelo para editar los productos ya creados previamente
    /// </summary>
    public class EditProductModel : BaseNopEntityModel
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ShortDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}