using System.Web.Optimization;

namespace Tamin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/Tamin").Include(
                                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap-fileupload.js",
                "~/Scripts/bootstrap-rtl.js",
                "~/Scripts/jquery.dcjqaccordion.2.7.js",
                "~/Scripts/jquery.scrollTo.js",
                "~/Scripts/jquery.slimscroll.js",
                "~/Scripts/jquery.nicescroll.js",
                "~/Scripts/jquery.scrollTo/jquery.scrollTo",
                "~/Scripts/js/data-tables/jquery.dataTables.js",
                "~/Scripts/js/data-tables/DT_bootstrap.js",
                "~/Scripts/js/scripts.js",
                "~/Scripts/js/table-editable.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.js",
                        "~/Scripts/price-range.js",
                        "~/Scripts/jquery.prettyPhoto.js",
                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap-rtl.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-rtl.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/prettyPhoto.css",
                      "~/Content/animate.css",
                      "~/Content/main.css",
                      "~/Content/responsive.css"));

            bundles.Add(new StyleBundle("~/AdminContent/css").Include(
                "~/Content/bootstrap-rtl.css",
                "~/Content/jquery-ui/jquery-ui-1.10.1.custom.css",
                "~/Content/bootstrap-reset.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/bootstrap-fileupload/bootstrap-fileupload.css",
                "~/Scripts/js/data-tables/DT_bootstrap.css",
                "~/Content/style.css",
                "~/Content/style-responsive.css"));
        }
    }
}
