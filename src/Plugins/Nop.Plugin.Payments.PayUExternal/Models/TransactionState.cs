using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.PayUExternal.Models
{
    public enum TransactionState
    {
        Approved = 4,
        Declined = 6,
        Expired = 5,
        Pending = 7,
        Error = 104
    }
}
