﻿using System;
using Nop.Core;

namespace Nop.Plugin.Misc.MailChimp.Data
{
    public class MailChimpEventQueueRecord : BaseEntity
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is subscribe.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is subscribe; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsSubscribe { get; set; }

        /// <summary>
        /// Informacion adicional de la suiscripcion del usuario
        /// </summary>
        public virtual string AdditionalInfo { get; set; }

        /// <summary>
        /// Tipo de suscripción
        /// </summary>
        public virtual int SuscriptionTypeId { get; set; }

        public virtual DateTime CreatedOnUtc { get; set; }
    }
}