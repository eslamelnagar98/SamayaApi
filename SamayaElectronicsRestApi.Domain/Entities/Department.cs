using SamayaElectronicsRestApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Domain.Entities
{
    public class Department:BaseEntity
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
        public string DeptName { get; set; }
        public string Location { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
