using Nop.Core.Data;
using Nop.Core.Domain.Messages;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace Nop.Services.Tests.Messages 
{
    [TestFixture]
    public class NewsLetterSubscriptionServiceTests
    {
        /// <summary>
        /// Verifies the active insert triggers subscribe event.
        /// </summary>
        [Test]
        public void VerifyActiveInsertTriggersSubscribeEvent()
        {
            var eventPublisher = MockRepository.GenerateStub<IEventPublisher>();
            var repo = MockRepository.GenerateStub<IRepository<NewsLetterSubscription>>();
            var context = MockRepository.GenerateStub<IDbContext>();
            var logger = MockRepository.GenerateStub<ILogger>();
            NewsLetterSuscriptionType suscriptionType = NewsLetterSuscriptionType.General;
            string additionalInfo = string.Empty;

            var subscription = new NewsLetterSubscription { Active = true, Email = "skyler@csharpwebdeveloper.com", SuscriptionTypeId = Convert.ToInt32(NewsLetterSuscriptionType.General), AdditionalInfo = string.Empty };

            var service = new NewsLetterSubscriptionService(context, repo, eventPublisher, logger);
            service.InsertNewsLetterSubscription(subscription, true);

            eventPublisher.AssertWasCalled(x => x.Publish(new EmailSubscribedEvent(subscription.Email, suscriptionType, additionalInfo)));
        }

        /// <summary>
        /// Verifies the delete triggers unsubscribe event.
        /// </summary>
        [Test]
        public void VerifyDeleteTriggersUnsubscribeEvent()
        {
            var eventPublisher = MockRepository.GenerateStub<IEventPublisher>();
            var repo = MockRepository.GenerateStub<IRepository<NewsLetterSubscription>>();
            var context = MockRepository.GenerateStub<IDbContext>();
            var logger = MockRepository.GenerateStub<ILogger>();

            var subscription = new NewsLetterSubscription { Active = true, Email = "skyler@csharpwebdeveloper.com", SuscriptionTypeId = Convert.ToInt32(NewsLetterSuscriptionType.General), AdditionalInfo = string.Empty };

            var service = new NewsLetterSubscriptionService(context, repo, eventPublisher, logger);
            service.DeleteNewsLetterSubscription(subscription, true);

            eventPublisher.AssertWasCalled(x => x.Publish(new EmailUnsubscribedEvent(subscription.Email, NewsLetterSuscriptionType.General)));
        }

        /// <summary>
        /// Verifies the email update triggers unsubscribe and subscribe event.
        /// </summary>
        [Test]
        [Ignore("Ignoring until a solution to the IDbContext methods are found. -SRS")]
        public void VerifyEmailUpdateTriggersUnsubscribeAndSubscribeEvent()
        {
            var eventPublisher = MockRepository.GenerateStub<IEventPublisher>();
            var repo = MockRepository.GenerateStub<IRepository<NewsLetterSubscription>>();
            var context = MockRepository.GenerateStub<IDbContext>();

            //Prepare the original result
            var originalSubscription = new NewsLetterSubscription { Active = true, Email = "skyler@csharpwebdeveloper.com" };
            repo.Stub(m => m.GetById(Arg<object>.Is.Anything)).Return(originalSubscription);

            var subscription = new NewsLetterSubscription { Active = true, Email = "skyler@tetragensoftware.com" };
            var logger = MockRepository.GenerateStub<ILogger>();
            var service = new NewsLetterSubscriptionService(context, repo, eventPublisher, logger);
            service.UpdateNewsLetterSubscription(subscription, true);

            eventPublisher.AssertWasCalled(x => x.Publish(new EmailUnsubscribedEvent(originalSubscription.Email, NewsLetterSuscriptionType.General)));
            eventPublisher.AssertWasCalled(x => x.Publish(new EmailSubscribedEvent(subscription.Email, NewsLetterSuscriptionType.General, string.Empty)));
        }

        /// <summary>
        /// Verifies the inactive to active update triggers subscribe event.
        /// </summary>
        [Test]
        [Ignore("Ignoring until a solution to the IDbContext methods are found. -SRS")]
        public void VerifyInactiveToActiveUpdateTriggersSubscribeEvent()
        {
            var eventPublisher = MockRepository.GenerateStub<IEventPublisher>();
            var repo = MockRepository.GenerateStub<IRepository<NewsLetterSubscription>>();
            var context = MockRepository.GenerateStub<IDbContext>();
            var logger = MockRepository.GenerateStub<ILogger>();

            //Prepare the original result
            var originalSubscription = new NewsLetterSubscription { Active = false, Email = "skyler@csharpwebdeveloper.com" };
            repo.Stub(m => m.GetById(Arg<object>.Is.Anything)).Return(originalSubscription);

            var subscription = new NewsLetterSubscription { Active = true, Email = "skyler@csharpwebdeveloper.com" };

            var service = new NewsLetterSubscriptionService(context, repo, eventPublisher, logger);
            service.UpdateNewsLetterSubscription(subscription, true);

            eventPublisher.AssertWasCalled(x => x.Publish(new EmailSubscribedEvent(subscription.Email, NewsLetterSuscriptionType.General, string.Empty)));
        }

        /// <summary>
        /// Verifies the insert event is fired.
        /// </summary>
        [Test]
        public void VerifyInsertEventIsFired()
        {
            var eventPublisher = MockRepository.GenerateStub<IEventPublisher>();
            var repo = MockRepository.GenerateStub<IRepository<NewsLetterSubscription>>();
            var context = MockRepository.GenerateStub<IDbContext>();
            var logger = MockRepository.GenerateStub<ILogger>();

            var service = new NewsLetterSubscriptionService(context, repo, eventPublisher, logger);
            service.InsertNewsLetterSubscription(new NewsLetterSubscription { Email = "skyler@csharpwebdeveloper.com" });

            eventPublisher.AssertWasCalled(x => x.EntityInserted(Arg<NewsLetterSubscription>.Is.Anything));
        }

        /// <summary>
        /// Verifies the update event is fired.
        /// </summary>
        [Test]
        [Ignore("Ignoring until a solution to the IDbContext methods are found. -SRS")]
        public void VerifyUpdateEventIsFired()
        {
            var eventPublisher = MockRepository.GenerateStub<IEventPublisher>();
            var repo = MockRepository.GenerateStub<IRepository<NewsLetterSubscription>>();
            var context = MockRepository.GenerateStub<IDbContext>();

            //Prepare the original result
            var originalSubscription = new NewsLetterSubscription { Active = false, Email = "skyler@csharpwebdeveloper.com" };
            repo.Stub(m => m.GetById(Arg<object>.Is.Anything)).Return(originalSubscription);
            var logger = MockRepository.GenerateStub<ILogger>();
            var service = new NewsLetterSubscriptionService(context, repo, eventPublisher, logger);
            service.UpdateNewsLetterSubscription(new NewsLetterSubscription { Email = "skyler@csharpwebdeveloper.com" });

            eventPublisher.AssertWasCalled(x => x.EntityUpdated(Arg<NewsLetterSubscription>.Is.Anything));
        }
    }
}