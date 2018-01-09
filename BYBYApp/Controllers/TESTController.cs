using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class TESTController : BaseController
    {
        readonly IUserAccountService _userService;

        public TESTController(IUserAccountService userService)
        {
            _userService = userService;
        }
        // GET: TEST
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateRequest request)
        {
            var res = await _userService.CreateUserAsync(request);

            return Json(res, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> UploadFile()
        {
            string msg = "";
            var newFilePaths = UploadFile("TestFile", out msg);
            if (msg != "")
            {
                return await Task.FromResult(ErrorJson(msg));
            }
            return Json(EmptyResponse.CreateSuccess("上传成功"), JsonRequestBehavior.AllowGet);
        }



    }
}