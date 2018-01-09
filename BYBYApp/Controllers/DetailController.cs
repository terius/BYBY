using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class DetailController : BaseController
    {
        // GET: Detail
        public ActionResult Index()
        {
            ViewBag.LastUrl = GetLastUrl();
            return View();
        }
    }
}