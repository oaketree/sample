using System.Web.Optimization;

namespace Cms.WebPage
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/content").Include(
                      "~/Content/Scripts/content.min.js"));

            bundles.Add(new StyleBundle("~/bundles/sitecss").Include(
                      "~/Content/Css/bootstrap.min14ed.css",
                      "~/Content/Css/font-awesome.min93e3.css",
                      "~/Content/Css/animate.min.css",
                      "~/Content/Css/style.min862f.css"
                      ));
            

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));




        }
    }
}
