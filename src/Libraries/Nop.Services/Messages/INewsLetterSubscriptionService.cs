using System;
using Nop.Core;
using Nop.Core.Domain.Messages;

namespace Nop.Services.Messages
{
    /// <summary>
    /// Newsletter subscription service interface
    /// </summary>
    public partial interface INewsLetterSubscriptionService
    {
        /// <summary>
        /// Inserts a newsletter subscription
        /// </summary>
        /// <param name="newsLetterSubscription">NewsLetter subscription</param>
        /// <param name="publishSubscriptionEvents">if set to <c>true</c> [publish subscription events].</param>
        void InsertNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool publishSubscriptionEvents = true);

        /// <summary>
        /// Inserta una suscripción al correo desde el email
        /// </summary>
        /// <param name="email">correo electronico del usuario</param>
        /// <param name="active">activo</param>
        /// <param name="type">tipo de correo enviado</param>
        /// <param name="storeId">tienda, por defecto es la 1</param>
        void InsertNewsLetterSubscription(string email, bool active, NewsLetterSuscriptionType type, string additionalInfo, int storeId = 1);
        
        /// <summary>
        /// Updates a newsletter subscription
        /// </summary>
        /// <param name="newsLetterSubscription">NewsLetter subscription</param>
        /// <param name="publishSubscriptionEvents">if set to <c>true</c> [publish subscription events].</param>
        void UpdateNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool publishSubscriptionEvents = true);

        /// <summary>
        /// Deletes a newsletter subscription
        /// </summary>
        /// <param name="newsLetterSubscription">NewsLetter subscription</param>
        /// <param name="publishSubscriptionEvents">if set to <c>true</c> [publish subscription events].</param>
        void DeleteNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool publishSubscriptionEvents = true);

        /// <summary>
        /// Crea, actualiza o elimina las newsletter para un correo en especifico
        /// </summary>
        /// <param name="email">correo que se desea actualizar</param>
        /// <param name="type">tipo de notificación</param>
        /// <param name="storeId">Tienda es opcional, por defecto es 1</param>
        bool SwitchNewsletterByEmail(string email, bool active,  NewsLetterSuscriptionType type, string additionalInfo, int storeId = 1);

        /// <summary>
        /// Gets a newsletter subscription by newsletter subscription identifier
        /// </summary>
        /// <param name="newsLetterSubscriptionId">The newsletter subscription identifier</param>
        /// <returns>NewsLetter subscription</returns>
        NewsLetterSubscription GetNewsLetterSubscriptionById(int newsLetterSubscriptionId);

        /// <summary>
        /// Gets a newsletter subscription by newsletter subscription GUID
        /// </summary>
        /// <param name="newsLetterSubscriptionGuid">The newsletter subscription GUID</param>
        /// <returns>NewsLetter subscription</returns>
        NewsLetterSubscription GetNewsLetterSubscriptionByGuid(Guid newsLetterSubscriptionGuid);

        /// <summary>
        /// Gets a newsletter subscription by email and store ID
        /// </summary>
        /// <param name="email">The newsletter subscription email</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="type">Tipo de suscripción por la que se busca, por defecto es la suscripción general</param>
        /// <returns>NewsLetter subscription</returns>
        NewsLetterSubscription GetNewsLetterSubscriptionByEmailAndStoreId(string email, int storeId, NewsLetterSuscriptionType type = NewsLetterSuscriptionType.General);

        /// <summary>
        /// Gets the newsletter subscription list
        /// </summary>
        /// <param name="email">Email to search or string. Empty to load all records.</param>
        /// <param name="storeId">Store identifier. 0 to load all records.</param>
        /// <param name="showHidden">A value indicating whether the not active subscriptions should be loaded</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>NewsLetterSubscription entities</returns>
        IPagedList<NewsLetterSubscription> GetAllNewsLetterSubscriptions(string email = null,
            int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue, 
            bool showHidden = false);


        /// <summary>
        /// Valida si un email está suscrito a newslettero no em un tipo
        /// </summary>
        /// <param name="email">correo suscrito</param>
        /// <param name="type">tipo de correo</param>
        /// <param name="storeId">tienda. Por defecto 1</param>
        bool IsEmailSubscribed(string email, NewsLetterSuscriptionType type, int storeId = 1);
    }
}
