using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    [Authorize]
    public class TranferController : Controller
    {

        readonly IMedicalHistoryService _medicalHistoryService;

        public TranferController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }

        // GET: Tranfer
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> PageQuery(ReferralQueryRequest request)
        {
            var pageData = await _medicalHistoryService.GetReferralList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }
    }
}