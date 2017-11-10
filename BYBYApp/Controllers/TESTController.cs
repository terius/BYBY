﻿using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class TESTController : BaseController
    {
         readonly IUserAccountService _userService;

        public TESTController(IUserAccountService userService)
        {
            _userService = userService;
        }
        // GET: TEST
        public ActionResult Index()
        {
            var aaa = HttpContext.CurrentHandler;
           
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateRequest request)
        {
            var res = await _userService.CreateUserAsync(request);

            return Json(res, JsonRequestBehavior.AllowGet);
        }


    }
}