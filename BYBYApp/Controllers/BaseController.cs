using BYBY.Cache;
using BYBY.Cache.CacheStorage;
using BYBY.Infrastructure;
using BYBY.Repository.Entities;
using BYBY.Services.Account;
using BYBY.Services.Response;
using BYBYApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace BYBYApp.Controllers
{
    public class BaseController : Controller
    {
        readonly ICacheService _cacheService = CacheManager.GetCacheService();
        readonly RoleManager _roleManager = RoleFactory.GetRoleManager();
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

        //public IList<SelectItem> CreateEnumList(Type type, bool ValueIsInt = true)
        //{
        //    IList<SelectItem> list = new List<SelectItem>();
        //    string strName, strVaule, strTitle;
        //    foreach (var myCode in Enum.GetValues(type))
        //    {

        //        FieldInfo field = type.GetField(myCode.ToString());
        //        DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //        strName = attributes[0].Description;
        //        strVaule = ValueIsInt ? ((int)myCode).ToString() : myCode.ToString();//获取值  
        //        strTitle = ValueIsInt ? myCode.ToString() : ((int)myCode).ToString();
        //        SelectItem myLi = new SelectItem { id = strVaule, text = strName, title = strTitle };
        //        list.Add(myLi);
        //    }
        //    return list;
        //}

        public int DefaultChinaId { get; set; }
        public int DefaultEthnicId { get; set; }

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
                case CacheKeys.Ethnic:
                    if (GetDefaultId)
                    {
                        DefaultEthnicId = Convert.ToInt32(cacheData.FirstOrDefault(d => d.text == "汉族").id);
                    }
                    break;
                default:
                    break;
            }
            return cacheData;
        }

        public void SaveRoleModuleToSession(TBUser user, string roleName)
        {
            var modules = user.GetModules(roleName);
            Session["RoleModule"] = modules;
        }

        public string RoleName
        {
            get
            {
                //RolePrincipal r = (RolePrincipal)User;
                //var rolesArray = r.GetRoles();
                //return rolesArray[0];
                return Request.Cookies["AccountCookies"].Values["rolename"];
            }
        }

        public async Task<IList<TBModule>> GetRoleModules()
        {
            var modules = Session["RoleModule"] as IList<TBModule>;
            if (modules == null)
            {
                var role = await _roleManager.FindByNameAsync(RoleName);
                modules = role.RoleModules.Select(d => d.Module).OrderBy(d => d.OrderBy).ToList();
            }
            return modules;
        }

        public async Task<IList<TBModule>> GetModulesForMenu()
        {
            var list = await GetRoleModules();
            return list.Where(d => d.IsMenu == true).ToList();
        }


    }
}