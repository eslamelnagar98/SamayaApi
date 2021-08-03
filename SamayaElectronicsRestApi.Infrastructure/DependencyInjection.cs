using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SamayaElectronicsRestApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection service
                                             , IConfiguration configuration)
        {
            service.AddDbContextPool<SamayaDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyDataBaseConnection"))
                       .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information)
                       .EnableSensitiveDataLogging();
            });
        }
    }
}
