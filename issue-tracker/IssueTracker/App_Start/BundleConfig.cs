﻿using System.Web.Optimization;

namespace IssueTracker
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/reordering-tr.css",
                      "~/Content/site.css",
                      "~/Content/normalize.css",
                      "~/Content/component.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/fa.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/classie.js",
                        "~/Scripts/gnmenu.js",
                        "~/Scripts/bootstrap-toolkit.js",
                        "~/Scripts/js/hammer.min.js",
                        "~/Scripts/language-menu.js"));

            // Pick colour
            bundles.Add(new ScriptBundle("~/bundles/pick-colour").Include(
                        "~/Scripts/pick-colour.js"));

            // Reordering
            bundles.Add(new ScriptBundle("~/bundles/reordering").Include(
                        "~/Scripts/jquery.dataTables.rowReordering.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-browser").Include(
                        "~/Scripts/jquery.browser.min.js"));

            // Rich text editor
            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                        "~/Scripts/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor-replace").Include(
                        "~/Scripts/ckeditor/replace.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor-replace-desc").Include(
                        "~/Scripts/ckeditor/replace-desc.js"));

            // State workflow visualizer
            bundles.Add(new ScriptBundle("~/bundles/gojs").Include(
                        "~/Scripts/go.js"));
            bundles.Add(new ScriptBundle("~/bundles/gojs-model").Include(
                        "~/Scripts/go-model.js"));

            // Dashboard Charts
            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
                "~/Scripts/dashboard-charts.js"));

            // Issues js functions
            bundles.Add(new ScriptBundle("~/bundles/issues-js").Include(
                        "~/Scripts/issues.js"));

            // Autocomplete
            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include(
                        "~/Scripts/autocomplete.js"));

        }
    }
}
