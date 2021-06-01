using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamayaElectronicsRestApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(E => E.Id);
            builder.Property(E => E.DepartmentId).IsRequired(false);
            builder.Property(B => B.Id).HasColumnName("EmpNo");
            builder.Property(E => E.DepartmentId).HasColumnName("DeptNo");
            builder.HasOne(D => D.Department)
                   .WithMany(E => E.Employees)
                   .HasForeignKey(D=>D.DepartmentId);
        }
    }
}
