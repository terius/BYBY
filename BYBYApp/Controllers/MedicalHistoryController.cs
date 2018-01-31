using BYBY.Cache;
using BYBY.Infrastructure;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBYApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    [Authorize]
    public class MedicalHistoryController : BaseController
    {
        readonly IMedicalHistoryService _medicalHistoryService;
        readonly IHospitalService _hospitalService;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService, IHospitalService hospitalService)
        {
            _medicalHistoryService = medicalHistoryService;
            _hospitalService = hospitalService;
        }

        /// <summary>
        /// 病历列表页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var model = new MedicalHistoryListModel();
            model.HospitalList = await GetCacheAsync(CacheKeys.Hospital);
            model.DoctorList = await GetCacheAsync(CacheKeys.Doctor);
            model.MasterHospitalList = await _medicalHistoryService.GetLoginUserMasterHospitalList();
            model.DefaultMasterHospitalId = model.MasterHospitalList.Count > 0 ? model.MasterHospitalList[0].id : "0";


            return View(model);
        }

        public async Task<JsonResult> GetPlanList(OnlyHasIdRequest request)
        {
            var response = await _hospitalService.GetPlansByHospitalId(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   查询病历信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<JsonResult> PageQuery(MedicalHistoryListSearchRequest request)
        {
            var pageData = await _medicalHistoryService.GetMedicalHistoryList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新建患者页面
        /// </summary>
        /// <returns></returns>
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
            model.AddModel.FemaleCardType = model.AddModel.MaleCardType = (CardType)(Convert.ToInt32(model.CardTypeList.First(d => d.text == "身份证").id));
            model.AddModel.FemaleMarriage = model.AddModel.MaleMarriage = MaritalStatus.YiHun;
            return View(model);
        }

        /// <summary>
        /// 保存新建患者信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveAdd(MedicalHistoryAddRequest request)
        {
            var response = await _medicalHistoryService.SaveAdd(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 病历详细信息页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Detail(int id, int? tab)
        {
            ViewBag.TabId = tab;
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


        /// <summary>
        /// 编辑病历基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveEditBaseInfo(MedicalHistoryEditRequest request)
        {
            var response = await _medicalHistoryService.SaveEditBaseInfo(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑女方病历或男方病历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MedicalDetailUpdate(MedicalDetailRequest request)
        {
            var response = await _medicalHistoryService.SaveEditMedicalDetail(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增女方病历或男方病历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MedicalDetailAdd(MedicalDetailAddRequest request)
        {
            var response = await _medicalHistoryService.SaveAddMedicalDetail(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 新建会诊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveConsultationAdd(ConsultationAddRequest request)
        {
            request.DoctorId = LoginDoctorId;
            request.RequestDate = DateTime.Now.Date;
            request.STime = DateTime.Now;
            request.ETime = DateTime.Now;
            var response = await _medicalHistoryService.SaveConsultationAdd(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  新建转诊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveReferralAdd(ReferralAddRequest request)
        {
            request.DoctorId = LoginDoctorId;
            var response = await _medicalHistoryService.SaveReferralAdd(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 会诊取消
        /// </summary>
        /// <param name="cancelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateConsultationToCancel(ConsultationCancelRequest cancelRequest)
        {
            var response = await _medicalHistoryService.UpdateConsultationToCancel(cancelRequest);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 转诊取消
        /// </summary>
        /// <param name="cancelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateReferralToCancel(ReferralCancelRequest cancelRequest)
        {
            var response = await _medicalHistoryService.UpdateReferralToCancel(cancelRequest);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 删除总病历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(MedicalHistoryDeleteRequest request)
        {
            var response = await _medicalHistoryService.Delete(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(int patientId)
        {
            string msg = "";
            var newFilePaths = UploadFile("MedicalHistoryFile", out msg);
            if (msg != "")
            {
                return await Task.FromResult(ErrorJson(msg));
            }
            var response = await _medicalHistoryService.SaveMHImage(newFilePaths, patientId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteImage(MedicalHistoryImageDeleteRequest request)
        {
            var response = await _medicalHistoryService.DeleteImage(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}