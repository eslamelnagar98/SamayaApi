using SamayaElectronicsRestApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IProjectRepository projectRepository,
            IWorksOnRepository worksOnRepository)
        {
            Employee = employeeRepository;
            Department = departmentRepository;
            Project = projectRepository;
            WorksOn = worksOnRepository;
        }
        public IEmployeeRepository Employee { get;}
        public IDepartmentRepository Department { get;}
        public IProjectRepository Project { get; }
        public IWorksOnRepository WorksOn { get; }
    }
}
