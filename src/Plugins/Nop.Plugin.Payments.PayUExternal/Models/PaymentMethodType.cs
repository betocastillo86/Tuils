using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.PayUExternal.Models
{
    public enum PaymentMethodType
    {
        CreditCard = 2,
        [Description("pse-Transferencias bancarias")]
        PSE = 4,
        ACH =5,
        DebitCard = 6,
        [Description("Efectibo")]
        Cash = 7,
        [Description("Pago Referenciado")]
        Referenced = 8,
        [Description("Pago en bancos")]
        BankReferenced = 9
    }
}
