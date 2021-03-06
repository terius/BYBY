﻿using BYBY.Cache;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBYApp.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    [Authorize]
    public class TranferController : BaseController
    {

        readonly IMedicalHistoryService _medicalHistoryService;

        public TranferController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }

        // GET: Tranfer
        public async Task<ActionResult> Index(int? status)
        {
            ReferralListModel model = new ReferralListModel();
            model.HospitalList = await GetCacheAsync(CacheKeys.Hospital);
          //  model.MasterHospitalId = await _medicalHistoryService.GetDoctorMasterHospitalId();
            ViewBag.IsMasterDoctor = LoginUserInfo.IsMasterDoctor;
            ViewBag.Status = status;
            return View(model);
        }



        public async Task<JsonResult> PageQuery(ReferralQueryRequest request)
        {
            var pageData = await _medicalHistoryService.GetReferralList(request);
            return Json(pageData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateReferralStatus(UpdateReferralStatusRequest request)
        {
            var response = await _medicalHistoryService.UpdateReferralStatus(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetDetail(OnlyHasIdRequest request)
        {
            var response = await _medicalHistoryService.GetReferralDetail(request);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


    }
}