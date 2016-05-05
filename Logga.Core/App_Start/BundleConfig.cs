using System.Web;
using System.Web.Optimization;

namespace ErrorLog.Core
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Views/Content/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Views/Content/Scripts/bootstrap.js",
                      "~/Views/Content/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Views/Content/bootstrap.css",
                      "~/Views/Content/site.css"));
        }
    }
}
