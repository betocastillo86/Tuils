using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Infrastructure
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
            var config = new
            {

                configuration = new
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
                    maxFileUploadSize = tuilsSettings.maxFileUploadSize
                }
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

        
        #endregion

    }
}