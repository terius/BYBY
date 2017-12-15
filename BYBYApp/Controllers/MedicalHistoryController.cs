using BYBY.Cache;
using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBYApp.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    //[Authorize]
    public class MedicalHistoryController : BaseController
    {
        readonly IMedicalHistoryService _medicalHistoryService;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }
        // GET: MedicalHistory
        public async Task<ActionResult> Index()
        {
            var model = new MedicalHistoryListModel();
            model.HospitalList = await GetCacheAsync(CacheKeys.Hospital);
            return View(model);
        }

        public async Task<JsonResult> PageQuery(MedicalHistoryListSearchRequest request)
        {
            var pageData = await _medicalHistoryService.GetMedicalHistoryList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddNew()
        {
            MedicalHistoryAddModel model = new MedicalHistoryAddModel();
            model.CardTypeList = await GetCacheAsync(CacheKeys.CardType);
            model.MarriageList = await GetCacheAsync(CacheKeys.Marriage);
            model.EducationList = await GetCacheAsync(CacheKeys.Education);
            model.NationList = await GetCacheAsync(CacheKeys.Nation, true);
            model.JobList = await GetCacheAsync(CacheKeys.Job);
            model.EthnicList = await GetCacheAsync(CacheKeys.Ethnic, true);
            model.AddModel.FemaleNation = model.AddModel.MaleNation = DefaultChinaId;
            model.AddModel.FemaleEthnic = model.AddModel.MaleEthnic = DefaultEthnicId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveAdd(MedicalHistoryAddRequest request)
        {
            var response = await _medicalHistoryService.SaveAdd(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var model = await _medicalHistoryService.GetDetailModel(id);
            model.CardTypeList = await GetCacheAsync(CacheKeys.CardType);
            model.MarriageList = await GetCacheAsync(CacheKeys.Marriage);
            model.EducationList = await GetCacheAsync(CacheKeys.Education);
            model.NationList = await GetCacheAsync(CacheKeys.Nation);
            model.JobList = await GetCacheAsync(CacheKeys.Job);
            model.EthnicList = await GetCacheAsync(CacheKeys.Ethnic);
          
          //  model.FemaleMedicalDetails = await _medicalHistoryService.GetMedicalDetails(model.female)
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveEditBaseInfo(MedicalHistoryEditRequest request)
        {
            var response = await _medicalHistoryService.SaveEditBaseInfo(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MedicalDetailUpdate(MedicalDetailRequest request)
        {
            var response = await _medicalHistoryService.SaveEditMedicalDetail(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}