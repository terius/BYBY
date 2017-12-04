using BYBY.Infrastructure.Helpers;
using BYBY.Infrastructure.Loger;
using BYBY.Services.Account;
using BYBY.Services.Interfaces;
using BYBY.Services.Response;
using BYBYApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        readonly UserManager _userManager = UserFactory.GetUserManager();
        readonly IUserAccountService _userAccountService;
        public AccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        // GET: Account

        [AllowAnonymous]
        public ActionResult Login()
        {
            ClearSession();
            return View();
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CreateValidCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            try
            {


                //if (!CheckValidCode(loginModel.ValidCode))
                //{
                //    return ErrorJson("验证码错误");
                //}

                // 1. 利用ASP.NET Identity获取用户对象
                var user = await _userManager.FindAsync(loginModel.UserName, loginModel.Password);
                if (user == null)
                {
                    return ErrorJson("用户名或密码错误");
                }

              //  _userManager.get
                // 2. 利用ASP.NET Identity获取identity 对象
                var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                // 3. 将上面拿到的identity对象登录

                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                //var request = new UserLoginRequest { UserName = loginModel.UserName, Password = loginModel.Password, RoleId = loginModel.RoleId };
                //var response = await _userAccountService.UserLogin(request);
                //if (response.Result)
                //{
                //    FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                //}
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().Log("错误信息:\r\n" + ex.ToString());
                return ErrorJson(ex.Message);
            }
            return Json(EmptyResponse.CreateSuccess(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LogOut()
        {

            return RedirectToAction("Login");
        }

        private void ClearSession()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                if (cookie != "__RequestVerificationToken")
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }

            }
        }



      

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool CheckValidCode(string code)
        {
            if (Session["ValidateCode"] == null)
            {
                return false;
            }
            var vcode = Session["ValidateCode"] as string;
            return code == vcode;
        }
    }
}