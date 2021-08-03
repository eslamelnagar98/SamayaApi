using SamayaElectronicsRestApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronics.Api.DTO
{
    public class PageDto<T>
    {
        public IEnumerable<T> Data { get; set; }
        public float TotalCount { get; set; }

        public float TotalPages { get; set; }

        public float CurrentPage { get; set; }

        public string PreviousLink { get; set; }

        public string NextLink { get; set; }
    }
}
