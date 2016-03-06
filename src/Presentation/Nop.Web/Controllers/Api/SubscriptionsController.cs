using Nop.Core;
using Nop.Core.Domain.Messages;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nop.Web.Controllers.Api
{
    [Route("api/subscriptions")]
    public class SubscriptionsController : ApiController
    {
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;

        public SubscriptionsController(INewsLetterSubscriptionService newsLetterSubscriptionService,
            IWorkflowMessageService workflowMessageService,
            IStoreContext storeContext,
            IWorkContext workContext,
            ILocalizationService localizationService)
        {
            _newsLetterSubscriptionService = newsLetterSubscriptionService;
            _workflowMessageService = workflowMessageService;
            _storeContext = storeContext;
            _workContext = workContext;
            _localizationService = localizationService;
        }
        
        /// <summary>
        /// Da de alta un usuario en el newsletter
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult Post(SubscriptionModel model)
        {
            if (ModelState.IsValid)
            {

                NewsLetterSuscriptionType subscriptionType;
                bool activeToMailchimp = true;
                switch (model.Type)
                {
                    case "landing-store":
                        subscriptionType = NewsLetterSuscriptionType.LandingStores;
                        break;
                    case "landing-repair":
                        subscriptionType = NewsLetterSuscriptionType.LandingRepairs;
                        break;
                    default:
                        subscriptionType = NewsLetterSuscriptionType.General;
                        break;
                }
                
                var subscription = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(model.Email, _storeContext.CurrentStore.Id);
                if (subscription != null)
                {
                    if (!subscription.Active)
                    {
                        _workflowMessageService.SendNewsLetterSubscriptionActivationMessage(subscription, _workContext.WorkingLanguage.Id);
                    }

                    return Ok(new { id = subscription.Id });
                }
                else
                {
                    subscription = new NewsLetterSubscription
                    {
                        NewsLetterSubscriptionGuid = Guid.NewGuid(),
                        Email = model.Email,
                        Active = activeToMailchimp,
                        StoreId = _storeContext.CurrentStore.Id,
                        CreatedOnUtc = DateTime.UtcNow,
                        SuscriptionType = subscriptionType,
                        AdditionalInfo = string.Format("{0}|{1}|{2}",model.Name,  model.Company, model.Phone)
                    };
                    _newsLetterSubscriptionService.InsertNewsLetterSubscription(subscription);
                    _workflowMessageService.SendNewsLetterSubscriptionActivationMessage(subscription, _workContext.WorkingLanguage.Id);

                    return Ok(new { id = subscription.Id });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
