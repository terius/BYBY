using BYBY.Infrastructure.Helpers;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBYApp.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class AccountController : BaseController
    {
        readonly IUserAccountService _userAccountService;
        public AccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CreateValidCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            loginModel.Password = StringHelper.Sha256(loginModel.Password);
            if (!CheckValidCode(loginModel.ValidCode))
            {
                ModelState.AddModelError("", "验证码错误");
                return View();
            }

            var request = new UserLoginRequest { UserName = loginModel.UserName, Password = loginModel.Password, Role = BYBY.Infrastructure.RoleType.Doctor };
            var response = await _userAccountService.UserLogin(request);
            if (!response.Result)
            {
                ModelState.AddModelError("", response.ErrorMessage);
            }

            //var userinfo = _userService.GetDbQuerySet().FirstOrDefault(d => d.UserName.Equals(UserName) && d.Password.Equals(Password));
            //if (userinfo != null)
            //{
            //    GetRoles(userinfo.Id, UserName);
            //    FormsAuthentication.SetAuthCookie(userinfo.UserName, false);

            //    return Redirect("~/Home/Index");
            //}
    
            return View();
        }


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