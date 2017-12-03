using BYBY.Repository.Entities;
using BYBY.Services.Account;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public async Task<TBUser> GetLoginInfo()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

    }


}
