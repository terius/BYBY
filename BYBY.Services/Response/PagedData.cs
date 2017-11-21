using BYBY.Infrastructure;
using PagedList;
using System.Collections.Generic;

namespace BYBY.Services.Response
{
    public class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }

        public IPagedList PageInfo { get; set; }

        //   public IList<int> PageNumList { get; set; }

        public int SortIndex { get; set; }
        public SortType SortType { get; set; }
    }

 
  
}
