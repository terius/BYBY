using BYBY.Services.Interfaces;
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
            if (LoginUserRoleType == BYBY.Infrastructure.RoleType.doctor)
            {
                model.HospitalList = await _doctorService.GetDoctorChildHospitals();
            }
            else  if (LoginUserRoleType == BYBY.Infrastructure.RoleType.customerservice)
            {
                model.HospitalList = await _doctorService.GetGroupHospitals();
            }
            return View(model);
        }

        public async Task<JsonResult> PageQuery(ConsultationQueryRequest request)
        {
            var pageData = await _medicalHistoryService.GetConsultationList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }
    }
}