using System.Web.Optimization;

namespace Gygl.WebPage
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Content/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/reg").Include(
                        "~/Content/Scripts/common.js",
                        "~/Content/Scripts/gygl.js",
                        "~/Content/Scripts/gygl.reg.js"));

            //bundles.Add(new ScriptBundle("~/bundles/contribute").Include(
            //            "~/Content/Scripts/common.js",
            //            "~/Content/Scripts/gygl.js",
            //            "~/Content/Scripts/gygl.co.js"));

            bundles.Add(new ScriptBundle("~/bundles/mag").Include(
                        "~/Content/Scripts/common.js",
                        "~/Content/Scripts/gygl.js",
                        "~/Content/Scripts/gygl.ma.js"));

            bundles.Add(new ScriptBundle("~/bundles/catalog").Include(
                        "~/Content/Scripts/jquery-ui.min.js",
                        "~/Content/Scripts/jquery.simplesidebar.js"));


            bundles.Add(new StyleBundle("~/Content/registercss").Include(
                     "~/Content/Css/gygl.css",
                      "~/Content/Css/magazinenei.css",
                      "~/Content/Css/magazineneire.css"));

            bundles.Add(new StyleBundle("~/Content/magazinecss").Include(
                      "~/Content/Css/magazinenei.css",
                      "~/Content/Css/magazinereading.css",
                      "~/Content/Css/font-awesome.min.css"));

            //bundles.Add(new StyleBundle("~/Content/contributecss").Include(
            //          "~/Content/Css/magazinenei.css",
            //          "~/Content/Css/magazineneire.css",
            //          "~/Content/Css/buttoncss.css"));
        }
    }
}
