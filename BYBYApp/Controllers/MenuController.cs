using System.Threading.Tasks;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class MenuController : BaseController
    {
        [ChildActionOnly]
        public async Task<ActionResult> Index()
        {
            var modules = await GetModulesForMenu();
            ViewBag.PageId = GetPageId(modules);
           // ViewBag.LoginUserInfo = LoginUserInfo;
            return PartialView(modules);
        }
    }
}