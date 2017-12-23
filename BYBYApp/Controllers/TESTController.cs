using BYBY.Services.Interfaces;
using BYBY.Services.Request;
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


       

    }
}