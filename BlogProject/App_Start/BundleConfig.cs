using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BlogProject.App_Start
{
    public class BundleConfig
    {
       
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").IncludeDirectory("~/Content/Admin/css", "*.css", true));

            bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory("~/Content/Admin/js", "*.js", true));

            bundles.Add(new StyleBundle("~/bundlesSite/styles").IncludeDirectory("~/Content/Site/css", "*.css", true));

            bundles.Add(new ScriptBundle("~/bundlesSite/scripts").IncludeDirectory("~/Content/Site/js", "*.js", true));

            BundleTable.EnableOptimizations = true;
        }

    }
}