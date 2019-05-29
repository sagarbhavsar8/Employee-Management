using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AddToMyWebCart
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/jquery-3.3.1.js", "~/Scripts/umd/popper.js","~/Scripts/bootstrap.js","~/Scripts/bootstrap.bundle.js","~/Scripts/feather.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/datepicker").Include("~/Scripts/datepicker/moment.js","~/Scripts/datepicker/bootstrap-datepicker.js"));


            bundles.Add(new StyleBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css", "~/Content/simple-sidebar.css"));
            bundles.Add(new StyleBundle("~/Styles/site").Include("~/Content/Styles.css"));
            bundles.Add(new StyleBundle("~/Styles/datepicker").Include("~/Styles/datepicker/bootstrap-datepicker.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
