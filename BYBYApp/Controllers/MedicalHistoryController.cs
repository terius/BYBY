using BYBY.Infrastructure;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBYApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class MedicalHistoryController : BaseController
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

        public async Task<ActionResult> AddNew()
        {
            MedicalHistoryAddModel model = new MedicalHistoryAddModel();
            model.CardTypeList = CreateEnumList(typeof(CardType));
            model.MarriageList = CreateEnumList(typeof(MaritalStatus));
            model.EducationList = CreateEnumList(typeof(Education));
            model.NationList = await GetCacheAsync(BYBY.Cache.CacheKeys.Nation,true);
            model.JobList = await GetCacheAsync(BYBY.Cache.CacheKeys.Job);
            model.AddModel.FemaleNation = DefaultChinaId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAdd(MedicalHistoryAddRequest request)
        {
            var response = new EmptyResponse();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}