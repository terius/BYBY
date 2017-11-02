using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string CreateUser(UserRegRequest request)
        {
            var res = _userService.CreateUser(request);

            return "";
        }


    }
}