﻿using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class MainController : BaseController
    {

        readonly IMedicalHistoryService _medicalHistoryService;

        public MainController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }
        // GET: Main
        public async Task<ActionResult> Index()
        {
            MainModel model = await _medicalHistoryService.GetMainModel();
            var loginUser = LoginUserInfo;
            model.RoleType = LoginUserInfo.RoleName;

            if (model.RoleType == BYBY.Infrastructure.RoleType.doctor)
            {
                model.RoomList = await GetCacheAsync(BYBY.Cache.CacheKeys.Room);
                model.DoctorId = LoginUserInfo.DoctorId;
                model.IsChildDoctor = LoginUserInfo.IsChildDoctor;
            }
            return View(model);
        }
    }
}