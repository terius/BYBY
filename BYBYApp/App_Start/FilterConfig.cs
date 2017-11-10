using BYBYApp.Filter;
using System.Web;
using System.Web.Mvc;

namespace BYBYApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
          //  filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomHandleError { View = "ErrorPage" }, 2);
        }
    }
}
