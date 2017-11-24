using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBYApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class MedicalHistoryController : Controller
    {
        readonly IMedicalHistoryService _medicalHistoryService;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }
        // GET: MedicalHistory
        public ActionResult Index()
        {
            //IList<MedicalHistoryListModel> models = new List<MedicalHistoryListModel>();
            //for (int i = 0; i < 10; i++)
            //{
            //    models.Add(new MedicalHistoryListModel { MaleName = "男方姓名" + i, MaleAge = 18, FeMaleName = "女方姓名" + i, FeMaleAge = 22 });
            //}

            return View();
        }

        public async Task<JsonResult> PageQuery(MedicalHistoryListSearchRequest request)
        {
            var pageData = await _medicalHistoryService.GetMedicalHistoryList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNew()
        {
            return View();
        }
    }
}