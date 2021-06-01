using Microsoft.Extensions.DependencyInjection;
using SamayaElectronicsRestApi.Core.Interfaces;
using SamayaElectronicsRestApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Core
{
    public static class ServicesRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            services.AddScoped<IProjectRepository,ProjectRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IWorksOnRepository, WorksOnRepository>();
        }
    }
}
