﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Web.Framework.Mvc
{
    /// <summary>
    /// Base nopCommerce model
    /// </summary>
    [ModelBinder(typeof(NopModelBinder))]
    public partial class BaseNopModel
    {
        public BaseNopModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
            
        }

        public bool IsMobileDevice { get; set; }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }
    }

    /// <summary>
    /// Base nopCommerce entity model
    /// </summary>
    public partial class BaseNopEntityModel : BaseNopModel
    {
        public virtual int Id { get; set; }
    }

    public partial class BaseNopCampaignEntityModel : BaseNopEntityModel
    {
        public bool HasCampaign { get { return !string.IsNullOrEmpty(AnalyticsSource) && !string.IsNullOrEmpty(AnalyticsMedium) && !string.IsNullOrEmpty(AnalyticsCampaign); } }

        public string AnalyticsSource { get; set; }

        public string AnalyticsMedium { get; set; }

        public string AnalyticsCampaign { get; set; }
    }
}
