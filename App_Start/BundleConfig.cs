using System.Web;
using System.Web.Optimization;

namespace CSS2
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Should fllow the same order. Please don't chnage the order of css/js files
            // to work properly. Applicable for all bundles.

            bundles.Add(new StyleBundle("~/LayoutPart1CSS").Include(
                "~/Content/css/Site.min.css",
                "~/Content/css/assets/css/ace-ie.min.css",
                "~/Content/css/assets/css/ace-skins.min.css",
                "~/Content/css/assets/css/ace.min.css",
                "~/Content/css/assets/css/bootstrap-datetimepicker.css",
                "~/Content/css/assets/css/bootstrap-editable.css",
                "~/Content/css/assets/css/bootstrap-timepicker.css"                           
                ));

            bundles.Add(new StyleBundle("~/LayoutPart2CSS").Include(
                "~/Content/css/assets/css/chosen.css",
                 "~/Content/css/assets/css/jquery-ui.custom.min.css",
                "~/Content/css/assets/css/jquery-ui.min.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/LayoutPart1JQuery").Include(
                        "~/Scripts/jquery-1.10.2.js",
                        "~/Scripts/modernizr-2.6.2.js",
                        "~/Scripts/jquery-ui-1.10.4.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Content/css/assets/js/jquery.inputlimiter.1.3.1.min.js",
                        "~/Content/css/assets/js/bootstrap.min.js",
                        "~/Content/css/assets/js/chosen.jquery.min.js",
                        "~/Content/css/assets/js/jquery.maskedinput.min.js",
                        "~/Scripts/dateformat.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/LayoutPart2JQuery").Include(
                "~/Content/css/assets/js/ace.min.js",
                "~/Content/css/assets/js/ace-elements.min.js",
                "~/Content/css/assets/js/ace-extra.min.js",
                "~/Content/css/assets/js/ace/elements.scroller.js",
                "~/Content/css/assets/js/ace/ace.sidebar-scroll-1.js",
                "~/Content/css/assets/js/ace/ace.sidebar-scroll-2.js",
                "~/Content/css/assets/js/ace/ace.onpage-help.js",
                "~/Scripts/jquery.tmpl.min.js",
                "~/Scripts/Validations.js",
                "~/Scripts/popup.min.js",
                "~/Scripts/MasterData.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/_DirectorsChosenJQuery").Include(
                "~/Areas/WO/Content/Scripts/_DirectorsChosen.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/_DynamicGridJQuery").Include(
               "~/Areas/WO/Content/Scripts/_DynamicGrid.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/_Css1InfoJQuery").Include(
              "~/Areas/WO/Content/Scripts/_Css1Info.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/_Css1ReviewJQuery").Include(
              "~/Areas/WO/Content/Scripts/_Css1Review.js"
              ));

            BundleTable.EnableOptimizations = true;


        }
    }
}