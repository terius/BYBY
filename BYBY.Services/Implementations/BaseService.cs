using BYBY.Infrastructure;
using BYBY.Infrastructure.Helpers;
using BYBY.Repository.Entities;
using BYBY.Services.Account;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace BYBY.Services.Implementations
{
    public abstract class BaseService : IBaseService
    {

        readonly UserManager _userManager = UserFactory.GetUserManager();
        protected virtual PagedData<V> PageQuery<T, V>(IQueryable<T> query, PageQueryRequest request, Func<IEnumerable<T>, IEnumerable<V>> func)
        where V : class
        where T : class, new()
        {

            var pageListData = query.ToPagedList(request.PageIndex, request.PageSize);
            var newPageView = new PagedData<V>
            {
                Data = func(pageListData),
                SortIndex = request.SortIndex,
                SortType = request.SortType,
                PageInfo = pageListData.GetMetaData()
            };
            switch (request.SortType)
            {
                case Infrastructure.SortType.None:

                    break;
                case Infrastructure.SortType.Normal:
                    break;
                case Infrastructure.SortType.Asc:
                    break;
                case Infrastructure.SortType.Desc:
                    break;
                default:
                    break;
            }
            return newPageView;

        }

        protected virtual PagedData<T> PageQuery<T>(IQueryable<T> query, PageQueryRequest request) where T : class, new()
        {
            var pageListData = query.ToPagedList(request.PageIndex, request.PageSize);
            var newPageView = new PagedData<T>
            {
                Data = pageListData,
                SortIndex = request.SortIndex,
                SortType = request.SortType,
                PageInfo = pageListData.GetMetaData()
            };
            return newPageView;

        }


        public async Task<TBUser> GetLoginInfoAsync()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        //public TBUser GetLoginInfo()
        //{
        //    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //    var user =  _userManager.FindById(userId);
        //    return user;
        //}

        public async Task<TBDoctor> GetLoginDoctorInfo()
        {
            var user = await GetLoginInfoAsync();
            return user.Doctors.First();
        }


        public async Task<IList<SelectItem>> GetLoginUserMasterHospitalList()
        {
            var userInfo = await GetLoginInfoAsync();
            return userInfo.MasterHospitalIds.ConvertTo_SelectItem();
        }

        //public async Task<int> GetLoginUserHospitalId()
        //{
        //    var userInfo = await GetLoginInfoAsync();
        //    return userInfo.ho;
        //}

        public LoginUserInfo LoginUserInfo
        {
            get
            {
                var login = HttpContext.Current.Session["LoginUserInfo"] as LoginUserInfo;
                if (login == null)
                {
                    var user = GetLoginInfoAsync().Result;
                    login = user.ConvertToLoginUserInfo();
                    HttpContext.Current.Session["LoginUserInfo"] = login;

                }
                return login;
            }
        }

        public int LoginUserHospitalId
        {
            get
            {
                return LoginUserInfo.HospitalId;
            }
        }

        /// <summary>
        /// 是否为母院医生
        /// </summary>
        public bool IsMasterDoctor
        {
            get
            {
                var userInfo = GetLoginInfoAsync().Result;
                return userInfo.IsMasterDoctor;
            }
        }

        public string GetLoginUserName()
        {
            return System.Web.HttpContext.Current.User.Identity.GetUserName();
        }

        public IList<DisplayModel> GetDisplayView<T>(T t) where T : class, new()
        {
            IList<DisplayModel> list = new List<DisplayModel>();
            ViewAttribute vi;
            string text;
            foreach (PropertyInfo item in t.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                vi = item.GetAttribute<ViewAttribute>();
                if (vi != null)
                {
                    text = vi.DisplayName;
                    list.Add(new DisplayModel { Text = text, Value = StringHelper.SafeGetStringFromObj(item.GetValue(t, null)) });
                }
            }
            return list;
        }


    }


}
