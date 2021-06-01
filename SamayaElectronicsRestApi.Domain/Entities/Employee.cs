using System;
using SamayaElectronicsRestApi.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Domain.Entities
{
    public class Employee:BaseEntity
    {
        public Employee()
        {
            WorksOns = new HashSet<WorksOn>();
        }
        public string FName { get; set; }
        public string LName { get; set; }
        public decimal salary { get; set; }
        public ICollection<WorksOn> WorksOns { get; set; }
        public int ? DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
