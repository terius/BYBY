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

        public IList<ListItem> CreateEnumList(Type type, bool ValueIsInt = true)
        {
            IList<ListItem> list = new List<ListItem>();
            foreach (var myCode in Enum.GetValues(type))
            {

                FieldInfo field = type.GetField(myCode.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string strName = attributes[0].Description;
                string strVaule = ValueIsInt ? ((int)myCode).ToString() : myCode.ToString();//获取值                 
                ListItem myLi = new ListItem(strName, strVaule);
                myLi.Attributes.Add("title", (ValueIsInt ? myCode.ToString() : ((int)myCode).ToString()));
                list.Add(myLi);
            }
            return list;
        }
    }
}