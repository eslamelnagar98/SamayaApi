using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get;}
        IDepartmentRepository Department { get; }
        IProjectRepository Project { get; }
        IWorksOnRepository WorksOn { get; }
    }
}
