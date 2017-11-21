using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BYBY.Services.Implementations
{
    public abstract class BaseService : IBaseService
    {


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


    }


}
