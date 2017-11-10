using BYBY.Services.Response;
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
    }
}