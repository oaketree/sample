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
                        "~/Content/Scripts/angular.min.js",
                        "~/Content/Scripts/angular-route.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/reg").Include(
                        "~/Content/Scripts/common.js",
                        "~/Content/Scripts/gygl.js",
                        "~/Content/Scripts/Register/gygl.reg.js"));

            //bundles.Add(new ScriptBundle("~/bundles/mag").Include(
            //    "~/Content/Scripts/Magazine/app.js",
            //    "~/Content/Scripts/Magazine/routers.js",
            //    "~/Content/Scripts/Magazine/services.js",
            //    "~/Content/Scripts/Magazine/controllers.js",
            //    "~/Content/Scripts/Magazine/directives.js",
            //            "~/Content/Scripts/common.js",
            //            "~/Content/Scripts/gygl.js"
            //            ));

            bundles.Add(new ScriptBundle("~/bundles/mag")
                .IncludeDirectory("~/Content/Scripts/Share", "*.js")
                .IncludeDirectory("~/Content/Scripts/Magazine", "*.js")
                .Include("~/Content/Scripts/common.js", "~/Content/Scripts/gygl.js"));


            bundles.Add(new ScriptBundle("~/bundles/home")
                .IncludeDirectory("~/Content/Scripts/Home", "*.js")
                .Include("~/Content/Scripts/common.js","~/Content/Scripts/gygl.js"));


            bundles.Add(new ScriptBundle("~/bundles/news")
                .IncludeDirectory("~/Content/Scripts/Share", "*.js")
                .IncludeDirectory("~/Content/Scripts/News", "*.js")
                .Include("~/Content/Scripts/common.js","~/Content/Scripts/gygl.js"));

            bundles.Add(new ScriptBundle("~/bundles/static").Include(
                "~/Content/Scripts/jquery.print.js",
                        "~/Content/Scripts/common.js",
                        "~/Content/Scripts/gygl.js"));


            bundles.Add(new StyleBundle("~/Content/homecss").Include(
                     "~/Content/Css/magazine.css"));

            bundles.Add(new StyleBundle("~/Content/staticss").Include(
                "~/Content/Css/gygl.css",
                     "~/Content/Css/magazinenei.css",
                      "~/Content/Css/magazineneire.css",
                      "~/Content/Css/static.css"));


            bundles.Add(new StyleBundle("~/Content/registercss").Include(
                     "~/Content/Css/gygl.css",
                      "~/Content/Css/magazinenei.css",
                      "~/Content/Css/magazineneire.css"));

            bundles.Add(new StyleBundle("~/Content/magazinecss").Include(
                "~/Content/Css/gygl.css",
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
