using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
    }
}