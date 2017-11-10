using BYBY.Infrastructure.Helpers;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBYApp.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

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
            if (!CheckValidCode(loginModel.ValidCode))
            {
                return ErrorJson("验证码错误");
            }
            var request = new UserLoginRequest { UserName = loginModel.UserName, Password = loginModel.Password, RoleId = loginModel.RoleId };
            var response = await _userAccountService.UserLogin(request);
            if (response.Result)
            {
                FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

            }
            return Json(response, JsonRequestBehavior.AllowGet);
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