using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace BYBYApp.Controllers
{
    public class HospitalController : BaseController
    {

        readonly IHospitalService _service;
        readonly IDoctorService _doctorService;

        public HospitalController(IHospitalService service, IDoctorService doctorService)
        {
            _service = service;
            _doctorService = doctorService;
        }

        // GET: Hospital
        public async Task<ActionResult> ConsultationRoom()
        {
            var model = new ConsultationRoomModel();
            model.RoomList = await _service.GetRoomList();
            //   model.HospitalList = await GetCacheAsync(BYBY.Cache.CacheKeys.Hospital);
            return View(model);
        }

        //public async Task<JsonResult> QueryRoomByHospital()
        //{
        //    var response = await _service.GetRoomList();
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRoom(AddRoomRequest request)
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


        #region 排班模块
        public async Task<ActionResult> Plan()
        {
            PlanListModel model = new PlanListModel();
            model.DoctorList = await GetCacheAsync(BYBY.Cache.CacheKeys.Doctor);
            var roomList = await GetCacheAsync(BYBY.Cache.CacheKeys.Room);

            model.RoomList = roomList.Where(d => d.parent == LogingUserHospitalId.ToString()).ToList();
         //   model.HospitalList = await GetCacheAsync(BYBY.Cache.CacheKeys.Hospital);
            return View(model);
        }

        public async Task<JsonResult> QueryPlan(PlanQueryRequest request)
        {
            var response = await _service.GetPlanList(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SavePlan(IList<DateSetupView> request)
        {
            var response = await _service.SavePlan(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 医生模块

        public async Task<ActionResult> Doctor()
        {
            var model = new DoctorListModel();
            model.HospitalList = await GetCacheAsync(BYBY.Cache.CacheKeys.Hospital);
            model.LockHospitalId = LogingUserHospitalId;
            return View(model);
        }


        public async Task<JsonResult> QueryDoctor(QueryDoctorRequest request)
        {
            var pageData = await _doctorService.GetDoctorList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDoctor(DoctorListView request)
        {
            var response = await _doctorService.AddDoctor(request);
            if (response.Result)
            {
                string msg = "";
                var newFilePaths = UploadFile("DoctorImage", out msg);
                if (msg == "" && newFilePaths.Count > 0)
                {
                    await _doctorService.SaveDoctorImage(response.Id, newFilePaths[0]);
                    //  return  ErrorJson(msg);
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDoctor(DoctorListView request)
        {
            var response = await _doctorService.EditDoctor(request);
            if (response.Result)
            {
                string msg = "";
                var newFilePaths = UploadFile("DoctorImage", out msg);
                if (msg == "" && newFilePaths.Count > 0)
                {
                    await _doctorService.SaveDoctorImage(response.Id, newFilePaths[0]);
                    //  return  ErrorJson(msg);
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDoctor(OnlyHasIdRequest request)
        {
            var response = await _doctorService.DeleteDoctor(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DoctorDetail(int id)
        {
            var model = await _doctorService.GetDoctorDetail(id);
            return View(model);
        }

        #endregion


    }
}