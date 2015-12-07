﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.PayUExternal.Models
{
    public enum PaymentConfirmationErrorCode
    {
        None,
        InvalidSignature,
        NoPlanSelected,
        NoProductSelected,
        InvalidOrderNumber,
        OrderAlreadyPaid
    }
}
