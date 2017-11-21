using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBYApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
            var aaa = HttpContext.CurrentHandler;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateRequest request)
        {
            var res = await _userService.CreateUserAsync(request);

            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetTestData()
        {

            IList<MedicalHistoryListModel> models = new List<MedicalHistoryListModel>();
            for (int i = 0; i < 10; i++)
            {
                models.Add(new MedicalHistoryListModel { MaleName = "男方姓名" + i, MaleAge = 18, FeMaleName = "女方姓名" + i, FeMaleAge = 22 });
            }
            return await Task.FromResult(CreateDataTablesEntity(models, 100));
        }

    }
}