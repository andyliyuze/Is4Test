using System;
using System.Collections.Generic;

namespace Is4.Service.Shared
{
    public class PaginatedList<T> where T : class
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public IList<T> Data { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        //总页面数
        public int TotalPages { get; set; }

        public int Total { get; set; }

        public PaginatedList(IList<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;

            TotalPages = (int)Math.Ceiling(count / (decimal)pageSize);

            Total = count;

            this.Data = items;
        }

        /// <summary>
        /// 判断是否有上一页
        /// </summary>
        public bool HasPreViousPage => (PageIndex > 1);

        /// <summary>
        /// 判断是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
