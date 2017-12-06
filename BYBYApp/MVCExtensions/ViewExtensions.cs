using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BYBYApp
{
    public static class ViewExtensions
    {
        public static HtmlString ValidationSummaryBootstrap(this HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            if (htmlHelper.ViewData.ModelState.IsValid)
            {
                return new HtmlString(string.Empty);
            }

            return new HtmlString(
                "<div class=\"alert alert-danger\"><i class=\"fa fa-warning\"></i>"
                + htmlHelper.ValidationSummary()
                + "</div>");
        }

        public static IHtmlString RenderAsJson(this HtmlHelper helper, object model)
        {
            return helper.Raw(Json.Encode(model));
        }

        public static HtmlString GetUserName(this HtmlHelper helper)
        {
            var userName = HttpContext.Current.User.Identity.Name;
            return new HtmlString(userName);
        }
    }
}