using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    /// <summary>
    /// Interfaz que valida si se puede o comparar un producto
    /// </summary>
    public interface IComparableModel
    {
        int Id { get; set; }

        bool CompareProductsEnabled { get; set; }
    }
}