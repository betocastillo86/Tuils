using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Nop.Web.Infrastructure
{
    /// <summary>
    /// Clase creada para controlar los nuevos estilos agregados en Tuils
    /// </summary>
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/tuils/backboneFront")
                .Include("~/Scripts/backbone/tuils.app.js",
                         "~/Scripts/backbone/tuils.router.js",
                         "~/Scripts/underscore.js",
                         "~/Scripts/backbone.js"
                ));
        }
    }
}