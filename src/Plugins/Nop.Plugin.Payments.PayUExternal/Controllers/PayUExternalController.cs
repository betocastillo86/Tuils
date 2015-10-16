using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;

using Nop.Web.Framework.Controllers;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain;
using Nop.Core.Domain.Catalog;
using Nop.Web;
using Nop.Web.Infrastructure.Cache;
using Nop.Services.Stores;
using Nop.Plugin.Payments.PayUExternal.Models;
using Nop.Plugin.Payments.ExternalPayU;


namespace Nop.Plugin.Payments.PayUExternal.Controllers
{
    public class PayUExternalController : BasePaymentController
    {
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly PayUExternalSettings _settings;
        public PayUExternalController(PayUExternalSettings settings,
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            this._settings = settings;
            this._settingService = settingService;
            this._localizationService = localizationService;
        }
        

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel();
            model.AccountId = _settings.AccountId;
            model.ApiKey = _settings.ApiKey;
            model.ConfirmationUrl = _settings.ConfirmationUrl;
            model.MerchantId = _settings.MerchantId;
            model.ResponseUrl = _settings.ResponseUrl;
            model.UrlPayment = _settings.UrlPayment;
            return View("~/Plugins/Payments.PayUExternal/Views/PayUExternal/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();
            else
            {
                _settings.AccountId = model.AccountId;
                _settings.ApiKey = model.ApiKey;
                _settings.ConfirmationUrl = model.ConfirmationUrl;
                _settings.MerchantId = model.MerchantId;
                _settings.ResponseUrl = model.ResponseUrl;
                _settings.UrlPayment = model.UrlPayment;
                _settingService.SaveSetting(_settings);
                SuccessNotification(_localizationService.GetResource("Admin.Configuration.Updated"));
            }

            return Configure();
        }



        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            throw new NotImplementedException();
        }

        public override Services.Payments.ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            throw new NotImplementedException();
        }
    }
}