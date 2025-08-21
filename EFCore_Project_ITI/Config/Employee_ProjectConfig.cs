using EFCore_Project_ITI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Project_ITI.Config
{
    internal class Employee_ProjectConfig : IEntityTypeConfiguration<Employee_Project>
    {
        public void Configure(EntityTypeBuilder<Employee_Project> builder)
        {
            builder.ToTable("Employee_Projects");   

            builder.HasKey(ep => new { ep.Employee_Id, ep.Project_Id });

            builder.HasOne(ep => ep.Employee)
                   .WithMany(e => e.Employee_Projects)
                   .HasForeignKey(ep => ep.Employee_Id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ep => ep.Project)
                .WithMany(p => p.Employee_Projects)
                .HasForeignKey(ep => ep.Project_Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
