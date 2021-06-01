using System;
using SamayaElectronicsRestApi.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Domain.Entities
{
    public class Project:BaseEntity
    {
        public Project()
        {
            WorksOns = new HashSet<WorksOn>();
        }
        public string ProjectName { get; set; }
        public decimal Budget { get; set; }
        public ICollection<WorksOn> WorksOns { get; set; }
    }
}
