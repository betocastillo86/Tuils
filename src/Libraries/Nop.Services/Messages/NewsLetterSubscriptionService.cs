using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Messages;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Logging;

namespace Nop.Services.Messages
{
    /// <summary>
    /// Newsletter subscription service
    /// </summary>
    public class NewsLetterSubscriptionService : INewsLetterSubscriptionService
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IDbContext _context;
        private readonly ILogger _logger;
        private readonly IRepository<NewsLetterSubscription> _subscriptionRepository;

        public NewsLetterSubscriptionService(IDbContext context,
            IRepository<NewsLetterSubscription> subscriptionRepository,
            IEventPublisher eventPublisher,
            ILogger logger)
        {
            _context = context;
            _subscriptionRepository = subscriptionRepository;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        /// <summary>
        /// Inserts a newsletter subscription
        /// </summary>
        /// <param name="newsLetterSubscription">NewsLetter subscription</param>
        /// <param name="publishSubscriptionEvents">if set to <c>true</c> [publish subscription events].</param>
        public virtual void InsertNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool publishSubscriptionEvents = true)
        {
            if (newsLetterSubscription == null)
            {
                throw new ArgumentNullException("newsLetterSubscription");
            }

            //Handle e-mail
            newsLetterSubscription.Email = CommonHelper.EnsureSubscriberEmailOrThrow(newsLetterSubscription.Email);

            //Persist
            _subscriptionRepository.Insert(newsLetterSubscription);

            //Publish the subscription event 
            if (newsLetterSubscription.Active)
            {
                PublishSubscriptionEvent(newsLetterSubscription.Email, true, publishSubscriptionEvents, newsLetterSubscription.SuscriptionType, newsLetterSubscription.AdditionalInfo);
            }

            //Publish event
            _eventPublisher.EntityInserted(newsLetterSubscription);
        }

        /// <summary>
        /// Updates a newsletter subscription
        /// </summary>
        /// <param name="insertIfNotExist">Si el registro no existe lo crea de nuevo</param>
        /// <param name="newsLetterSubscription">NewsLetter subscription</param>
        /// <param name="publishSubscriptionEvents">if set to <c>true</c> [publish subscription events].</param>
        public virtual void UpdateNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool publishSubscriptionEvents = true)
        {
            if (newsLetterSubscription == null)
            {
                throw new ArgumentNullException("newsLetterSubscription");
            }

            //Handle e-mail
            newsLetterSubscription.Email = CommonHelper.EnsureSubscriberEmailOrThrow(newsLetterSubscription.Email);

            //Get original subscription record
            NewsLetterSubscription originalSubscription = _context.LoadOriginalCopy(newsLetterSubscription);

            //Persist
            _subscriptionRepository.Update(newsLetterSubscription);

            //Publish the subscription event 
            if ((originalSubscription.Active == false && newsLetterSubscription.Active) ||
                (newsLetterSubscription.Active && (originalSubscription.Email != newsLetterSubscription.Email)))
            {
                //If the previous entry was false, but this one is true, publish a subscribe.
                PublishSubscriptionEvent(newsLetterSubscription.Email, true, publishSubscriptionEvents, newsLetterSubscription.SuscriptionType, newsLetterSubscription.AdditionalInfo);
            }

            if ((originalSubscription.Active && newsLetterSubscription.Active) &&
                (originalSubscription.Email != newsLetterSubscription.Email))
            {
                //If the two emails are different publish an unsubscribe.
                PublishSubscriptionEvent(originalSubscription.Email, false, publishSubscriptionEvents, newsLetterSubscription.SuscriptionType, newsLetterSubscription.AdditionalInfo);
            }

            if ((originalSubscription.Active && !newsLetterSubscription.Active))
            {
                //If the previous entry was true, but this one is false
                PublishSubscriptionEvent(originalSubscription.Email, false, publishSubscriptionEvents, newsLetterSubscription.SuscriptionType, newsLetterSubscription.AdditionalInfo);
            }

            //Publish event
            _eventPublisher.EntityUpdated(newsLetterSubscription);
        }

        /// <summary>
        /// Deletes a newsletter subscription
        /// </summary>
        /// <param name="newsLetterSubscription">NewsLetter subscription</param>
        /// <param name="publishSubscriptionEvents">if set to <c>true</c> [publish subscription events].</param>
        public virtual void DeleteNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool publishSubscriptionEvents = true)
        {
            if (newsLetterSubscription == null) throw new ArgumentNullException("newsLetterSubscription");

            _subscriptionRepository.Delete(newsLetterSubscription);

            //Publish the unsubscribe event 
            PublishSubscriptionEvent(newsLetterSubscription.Email, false, publishSubscriptionEvents, newsLetterSubscription.SuscriptionType, newsLetterSubscription.AdditionalInfo);

            //event notification
            _eventPublisher.EntityDeleted(newsLetterSubscription);
        }

        /// <summary>
        /// Gets a newsletter subscription by newsletter subscription identifier
        /// </summary>
        /// <param name="newsLetterSubscriptionId">The newsletter subscription identifier</param>
        /// <returns>NewsLetter subscription</returns>
        public virtual NewsLetterSubscription GetNewsLetterSubscriptionById(int newsLetterSubscriptionId)
        {
            if (newsLetterSubscriptionId == 0) return null;

            return _subscriptionRepository.GetById(newsLetterSubscriptionId);
        }

        /// <summary>
        /// Gets a newsletter subscription by newsletter subscription GUID
        /// </summary>
        /// <param name="newsLetterSubscriptionGuid">The newsletter subscription GUID</param>
        /// <returns>NewsLetter subscription</returns>
        public virtual NewsLetterSubscription GetNewsLetterSubscriptionByGuid(Guid newsLetterSubscriptionGuid)
        {
            if (newsLetterSubscriptionGuid == Guid.Empty) return null;

            var newsLetterSubscriptions = from nls in _subscriptionRepository.Table
                                          where nls.NewsLetterSubscriptionGuid == newsLetterSubscriptionGuid
                                          orderby nls.Id
                                          select nls;

            return newsLetterSubscriptions.FirstOrDefault();
        }

        /// <summary>
        /// Gets a newsletter subscription by email and store ID
        /// </summary>
        /// <param name="email">The newsletter subscription email</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>NewsLetter subscription</returns>
        public virtual NewsLetterSubscription GetNewsLetterSubscriptionByEmailAndStoreId(string email, int storeId, NewsLetterSuscriptionType type = NewsLetterSuscriptionType.General)
        {
            if (!CommonHelper.IsValidEmail(email))
                return null;

            email = email.Trim();

            var newsLetterSubscriptions = from nls in _subscriptionRepository.Table
                                          where nls.Email == email && nls.StoreId == storeId && nls.SuscriptionTypeId == (int)type
                                          orderby nls.Id
                                          select nls;

            return newsLetterSubscriptions.FirstOrDefault();
        }

        /// <summary>
        /// Gets the newsletter subscription list
        /// </summary>
        /// <param name="email">Email to search or string. Empty to load all records.</param>
        /// <param name="storeId">Store identifier. 0 to load all records.</param>
        /// <param name="showHidden">A value indicating whether the not active subscriptions should be loaded</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>NewsLetterSubscription entities</returns>
        public virtual IPagedList<NewsLetterSubscription> GetAllNewsLetterSubscriptions(string email = null,
            int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue,
            bool showHidden = false)
        {
            var query = _subscriptionRepository.Table;
            if (!String.IsNullOrEmpty(email))
            {
                query = query.Where(nls => nls.Email.Contains(email));
            }
            if (storeId > 0)
            {
                query = query.Where(nls => nls.StoreId == storeId);
            }
            if (!showHidden)
            {
                query = query.Where(nls => nls.Active);
            }
            query = query.OrderBy(nls => nls.Email);

            var newsletterSubscriptions = new PagedList<NewsLetterSubscription>(query, pageIndex, pageSize);
            return newsletterSubscriptions;
        }

        /// <summary>
        /// Publishes the subscription event.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="isSubscribe">if set to <c>true</c> [is subscribe].</param>
        /// <param name="publishSubscriptionEvents">if set to <c>true</c> [publish subscription events].</param>
        private void PublishSubscriptionEvent(string email, bool isSubscribe, bool publishSubscriptionEvents, NewsLetterSuscriptionType suscriptionType, string additionalInfo)
        {
            if (publishSubscriptionEvents)
            {
                if (isSubscribe)
                {
                    _eventPublisher.PublishNewsletterSubscribe(email, suscriptionType, additionalInfo);
                }
                else
                {
                    _eventPublisher.PublishNewsletterUnsubscribe(email, suscriptionType);
                }
            }
        }

        /// <summary>
        /// Crea, actualiza o elimina las newsletter para un correo en especifico
        /// </summary>
        /// <param name="email">correo que se desea actualizar</param>
        /// <param name="type">tipo de notificaci�n</param>
        /// <param name="storeId">Tienda es opcional, por defecto es 1</param>
        public bool SwitchNewsletterByEmail(string email, bool active, NewsLetterSuscriptionType type, string additionalInfo, int storeId = 1)
        {

            try
            {
                var newsletter = GetNewsLetterSubscriptionByEmailAndStoreId(email, storeId, type);
                if (newsletter != null)
                {
                    newsletter.Active = active;
                    UpdateNewsLetterSubscription(newsletter);
                    return true;
                }
                else
                {
                    if (active)
                    {
                        InsertNewsLetterSubscription(new NewsLetterSubscription
                        {
                            NewsLetterSubscriptionGuid = Guid.NewGuid(),
                            Email = email,
                            Active = true,
                            StoreId = storeId,
                            CreatedOnUtc = DateTime.UtcNow,
                            SuscriptionTypeId = (int)type
                        });
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error actualizando el newsletter", e);
                return false;
            }

            return true;

        }

        /// <summary>
        /// Inserta una suscripci�n al correo desde el email
        /// </summary>
        /// <param name="email">correo electronico del usuario</param>
        /// <param name="active">activo</param>
        /// <param name="type">tipo de correo enviado</param>
        /// <param name="storeId">tienda, por defecto es la 1</param>
        public void InsertNewsLetterSubscription(string email, bool active, NewsLetterSuscriptionType type, string additionalInfo, int storeId = 1)
        {
            InsertNewsLetterSubscription(new NewsLetterSubscription()
            {
                NewsLetterSubscriptionGuid = Guid.NewGuid(),
                Active = active,
                Email = email,
                SuscriptionTypeId = (int)type,
                StoreId = storeId,
                AdditionalInfo = additionalInfo,
                CreatedOnUtc = DateTime.Now
            });
        }

        /// <summary>
        /// Valida si un email est� suscrito a newslettero no em un tipo
        /// </summary>
        /// <param name="email">correo suscrito</param>
        /// <param name="type">tipo de correo</param>
        /// <param name="storeId">tienda. Por defecto 1</param>
        public bool IsEmailSubscribed(string email, NewsLetterSuscriptionType type, int storeId = 1)
        {
            var news = GetNewsLetterSubscriptionByEmailAndStoreId(email, storeId, type);
            return news != null ? news.Active : false;
        }


    }
}