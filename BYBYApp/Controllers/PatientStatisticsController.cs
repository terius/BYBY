using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{

    public class PatientStatisticsController : BaseController
    {

        readonly IMedicalHistoryService _medicalHistoryService;

        public PatientStatisticsController(IMedicalHistoryService medicalHistoryService)
        {

            _medicalHistoryService = medicalHistoryService;
        }
        // GET: PatientStatistics
        public async Task<ActionResult> Index()
        {
            var model = new PatientStatisticsModel();
            ViewBag.IsMasterDoctor = LoginUserInfo.IsMasterDoctor;
            if (ViewBag.IsMasterDoctor == true)
            {
                model.ChildHospitalList = await GetLoginUserChildHospList();
            }
            else
            {
                model.MyHospitalId = LoginUserInfo.HospitalId;
            }
         
            return View(model);
        }

        public async Task<JsonResult> QueryReport(ReportQueryRequest request)
        {
            var response = await _medicalHistoryService.GetReport(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> QueryList(ReportQueryRequest request)
        {
            PagedData<ReportListView> response;
            if (request.Type == 1)
            {
                response = await _medicalHistoryService.GetConsultationListInReport(request);
            }
            else
            {
                response = await _medicalHistoryService.GetReferralListInReport(request);
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}