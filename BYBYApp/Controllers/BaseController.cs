using BYBY.Services.Response;
using BYBYApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult ErrorJson(string errorMessage)
        {
            EmptyResponse res = new EmptyResponse();
            res.Result = false;
            res.ErrorMessage = errorMessage;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateDataTablesEntity<T>(IList<T> views, int allCount) where T : class, new()
        {
            var dd = new DataTablesEntity<T>();
            dd.draw = Convert.ToInt32(HttpContext.Request["draw"]);
            dd.recordsTotal = allCount;
            dd.recordsFiltered = allCount;
            dd.data = views;
            return Json(dd,JsonRequestBehavior.AllowGet);
        }
    }
}