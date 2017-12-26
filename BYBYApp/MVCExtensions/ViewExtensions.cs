using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using BYBY.Infrastructure;
using BYBY.Services.Account;
using Microsoft.AspNet.Identity;

namespace BYBYApp
{
    public static class ViewExtensions
    {
        static readonly UserManager _userManager = UserFactory.GetUserManager();
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

        public static bool IsMasterDoctor(this HtmlHelper helper)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = _userManager.FindByIdAsync(userId).Result;
            return user.IsMasterDoctor;
        }

        public static RoleType GetRoleType(this HtmlHelper helper)
        {
           // var roles = Roles.GetRolesForUser();
            var roleName = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).First();
           // var roleName = roles[0];
            var roleType = RoleType.doctor;
            switch (roleName)
            {
                case "patient":
                    roleType = RoleType.patient;
                    break;
                case "doctor":
                    roleType = RoleType.doctor;
                    break;
                case "customerservice":
                    roleType = RoleType.customerservice;
                    break;
                case "admin":
                    roleType = RoleType.admin;
                    break;
                default:
                    break;
            }
            return roleType;
        }


        public static string ToBackPage(this HtmlHelper helper)
        {
            if (HttpContext.Current.Session["DefaultUrl"] != null)
            {
                return HttpContext.Current.Session["DefaultUrl"] as string;
            }
            return "history.go(-1)";
        }
    }
}