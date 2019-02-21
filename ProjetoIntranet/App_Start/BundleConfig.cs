using System.Web;
using System.Web.Optimization;

namespace ProjetoIntranet
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.scrollTo.js", "~/Scripts/jquery.mask.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Extranet").Include("~/Scripts/Extranet.js"));

            

            bundles.Add(new ScriptBundle("~/bundles/Intranet").Include("~/Scripts/Intranet.js"));

            bundles.Add(new ScriptBundle("~/bundles/Loader").Include("~/Scripts/Loader.js"));

            bundles.Add(new ScriptBundle("~/bundles/NTM").Include("~/Scripts/jquery.ntm.js"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/Intra").Include("~/Content/Intra.css"));
            bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/Content/Curriculo").Include("~/Content/Curriculo.css"));
            bundles.Add(new StyleBundle("~/Content/menu").Include("~/Content/Menu.css"));
        }
    }
}
