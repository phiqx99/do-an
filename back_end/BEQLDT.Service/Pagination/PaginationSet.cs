using System.Collections.Generic;

namespace BEQLDT.Service.Model
{
    public class PaginationSet<T> where T : class
    {
        public int PageNo { set; get; }
        public int PageSize { set; get; }
        public int Total { set; get; }
        public int TotalCount { set; get; }
        public int MaxPage { set; get; }
        public IEnumerable<T> Items { set; get; }
    }
}
