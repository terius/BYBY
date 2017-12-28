using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Views;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class HospitalController : BaseController
    {

        readonly IHospitalService _service;

        public HospitalController(IHospitalService service)
        {
            _service = service;
        }

        // GET: Hospital
        public async Task<ActionResult> ConsultationRoom()
        {
            var model = await _service.GetRoomList();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRoom(AddRoomRequest  request)
        {
            var response = await _service.AddRoom(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRoom(ConsultationRoomListView request)
        {
            var response = await _service.EditRoom(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRoom(OnlyHasIdRequest request)
        {
            var response = await _service.DeleteRoom(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> DateSetup()
        {
            var model = await _service.GetDateSetupList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDateSetup(AddDateSetupRequest request)
        {
            var response = await _service.AddDateSetup(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDateSetup(OnlyHasIdRequest request)
        {
            var response = await _service.DeleteDateSetup(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Plan()
        {
            PlanListModel model = new PlanListModel();
            model.DoctorList = await GetCacheAsync(BYBY.Cache.CacheKeys.Doctor);
            return View(model);
        }

        public async Task<JsonResult> QueryPlan(PlanQueryRequest request)
        {
            var response = await _service.GetPlanList(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}