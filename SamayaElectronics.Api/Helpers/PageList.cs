using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronics.Api.Helpers
{
    public class PageList<T>:List<T>
    {
        public float TotalCount { get; set; }

        public float TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public PageList(List<T> data, int pageNum, float pageSize, float count)
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
