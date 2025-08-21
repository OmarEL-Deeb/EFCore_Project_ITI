using EFCore_Project_ITI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Project_ITI.Config
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
        builder.ToTable("Employees");
            builder.Property(e => e.F_Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(e => e.L_Name)
               .HasMaxLength(50)
               .IsRequired();

        }
    }
}
