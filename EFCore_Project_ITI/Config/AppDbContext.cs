using EFCore_Project_ITI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Project_ITI.Config
{
     class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee_Project> Employee_Projects { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Iti_Project.db");
            }
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT" },
                new Department { Id = 3, Name = "Finance" }
            );


            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, F_Name = "Hassan", L_Name = "Khaled", DepartmentId = 1 },
                new Employee { Id = 2, F_Name = "Dalal", L_Name = "Omar", DepartmentId = 2 },
                new Employee { Id = 3, F_Name = "Ali", L_Name = "Fares", DepartmentId = 2 },
                new Employee { Id = 4, F_Name = "Nada", L_Name = "Ahmed", DepartmentId = 3 }
            );


            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Payroll System" },
                new Project { Id = 2, Name = "E-Commerce Website" },
                new Project { Id = 3, Name = "Recruitment App" }
            );


            modelBuilder.Entity<Employee_Project>().HasData(
                new Employee_Project { Employee_Id = 1, Project_Id = 3 }, 
                new Employee_Project { Employee_Id = 2, Project_Id = 2 }, 
                new Employee_Project { Employee_Id = 3, Project_Id = 1 }, 
                new Employee_Project { Employee_Id = 4, Project_Id = 1 }, 
                new Employee_Project { Employee_Id = 4, Project_Id = 3 } 
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
