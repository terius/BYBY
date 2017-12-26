using BYBY.Infrastructure;
using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Views;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class SystemController : BaseController
    {
        readonly ISystemService _service;

        public SystemController(ISystemService service)
        {
            _service = service;
        }
        // GET: System
        public ActionResult MedicineDic()
        {
            return View();
        }

        public async Task<JsonResult> PageQueryMedicine(MedicineQueryRequest request)
        {
            var pageData = await _service.GetMedicineList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMedicine(MedicineAddRequest request)
        {
            var response = await _service.AddMedicine(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMedicine(MedicineEditRequest request)
        {
            var response = await _service.EditMedicine(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMedicine(OnlyHasIdRequest request)
        {
            var response = await _service.DeleteMedicine(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CheckAssay()
        {
            CheckListModel model = new CheckListModel();
            model.CheckModeList = CreateEnumList(typeof(CheckMode));
            model.AssayTypeList = CreateEnumList(typeof(AssayType));
            return View(model);
        }



        public async Task<JsonResult> PageQueryCheck(CheckQueryRequest request)
        {
            var pageData = await _service.GetCheckList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCheck(CheckListView request)
        {
            var response = await _service.AddCheck(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCheck(CheckListView request)
        {
            var response = await _service.EditCheck(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCheck(OnlyHasIdRequest request)
        {
            var response = await _service.DeleteCheck(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}