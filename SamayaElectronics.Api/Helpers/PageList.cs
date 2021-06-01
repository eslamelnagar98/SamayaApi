using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronics.Api.Helpers
{
    public class PageList<T>:List<T>
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalCount;

        public PageList(List<T> data, int pageNum, int pageSize, int count)
        {
            TotalCount = count;
            TotalPages = count / pageSize;
            CurrentPage = pageNum;
            AddRange(data);
        }

        public static PageList<T> CreateInstance(IEnumerable<T> data, int pageNum, int pageSize)
        {
            var paginatedData = data.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(paginatedData, pageNum, pageSize, data.Count());
        }
    }
}
