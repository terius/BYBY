using BYBY.Cache;
using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBYApp.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    [Authorize]
    public class ConsultationController : BaseController
    {
        readonly IMedicalHistoryService _medicalHistoryService;
        readonly IDoctorService _doctorService;

        public ConsultationController(IMedicalHistoryService medicalHistoryService,
            IDoctorService doctorService
            )
        {
            _medicalHistoryService = medicalHistoryService;
            _doctorService = doctorService;
        }

        // GET: Consultation
        public async Task<ActionResult> Index()
        {
            var model = new ConsultationListModel();
            var loginInfo = LoginUserInfo;
            if (loginInfo.RoleName == BYBY.Infrastructure.RoleType.doctor)
            {
                if (loginInfo.IsMasterDoctor)
                {
                    model.HospitalList = await _doctorService.GetDoctorChildHospitals();
                }
                else
                {

                }
            }
            else if (loginInfo.RoleName == BYBY.Infrastructure.RoleType.customerservice)
            {
                model.HospitalList = await _doctorService.GetGroupHospitals();
            }
            ViewBag.IsMasterDoctor = LoginUserInfo.IsMasterDoctor;

            model.MotherHospitalList = await GetCacheAsync(CacheKeys.MotherHospital);
            model.MotherDoctorList = await GetCacheAsync(CacheKeys.MotherDoctor);
            model.MasterHospitalId = await _medicalHistoryService.GetDoctorMasterHospitalId();
            return View(model);
        }

        public async Task<JsonResult> PageQuery(ConsultationQueryRequest request)
        {
            var pageData = await _medicalHistoryService.GetConsultationList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Detail(int id)
        {
            SetBackPage();
            var model = await _medicalHistoryService.GetConsultationDetail(id);
            model.DoctorList = await _doctorService.GetDoctorListByHospital();
            model.MedicineList = await GetCacheAsync(CacheKeys.Medicine);
            model.CheckList = await GetCacheAsync(CacheKeys.CheckAssay);
            ViewBag.IsMasterDoctor = LoginUserInfo.IsMasterDoctor;
            return await Task.FromResult(View(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveRecord(ConsultationRecordEditRequest request)
        {
            var response = await _medicalHistoryService.SaveConsultationRecord(request);

            return Json(response, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 取消会诊
        /// </summary>
        /// <param name="cancelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateConsultationStatus(UpdateConsultationStatusRequest cancelRequest)
        {
            var response = await _medicalHistoryService.UpdateConsultationStatus(cancelRequest);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddMedicine(ConsultationMedicineListRequest request)
        {
            var response = await _medicalHistoryService.AddMedicine(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> EditMedicine(ConsultationMedicineListRequest request)
        {
            var response = await _medicalHistoryService.EditMedicine(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMedicine(OnlyHasIdRequest request)
        {
            var response = await _medicalHistoryService.DeleteMedicine(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public async Task<ActionResult> AddCheck(ConsultationCheckListRequest request)
        {
            var response = await _medicalHistoryService.AddCheck(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> EditCheck(ConsultationCheckListRequest request)
        {
            var response = await _medicalHistoryService.EditCheck(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCheck(OnlyHasIdRequest request)
        {
            var response = await _medicalHistoryService.DeleteCheck(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}