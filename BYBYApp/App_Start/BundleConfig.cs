﻿using System.Web;
using System.Web.Optimization;

namespace BYBYApp
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            //select2
            bundles.Add(new StyleBundle("~/bundles/css/select2")
                .Include("~/Metronic/assets/global/plugins/select2/css/select2.css", new CssRewriteUrlTransform())
                .Include("~/Metronic/assets/global/plugins/select2/css/select2-bootstrap.min.css", new CssRewriteUrlTransform())
              );
            bundles.Add(new ScriptBundle("~/bundles/js/select2").Include(
                      "~/Metronic/assets/global/plugins/select2/js/select2.full.js",
                      "~/Metronic/assets/global/plugins/select2/js/i18n/zh-CN.js"));



            //datepicker
            bundles.Add(
           new StyleBundle("~/bundles/css/datepicker")
               .Include("~/Metronic/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css", new CssRewriteUrlTransform())
               );
            bundles.Add(
             new ScriptBundle("~/bundles/js/datepicker")
                 .Include(
                 "~/Metronic/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                 "~/Metronic/assets/global/plugins/bootstrap-datepicker/locales/bootstrap-datepicker.zh-CN.min.js"
                 ));

            //FormValidate
            bundles.Add(
           new ScriptBundle("~/bundles/js/jqueryvalidate")
               .Include(
               "~/Scripts/jquery.validate.js",
               "~/Scripts/validate-ex.js"
               ));

            //jquery.form.js
            bundles.Add(new ScriptBundle("~/bundles/js/jqueryform")
              .Include(
              "~/Scripts/jquery.form.min.js"
              ));


           
            //bootstrap select
            bundles.Add(new StyleBundle("~/bundles/css/bootstrapselect")
                .Include("~/Metronic/assets/global/plugins/bootstrap-select/css/bootstrap-select.min.css", new CssRewriteUrlTransform())
              );
            bundles.Add(new ScriptBundle("~/bundles/js/bootstrapselect").Include(
                      "~/Metronic/assets/global/plugins/bootstrap-select/js/bootstrap-select.min.js",
                      "~/Metronic/assets/global/plugins/bootstrap-select/js/i18n/defaults-zh_CN.min.js"));

            //input mask
            bundles.Add(new ScriptBundle("~/bundles/js/inputmask").Include(
                    "~/Scripts/inputmask.js"));


            //viewer js
            bundles.Add(new StyleBundle("~/bundles/css/viewerjs")
                .Include("~/Scripts/viewer.min.css", new CssRewriteUrlTransform())
              );
            bundles.Add(new ScriptBundle("~/bundles/js/viewerjs").Include(
                      "~/Scripts/viewer.min.js"));


            //datetimepicker
            bundles.Add(
           new StyleBundle("~/bundles/css/datetimepicker")
               .Include("~/Metronic/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css", new CssRewriteUrlTransform())
               );
            bundles.Add(
             new ScriptBundle("~/bundles/js/datetimepicker")
                 .Include(
                 "~/Metronic/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js",
                 "~/Metronic/assets/global/plugins/bootstrap-datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"
                 ));


            //icheck
            bundles.Add(
       new StyleBundle("~/bundles/css/icheck")
           .Include("~/Metronic/assets/global/plugins/icheck/skins/all.css", new CssRewriteUrlTransform())
           );
            bundles.Add(
             new ScriptBundle("~/bundles/js/icheck")
                 .Include(
                 "~/Metronic/assets/global/plugins/icheck/icheck.min.js"
                 ));

            //jquery file Upload
            bundles.Add(
    new StyleBundle("~/bundles/css/jqfileupload")
        .Include("~/Metronic/assets/global/plugins/jquery-file-upload/css/jquery.fileupload.css", new CssRewriteUrlTransform())
        );
        }
    }
}
