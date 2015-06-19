using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public interface IWishableModel
    {
        int Id { get; set; }

        bool DisableWishlistButton { get; set; }
    }
}