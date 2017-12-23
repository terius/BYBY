﻿using BYBY.Cache;
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
using System.Web;
using BYBY.Infrastructure.Helpers;
using BYBY.Infrastructure.Loger;

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

        public JsonResult SuccessJson(string successMessage)
        {
            EmptyResponse res = new EmptyResponse();
            res.Result = true;
            res.ErrorMessage = successMessage;
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

        public RoleType LoginUserRoleType
        {
            get {
                RoleType roleType = RoleType.doctor;
                var roleName = RoleName;
                switch (roleName)
                {
                    case "patient":
                        roleType = RoleType.patient;
                        break;
                    case "doctor":
                        roleType = RoleType.doctor;
                        break;
                    case "customerservice":
                        roleType = RoleType.customerservice;
                        break;
                    case "admin":
                        roleType = RoleType.admin;
                        break;
                    default:
                        break;
                }
                return roleType;
            }
        }

        public string RoleName
        {
            get
            {
                //RolePrincipal r = (RolePrincipal)User;
                //var rolesArray = r.GetRoles();
                //return rolesArray[0];
                if (Request.Cookies["AccountCookies"] == null)
                {
                    return "";
                }
                return Request.Cookies["AccountCookies"].Values["rolename"];
            }
        }


        public LoginUserInfo LoginUserInfo
        {
            get
            {
                var login = Session["LoginUserInfo"] as LoginUserInfo;
                if (login == null)
                {
                    throw new Exception("用户登录超时");

                }
                return login;
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

        public string GetPageId(IList<TBModule> modules)
        {
            var url = Request.Url.AbsolutePath;
            foreach (var item in modules)
            {
                if (item.Path != null && item.IsMenu)
                {
                    if (item.Path.Equals(url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Session["pageid"] = item.Id;
                        break;
                    }
                }
            }
            return Session["pageid"] as string;
        }


        /// <summary>
        ///  上传图片
        /// </summary>
        /// <param name="upType"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string UploadFile(string upType, out string msg)
        {
            msg = "";
            string newFilePath = "";

            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase uploadFile = Request.Files[i];
                    var extArr = GetALLowUploadPicType();
                    string fileExt = System.IO.Path.GetExtension(uploadFile.FileName).ToLower();
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(uploadFile.FileName);
                    if (extArr.Contains(fileExt))
                    {
                        float fileSize = (float)uploadFile.ContentLength / 1024 / 1024;
                        if (FileHelper.CheckFileLength(fileSize, out msg))
                        {
                            var path = "/Upfiles/" + upType + "/";
                            var ServerPath = Server.MapPath(path);
                            fileName += "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
                            if (!System.IO.Directory.Exists(ServerPath))
                            {
                                System.IO.Directory.CreateDirectory(ServerPath);
                            }

                            uploadFile.SaveAs(ServerPath + fileName);
                            newFilePath = ServerPath + fileName;
                        }
                    }
                    else
                    {
                        msg = "上传文件类型错误";
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().Log("上传文件错误:\r\n" + ex.ToString());
                msg = ex.Message;
            }
            return newFilePath;
        }

        private string GetALLowUploadPicType()
        {
            return System.Configuration.ConfigurationManager.AppSettings["UploadFileType"];
        }


    }
}