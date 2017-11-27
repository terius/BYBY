using BYBY.Services.Response;
using BYBYApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
    }
}