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
    public class WorksOnConfiguration : IEntityTypeConfiguration<WorksOn>
    {
        public void Configure(EntityTypeBuilder<WorksOn> builder)
        {
            builder.HasKey(WO => new { WO.EmployeeId, WO.ProjectId });

            builder.HasOne(E => E.Employee)
                   .WithMany(E => E.WorksOns)
                   .HasForeignKey(WO => WO.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(E => E.Project)
                  .WithMany(E => E.WorksOns)
                  .HasForeignKey(WO => WO.ProjectId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.Property(WO => WO.EmployeeId).HasColumnName("EmpNO");
            builder.Property(WO => WO.ProjectId).HasColumnName("ProjectNo");

        }
    }
}
