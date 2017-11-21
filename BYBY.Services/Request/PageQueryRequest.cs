using BYBY.Infrastructure;

namespace BYBY.Services.Request
{
    public class PageQueryRequest
    {

        private int _PageIndex;

        public int PageIndex
        {
            get { return _PageIndex > 0 ? _PageIndex : 1; }
            set { _PageIndex = value; }
        }

        public int DataSize { get; set; }

        private int _PageSize;

        public int PageSize
        {
            get { return _PageSize > 0 ? _PageSize : 5; }
            set { _PageSize = value; }
        }

        //public string clientMethod { get; set; }

        /// <summary>
        /// 排序字段索引
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// 排序类型:0-不排序 1-asc 2-desc
        /// </summary>
        public SortType SortType { get; set; }

    }
}
