using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SamayaElectronicsRestApi.Domain.Entities;
using SamayaElectronicsRestApi.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Infrastructure.Context
{
    public class SamayaDbContext:IdentityDbContext
    {
        public SamayaDbContext(DbContextOptions<SamayaDbContext> options):base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorksOnConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.Entity<Project>(P =>
            {
                P.HasKey(P => P.Id);
                P.Property(P => P.Id).HasColumnName("ProjectNo");
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<WorksOn> WorksOns { get; set; }
        public DbSet<Project> Projects { get; set; }

    }
}
