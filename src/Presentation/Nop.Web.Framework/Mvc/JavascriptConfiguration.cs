using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Web.Framework.UI.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Framework.Mvc
{
    /// <summary>
    /// Clase que genera el archivo javascript que contiene la configuración de la aplicación
    /// </summary>
    public class JavascriptConfiguration
    {

        #region Methods
        /// <summary>
        /// Crea el javascript que genera toda la configuración hacia los usuarios
        /// </summary>
        /// <param name="tuilsSettings"></param>
        public static void CreateJavascriptConfigurationFile(TuilsSettings tuilsSettings)
        {
            var catalogSettings = EngineContext.Current.Resolve<CatalogSettings>();
            var captchaSettings = EngineContext.Current.Resolve<CaptchaSettings>();
            var dateSettings = EngineContext.Current.Resolve<DateTimeSettings>();
            var mediaSettings = EngineContext.Current.Resolve<MediaSettings>();
            var vendorSettings = EngineContext.Current.Resolve<VendorSettings>();
            var planSettings = EngineContext.Current.Resolve<PlanSettings>();

            var config = new
            {


                //Configuración de las categorias base
                productBaseTypes = new
                {
                    product = tuilsSettings.productBaseTypes_product,
                    service = tuilsSettings.productBaseTypes_service,
                    bike = tuilsSettings.productBaseTypes_bike
                },
                specialCategories = new
                {
                    bikeBrand = (int)SpecialCategoryProductType.BikeBrand
                },
                specialCategoriesVendor = new
                {
                    bikeBrand = (int)SpecialCategoryVendorType.BikeBrand,
                    specializedCategory = (int)SpecialCategoryVendorType.SpecializedCategory
                },
                maxFileUploadSize = tuilsSettings.maxFileUploadSize,
                searchWithSearchTerms = catalogSettings.ProductSearchAutoCompleteWithSearchTerms,
                captcha = new
                {
                    showOnQuestions = captchaSettings.ShowOnProductQuestions
                },
                jquery = new
                {
                    dateFormat = dateSettings.JqueryFormat
                },
                media = new
                {
                    productImageMaxSizeResize = mediaSettings.ProductImageMaxSizeResize,
                    coverImageMaxSizeResize = mediaSettings.CoverImageMaxSizeResize,
                    logoImageMaxSizeResize = mediaSettings.LogoImageMaxSizeResize,
                    vendorBackgroundThumbPictureSize = mediaSettings.VendorBackgroundThumbPictureSize,
                    productthumbpicturesizeonproductdetailspage = mediaSettings.ProductThumbPictureSizeOnProductDetailsPage
                },
                plan = new {
                    planProductsFree = planSettings.PlanProductsFree,
                    planStoresFree = planSettings.PlanStoresFree
                },
                vendor = new
                {
                    reviewsPageSize = vendorSettings.DefaultReviewsPageSize,
                    minWidthCover = vendorSettings.MinWidthCover,
                    minHeightCover = vendorSettings.MinHeightCover
                },
                catalog = new
                {
                    limitOfSpecialCategories = catalogSettings.LimitOfSpecialCategories,
                    //limitDaysOfProductPublished = catalogSettings.LimitDaysOfProductPublished,
                    limitNumPictures = catalogSettings.LimitNumPictures,
                    disabledCategoriesForUsedProducts = catalogSettings.DisabledCategoriesForUsedProducts
                },
                errorCodes = new
                {
                    publishInvalidCategory = (int)CodeNopException.UserTypeNotAllowedPublishProductType,
                    hasReachedLimitOfProducts = (int)CodeNopException.UserHasReachedLimitOfProducts,
                    hasPublishedSimilarProduct = (int)CodeNopException.UserHasHasPublishedSimilarProduct
                },
                //Cada vez que se recargue la información del javascript los clientes
                //deben eliminar del localstorage las referencias de motos que tienen
                expirationBikeReferencesKey = tuilsSettings.ExpirationBikeReferencesKey
            };


            //Convierte el valor del json a un string
            var jsonString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(config);

            using (var sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/Scripts/tuils/configuration.js")))
            {
                //sw.Write("define(['jquery','backboneApp'], function($, TuilsApp){$.extend(TuilsApp, {0});})".Replace("{0}", jsonString));
                sw.Write("define([], function(){var TuilsConfiguration = {0}; return TuilsConfiguration; });".Replace("{0}", jsonString));
                sw.Close();
            }
        }

        public static void CreateJavascriptResourcesFile()
        {
            var _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            //Se quema idioma en el que debe generar el archivo de resources
            //ya que desde el global.asax no puede acceder al request
            int languageId = 2;
            var Resources = new
            {
                account = new
                {
                    login = _localizationService.GetResource("account.login", languageId),
                    newCustomer = _localizationService.GetResource("account.login.newcustomer", languageId)
                },
                products = new
                {
                    confirmBuy = _localizationService.GetResource("products.confirmBuy", languageId),
                    hasReachedLimitFeaturedAlert = _localizationService.GetResource("myproducts.hasReachedLimitFeaturedAlert", languageId),
                    defaultErrorPublishingProduct = _localizationService.GetResource("PublishProduct.DefaultErrorResponse", languageId),
                    hasPublisedSimilarProduct = _localizationService.GetResource("PublishProduct.AskUserPublishSimilarProduct", languageId)
                },
                loginMessages = new
                {
                    publishProduct = _localizationService.GetResource("LoginMessage.PublishProduct", languageId),
                    showVendor = _localizationService.GetResource("LoginMessage.ShowVendor", languageId),
                    askQuestion = _localizationService.GetResource("LoginMessage.AskQuestion", languageId),
                    getPlanMarketLikeUserError = _localizationService.GetResource("LoginMessage.GetPlanMarketLikeUserError", languageId),
                },
                confirm = new
                {
                    myAccount = _localizationService.GetResource("MyAccount.Confirm", languageId),
                    //offices = _localizationService.GetResource("MyOffices.Confirm", languageId),
                    closeButton = _localizationService.GetResource("Common.CloseButtonDialog", languageId),
                    userRegistered = _localizationService.GetResource("createuser.ConfirmMessage", languageId)
                }
            };

            //Convierte el valor del json a un string
            var jsonString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Resources);
            using (var sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/Scripts/tuils/resources.js")))
            {
                sw.Write("define([], function(){var TuilsResources = {0}; return TuilsResources; });".Replace("{0}", jsonString));
                sw.Close();
            }
        }
        #endregion

    }
}