﻿using System.Web.Mvc;
using Nop.Core.Infrastructure;
using Nop.Core.Domain.Common;

namespace Nop.Web.Framework.UI
{
    public static class LayoutExtensions
    {

        /// <summary>
        /// Add title element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Title part</param>
        public static void AddTitleParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder  = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddTitleParts(part);
        }
        /// <summary>
        /// Append title element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Title part</param>
        public static void AppendTitleParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder  = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendTitleParts(part);
        }
        /// <summary>
        /// Generate all title parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="addDefaultTitle">A value indicating whether to insert a default title</param>
        /// <param name="part">Title part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopTitle(this HtmlHelper html, bool addDefaultTitle, string part = "")
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            html.AppendTitleParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateTitle(addDefaultTitle)));
        }


        /// <summary>
        /// Add meta description element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta description part</param>
        public static void AddMetaDescriptionParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddMetaDescriptionParts(part);
        }
        /// <summary>
        /// Append meta description element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta description part</param>
        public static void AppendMetaDescriptionParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendMetaDescriptionParts(part);
        }
        /// <summary>
        /// Generate all description parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta description part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopMetaDescription(this HtmlHelper html, string part = "")
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            html.AppendMetaDescriptionParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateMetaDescription()));
        }


        /// <summary>
        /// Add meta keyword element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta keyword part</param>
        public static void AddMetaKeywordParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddMetaKeywordParts(part);
        }
        /// <summary>
        /// Append meta keyword element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta keyword part</param>
        public static void AppendMetaKeywordParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendMetaKeywordParts(part);
        }
        /// <summary>
        /// Generate all keyword parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta keyword part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopMetaKeywords(this HtmlHelper html, string part = "")
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            html.AppendMetaKeywordParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateMetaKeywords()));
        }


        /// <summary>
        /// Add script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AddScriptParts(this HtmlHelper html, string part, bool excludeFromBundle = false)
        {
            AddScriptParts(html, ResourceLocation.Head, part, excludeFromBundle);
        }
        /// <summary>
        /// Add script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AddScriptParts(this HtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddScriptParts(location, part, excludeFromBundle);
        }
        /// <summary>
        /// Append script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AppendScriptParts(this HtmlHelper html, string part, bool excludeFromBundle = false)
        {
            AppendScriptParts(html, ResourceLocation.Head, part, excludeFromBundle);
        }
        /// <summary>
        /// Append script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AppendScriptParts(this HtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendScriptParts(location, part, excludeFromBundle);
        }

        /// <summary>
        /// Agrega los recursos basicos de Backbone, es decir backbonejs, underscorejs y la app y router
        /// </summary>
        /// <param name="html">helper</param>
        public static void AppendScriptsBasicBackbone(this HtmlHelper html)
        {
            AppendScriptParts(html, ResourceLocation.Head, "~/Scripts/backbone/tuils.storage.js", false);
            AppendScriptParts(html, ResourceLocation.Head, "~/Scripts/backbone/tuils.router.js", false);
            AppendScriptParts(html, ResourceLocation.Head, "~/Scripts/backbone/tuils.app.js", false);
            AppendScriptParts(html, ResourceLocation.Head, "~/Scripts/backbone.stickit.min.js", false);
            AppendScriptParts(html, ResourceLocation.Head, "~/Scripts/backbone-validation.min.js", false);
            AppendScriptParts(html, ResourceLocation.Head, "~/Scripts/backbone.js", false);
            AppendScriptParts(html, ResourceLocation.Head, "~/Scripts/underscore.js", false);
        }
        
        
        /// <summary>
        /// Agrega una parte creada previmente de JS
        /// </summary>
        //public static void AppendScriptPart(this HtmlHelper html, ResourceLocation location, params string[] parts)
        //{
        //    foreach (var part in parts)
        //    {
                    
        //    }
        //}

        /// <summary>
        /// Generate all script parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="urlHelper">URL Helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="bundleFiles">A value indicating whether to bundle script elements</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopScripts(this HtmlHelper html, UrlHelper urlHelper, 
            ResourceLocation location, bool? bundleFiles = null)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            return MvcHtmlString.Create(pageHeadBuilder.GenerateScripts(urlHelper, location, bundleFiles));
        }



        /// <summary>
        /// Add CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS part</param>
        public static void AddCssFileParts(this HtmlHelper html, string part)
        {
            AddCssFileParts(html, ResourceLocation.Head, part);
        }
        /// <summary>
        /// Add CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">CSS part</param>
        public static void AddCssFileParts(this HtmlHelper html, ResourceLocation location, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddCssFileParts(location, part);
        }
        /// <summary>
        /// Append CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS part</param>
        public static void AppendCssFileParts(this HtmlHelper html, string part)
        {
            AppendCssFileParts(html, ResourceLocation.Head, part);
        }
        /// <summary>
        /// Append CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">CSS part</param>
        public static void AppendCssFileParts(this HtmlHelper html, ResourceLocation location, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendCssFileParts(location, part);
        }
        /// <summary>
        /// Generate all CSS parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="urlHelper">URL Helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="bundleFiles">A value indicating whether to bundle script elements</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopCssFiles(this HtmlHelper html, UrlHelper urlHelper,
            ResourceLocation location, bool? bundleFiles = null)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            return MvcHtmlString.Create(pageHeadBuilder.GenerateCssFiles(urlHelper, location, bundleFiles));
        }


        /// <summary>
        /// Add canonical URL element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Canonical URL part</param>
        public static void AddCanonicalUrlParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddCanonicalUrlParts(part);
        }
        /// <summary>
        /// Append canonical URL element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Canonical URL part</param>
        public static void AppendCanonicalUrlParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendCanonicalUrlParts(part);
        }
        /// <summary>
        /// Generate all canonical URL parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Canonical URL part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopCanonicalUrls(this HtmlHelper html, string part = "")
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            html.AppendCanonicalUrlParts(part);
            return MvcHtmlString.Create(pageHeadBuilder.GenerateCanonicalUrls());
        }

        /// <summary>
        /// Add any custom element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">The entire element. For example, <![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
        public static void AddHeadCustomParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddHeadCustomParts(part);
        }


        public static void AddHeadFacebookPixel(this HtmlHelper html, string action, int part = 0)
        {
            var settings = EngineContext.Current.Resolve<CommonSettings>();

            //Valida que la llave este configurada
            if (!string.IsNullOrEmpty(settings.ActiveFacebookPixels))
            {
                //realiza split para buscar el pixel por indice
                var activePixels = settings.ActiveFacebookPixels.Split(new char[]{ '-' });
                if (activePixels.Length < part + 1)
                    return;

                string pixelId = activePixels[part];
                
                var pixelString = new System.Text.StringBuilder();
                pixelString.Append("<!-- Facebook Pixel Code -->");
                pixelString.Append("<script>");
                pixelString.Append("!function(f,b,e,v,n,t,s){if(f.fbq)return;n=f.fbq=function(){n.callMethod?");
                pixelString.Append("n.callMethod.apply(n,arguments):n.queue.push(arguments)};if(!f._fbq)f._fbq=n;");
                pixelString.Append("n.push=n;n.loaded=!0;n.version='2.0';n.queue=[];t=b.createElement(e);t.async=!0;");
                pixelString.Append("t.src=v;s=b.getElementsByTagName(e)[0];s.parentNode.insertBefore(t,s)}(window,");
                pixelString.Append("document,'script','//connect.facebook.net/en_US/fbevents.js');");
                pixelString.AppendFormat("fbq('init', '{0}');", pixelId);
                pixelString.AppendFormat("fbq('track', \"{0}\");</script>", action);
                pixelString.Append("<noscript><img height=\"1\" width=\"1\" style='display:none'");
                pixelString.AppendFormat("src=\"https://www.facebook.com/tr?id={0}&ev={1}&noscript=1\"", pixelId, action);
                pixelString.Append("/></noscript>");
                pixelString.Append("<!-- End Facebook Pixel Code -->");

                var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
                pageHeadBuilder.AddHeadCustomParts(pixelString.ToString());
            }


            
        }

        /// <summary>
        /// Append any custom element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">The entire element. For example, <![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
        public static void AppendHeadCustomParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendHeadCustomParts(part);
        }
        /// <summary>
        /// Generate all custom elements
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopHeadCustom(this HtmlHelper html)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            return MvcHtmlString.Create(pageHeadBuilder.GenerateHeadCustom());
        }
    }
}
