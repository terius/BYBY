using BYBY.Cache;
using BYBY.Cache.CacheStorage;
using BYBY.Infrastructure;
using BYBY.Infrastructure.Helpers;
using BYBY.Infrastructure.Loger;
using BYBY.Repository.Entities;
using BYBY.Services;
using BYBY.Services.Account;
using BYBY.Services.Response;
using BYBY.Services.Views;
using BYBYApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BYBYApp.Controllers
{
    public class BaseController : Controller
    {
        readonly ICacheService _cacheService = CacheManager.GetCacheService();
        readonly RoleManager _roleManager = RoleFactory.GetRoleManager();
        readonly UserManager _userManager = UserFactory.GetUserManager();
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
            res.SuccessMessage = successMessage;
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
            if (user.IsMasterDoctor)
            {
                modules = modules.Where(d => d.Name != "MedicalHistory").ToList();
            }
            Session["RoleModule"] = modules;
        }

        public RoleType LoginUserRoleType
        {
            get
            {
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
                return GetCookieValue("rolename");
            }
        }

        public string UserName
        {
            get
            {
                return GetCookieValue("username");
            }
        }

        public string TrueName
        {
            get
            {
                return GetCookieValue("truename");
            }
        }

        public bool IsMasterDoctor
        {
            get
            {
                return LoginUserInfo.IsMasterDoctor;
            }
        }

        public int LogingUserHospitalId
        {
            get
            {
                return LoginUserInfo.HospitalId;
            }
        }

        private string GetCookieValue(string name)
        {
            if (Request.Cookies["AccountCookies"] == null)
            {
                throw new Exception("登录超时，请重新登录");
            }
            return Request.Cookies["AccountCookies"].Values[name];
        }


        public LoginUserInfo LoginUserInfo
        {
            get
            {
                var login = Session["LoginUserInfo"] as LoginUserInfo;
                if (login == null)
                {
                    var user = GetLoginInfoAsync().Result;
                    login = user.ConvertToLoginUserInfo();
                    Session["LoginUserInfo"] = login;

                }
                return login;
            }
        }





        public async Task<TBUser> GetLoginInfoAsync()
        {
            //    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var userId = GetCookieValue("userid");
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<IList<TBModule>> GetRoleModules()
        {
            var modules = Session["RoleModule"] as IList<TBModule>;
            if (modules == null)
            {
                var role = await _roleManager.FindByNameAsync(RoleName);
                if (IsMasterDoctor)
                {
                    modules = role.RoleModules.Select(d => d.Module).Where(d => d.Name != "MedicalHistory").OrderBy(d => d.OrderBy).ToList();
                }
                else
                {
                    modules = role.RoleModules.Select(d => d.Module).OrderBy(d => d.OrderBy).ToList();
                }
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
        public IList<string> UploadFile(string upType, out string msg)
        {
            msg = "";
            IList<string> newFilePaths = new List<string>();

            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase uploadFile = Request.Files[i];
                    if (string.IsNullOrWhiteSpace(uploadFile.FileName))
                    {
                        throw new FileNotFoundException("上传图片为空");
                    }
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
                            if (!Directory.Exists(ServerPath))
                            {
                                Directory.CreateDirectory(ServerPath);
                            }

                            uploadFile.SaveAs(ServerPath + fileName);
                            newFilePaths.Add(Path.Combine(path + fileName));
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
            return newFilePaths;
        }

        private string GetALLowUploadPicType()
        {
            return System.Configuration.ConfigurationManager.AppSettings["UploadFileType"];
        }

        public void SetBackPage()
        {
            Session["DefaultUrl"] = Request.Url.AbsoluteUri;
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


    }
}