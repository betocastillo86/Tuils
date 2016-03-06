﻿using System;

namespace Nop.Core.Domain.Messages
{
    /// <summary>
    /// Represents NewsLetterSubscription entity
    /// </summary>
    public partial class NewsLetterSubscription : BaseEntity
    {       
        /// <summary>
        /// Gets or sets the newsletter subscription GUID
        /// </summary>
        public Guid NewsLetterSubscriptionGuid { get; set; }

        /// <summary>
        /// Gets or sets the subcriber email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether subscription is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the store identifier in which a customer has subscribed to newsletter
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when subscription was created
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Tipo de suscripción a la que se une el cliente
        /// </summary>
        public int SuscriptionTypeId { get; set; }

        /// <summary>
        /// Informacion adicional de la suscripción
        /// </summary>
        public string AdditionalInfo { get; set; }

        public NewsLetterSuscriptionType SuscriptionType { get { return (NewsLetterSuscriptionType)SuscriptionTypeId; } set { SuscriptionTypeId = Convert.ToInt32(value); } }
    }
}
