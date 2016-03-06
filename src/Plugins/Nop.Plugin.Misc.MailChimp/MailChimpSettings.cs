using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.MailChimp
{
    public class MailChimpSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public virtual string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the web hook key.
        /// </summary>
        /// <value>
        /// The web hook key.
        /// </value>
        public virtual string WebHookKey { get; set; }

        /// <summary>
        /// Gets or sets the default list id.
        /// </summary>
        /// <value>
        /// The default list id.
        /// </value>
        public virtual string DefaultListId { get; set; }

        /// <summary>
        /// Lista en mailchimp para la suscripción al newsletter
        /// </summary>
        public virtual string GeneralSuscriptionListId { get; set; }

        /// <summary>
        /// Id de la lista para usuarios registrados como usuario Natural
        /// </summary>
        public virtual string UserSuscriptionListId { get; set; }

        /// <summary>
        /// Id de la lista para usuarios registrados como tienda
        /// </summary>
        public virtual string ShopSuscriptionListId { get; set; }

        /// <summary>
        /// Id de la lista para usuarios registrados como taller
        /// </summary>
        public virtual string RepairShopSuscriptionListId { get; set; }

        public virtual string LandingRepairShopSuscriptionListId { get; set; }
        public virtual string LandingStoreSuscriptionListId { get; set; }

        /// <summary>
        /// Habilita el doubleoptin en la suscripción a la lista
        /// </summary>
        public virtual bool DoubleOptin { get; set; }
    }
}