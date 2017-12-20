using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    [Authorize]
    public class ConsultationController : Controller
    {
        readonly IMedicalHistoryService _medicalHistoryService;

        public ConsultationController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }

        // GET: Consultation
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> PageQuery(ConsultationQueryRequest request)
        {
            var pageData = await _medicalHistoryService.GetConsultationList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }
    }
}