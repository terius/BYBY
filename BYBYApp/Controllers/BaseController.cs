using BYBY.Cache;
using BYBY.Cache.CacheStorage;
using BYBY.Infrastructure;
using BYBY.Services.Response;
using BYBYApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Linq;

namespace BYBYApp.Controllers
{
    public class BaseController : Controller
    {
        readonly ICacheService _cacheService = CacheManager.GetCacheService();
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
            return Json(dd, JsonRequestBehavior.AllowGet);
        }

        public IList<SelectItem> CreateEnumList(Type type, bool ValueIsInt = true)
        {
            IList<SelectItem> list = new List<SelectItem>();
            string strName, strVaule, strTitle;
            foreach (var myCode in Enum.GetValues(type))
            {

                FieldInfo field = type.GetField(myCode.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                strName = attributes[0].Description;
                strVaule = ValueIsInt ? ((int)myCode).ToString() : myCode.ToString();//获取值  
                strTitle = ValueIsInt ? myCode.ToString() : ((int)myCode).ToString();
                SelectItem myLi = new SelectItem { id = strVaule, text = strName, title = strTitle };
                list.Add(myLi);
            }
            return list;
        }

        public int DefaultChinaId { get; set; }


        public async Task<IList<SelectItem>> GetCacheAsync(CacheKeys key, bool GetDefaultId = false)
        {
            IList<SelectItem> cacheData = await _cacheService.GetSelectItemAsync(key);
            switch (key)
            {
                case CacheKeys.Nation:
                    if (GetDefaultId)
                    {
                        DefaultChinaId = Convert.ToInt32(cacheData.FirstOrDefault(d => d.text == "中国").id);
                    }
                    break;
                case CacheKeys.Job:
                    break;
                default:
                    break;
            }
            return cacheData;
        }



    }
}