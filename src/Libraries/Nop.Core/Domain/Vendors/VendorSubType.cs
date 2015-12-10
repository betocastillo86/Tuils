using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Vendors
{
    public enum VendorSubType
    {
        /// <summary>
        /// Usuario normal
        /// </summary>
        User = 1,
        /// <summary>
        /// Sub tipo de almacen normal
        /// </summary>
        Store = 2,
        /// <summary>
        /// Subtipo de taller
        /// </summary>
        RepairShop = 3
    }
}
