using EFCore_Project_ITI.Config;
using EFCore_Project_ITI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore_Project_ITI
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            bool looping = true;
            while (looping)
            {
                Console.WriteLine(" 1. Employee 2. Department 3. Project 0. Exit");
                int choice = int.Parse(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        bool Emp_Loop = true;
                        while (Emp_Loop)
                        {
                            Console.WriteLine("Enter the num of the operation you want : ");
                            Console.WriteLine("1. Add Employee");
                            Console.WriteLine("2. Display Employees");
                            Console.WriteLine("3. Update Employee");
                            Console.WriteLine("4. Delete Employee");
                            Console.WriteLine("0. Exit");
                            int empChoice = int.Parse(Console.ReadLine());
                            switch (empChoice)
                            {
                                case 1:
                                    using(var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Adding a new Employee...");
                                        Console.WriteLine("Enter Employee ID:");
                                        int id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter Employee First Name:");
                                        string F_Name = Console.ReadLine();
                                        Console.WriteLine("Enter Employee Last Name:");
                                        string L_Name = Console.ReadLine();
                                        var departments = context.Departments.ToList();
                                        if (departments.Count == 0)
                                        {
                                            Console.WriteLine("No departments found.");
                                            break;
                                        }

                                        Console.WriteLine("Departments:");
                                        foreach (var dept in departments)
                                        {
                                            Console.WriteLine($"ID: {dept.Id}, Name: {dept.Name}");
                                        }
                                        Console.WriteLine("Enter Employee Department ID:");
                                        int departmentId = int.Parse(Console.ReadLine());
                                        var employee = new Employee
                                        {
                                           Id = id,
                                           F_Name = F_Name,
                                           L_Name = L_Name,
                                           DepartmentId = departmentId
                                        };
                                        context.Employees.Add(employee);
                                        context.SaveChanges();
                                        Console.WriteLine("Employee added successfully.");
                                    }
                                    break;
                                case 2:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Displaying all Employees...");
                                        var employees = context.Employees
                                            .Include(e => e.Department)
                                              .Include(e => e.Employee_Projects)
                                                 .ThenInclude(ep => ep.Project)
                                                   .ToList();

                                        if (employees.Count == 0)
                                        {
                                            Console.WriteLine("No employees found.");
                                            break;
                                        }
                                        foreach (var emp in employees)
                                        {
                                            Console.WriteLine($"ID: {emp.Id}, Name: {emp.F_Name} {emp.L_Name}, Department: {emp.Department?.Name}");

                                            if (emp.Employee_Projects == null || !emp.Employee_Projects.Any())
                                            {
                                                Console.WriteLine("   No Projects assigned.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("   Projects:");
                                                foreach (var ep in emp.Employee_Projects)
                                                {
                                                    Console.WriteLine($"     - {ep.Project.Name}");
                                                }
                                            }
                                        }

                                    }
                                    break;
                                case 3:
                                    using (var context = new AppDbContext())
                                    {
                                        var employees = context.Employees.ToList();
                                        if (employees.Count == 0)
                                        {
                                            Console.WriteLine("No employees found.");
                                            break;
                                        }
                                        foreach (var emp in employees)
                                        {
                                            Console.WriteLine($"ID: {emp.Id}, Name: {emp.F_Name} {emp.L_Name}");
                                        }
                                        Console.WriteLine("Enter the id for employee you want to update : ");
                                        int id = int.Parse(Console.ReadLine());
                                        var employee = context.Employees.Find(id);
                                        Console.WriteLine("1. First Name 2. Last Name");
                                        int updateChoice = int.Parse(Console.ReadLine());
                                        if (employee == null)
                                        {
                                            Console.WriteLine("Employee not found.");
                                            break;
                                        }
                                        switch (updateChoice)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter new First Name:");
                                                string newFName = Console.ReadLine();
                                                employee.F_Name = newFName;
                                                break;
                                            case 2:
                                                Console.WriteLine("Enter new Last Name:");
                                                string newLName = Console.ReadLine();
                                                employee.L_Name = newLName;
                                                break;
                                            default:
                                                Console.WriteLine("Invalid choice.");
                                                continue;
                                        }
                                        context.SaveChanges();
                                        Console.WriteLine("Employee updated successfully.");
                                    }
                                   
                                    break;
                                case 4:
                                    using (var context = new AppDbContext())
                                    {
                                        var employees = context.Employees.ToList();
                                        if (employees.Count == 0)
                                        {
                                            Console.WriteLine("No employees found.");
                                            break;
                                        }
                                        foreach (var emp in employees)
                                        {
                                            Console.WriteLine($"ID: {emp.Id}, Name: {emp.F_Name} {emp.L_Name}");
                                        }
                                        Console.WriteLine("Enter the id for employee you want to delete : ");
                                        int id = int.Parse(Console.ReadLine());
                                        var employee = context.Employees.Find(id);
                                        if (employee == null)
                                        {
                                            Console.WriteLine("Employee not found.");
                                            break;
                                        }
                                        context.Employees.Remove(employee);
                                        context.SaveChanges();
                                        Console.WriteLine("Employee deleted successfully.");
                                    }
                                    break;
                                case 0:
                                    Emp_Loop = false;
                                    Console.WriteLine("Exiting Employee operations.");
                                    Console.WriteLine();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice, please try again.");
                                    Console.WriteLine();
                                    break;
                            }
                        }

                        break;
                    case 2:
                        bool Dept_Loop = true;
                        while (Dept_Loop)
                        {
                            Console.WriteLine("Enter the num of the operation you want : ");
                            Console.WriteLine("1. Add Department");
                            Console.WriteLine("2. Display Departments");
                            Console.WriteLine("3. Update Department");
                            Console.WriteLine("4. Delete Department");
                            Console.WriteLine("5. Add Employees to Department");
                            Console.WriteLine("0. Exit");
                            int DeptChoice = int.Parse(Console.ReadLine());
                            switch (DeptChoice)
                            {
                                case 1:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Adding a new Department...");
                                        Console.WriteLine("Enter Department ID:");
                                        int id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter Department Name:");
                                        string name = Console.ReadLine();
                                        var department = new Department
                                        {
                                            Id = id,
                                            Name = name
                                        };
                                        context.Departments.Add(department);
                                        context.SaveChanges();
                                        Console.WriteLine("Department added successfully.");
                                    }
                                    break;
                                case 2:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Displaying all Departments...");
                                        var departments = context.Departments.Include(e => e.Employees).ToList();
                                        if (departments.Count == 0)
                                        {
                                            Console.WriteLine("No departments found.");
                                            break;
                                        }
                                        foreach (var dept in departments)
                                        {
                                            Console.WriteLine($"ID: {dept.Id}, Name: {dept.Name}");

                                            if (dept.Employees == null || !dept.Employees.Any())
                                            {
                                                Console.WriteLine("  No Employees assigned.");
                                            }
                                            else
                                            {
                                                foreach (var emp in dept.Employees)
                                                {
                                                    Console.WriteLine($"  Employee ID: {emp.Id}, Name: {emp.F_Name} {emp.L_Name}");
                                                }
                                            }
                                        }

                                    }
                                    break;
                                case 3:
                                    using (var context = new AppDbContext())
                                    {
                                        var departments = context.Departments.ToList();
                                        if (departments.Count == 0)
                                        {
                                            Console.WriteLine("No departments found.");
                                            break;
                                        }

                                        Console.WriteLine("Departments:");
                                        foreach (var dept in departments)
                                        {
                                            Console.WriteLine($"ID: {dept.Id}, Name: {dept.Name}");
                                        }

                                        Console.Write("Enter the ID of the department you want to update: ");
                                        int id = int.Parse(Console.ReadLine());

                                        var department = context.Departments.Find(id);

                                        if (department == null)
                                        {
                                            Console.WriteLine("Department not found.");
                                            break;
                                        }

                                        Console.Write("Enter new Department Name: ");
                                        string newName = Console.ReadLine();

                                        if (!string.IsNullOrWhiteSpace(newName))
                                        {
                                            department.Name = newName;
                                            context.SaveChanges();
                                            Console.WriteLine("Department updated successfully.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Department name cannot be empty.");
                                        }
                                    }
                                    break;

                                case 4:
                                    using (var context = new AppDbContext())
                                    {
                                        var departments = context.Departments.ToList();
                                        if (departments.Count == 0)
                                        {
                                            Console.WriteLine("No departments found.");
                                            break;
                                        }

                                        Console.WriteLine("Departments:");
                                        foreach (var dept in departments)
                                        {
                                            Console.WriteLine($"ID: {dept.Id}, Name: {dept.Name}");
                                        }
                                        Console.WriteLine("Enter the id for department you want to delete : ");
                                        int id = int.Parse(Console.ReadLine());
                                        var department = context.Departments.Find(id);
                                        if (department == null)
                                        {
                                            Console.WriteLine("Department not found.");
                                            break;
                                        }
                                        context.Departments.Remove(department);
                                        context.SaveChanges();
                                        Console.WriteLine("Department deleted successfully.");
                                    }
                                    break;
                                case 5: 
                                    using (var context = new AppDbContext())
                                    {
                                        Console.Write("Enter Department Name: ");
                                        string deptName = Console.ReadLine();

                                        var department = new Department { Name = deptName };

                                        Console.Write("How many employees do you want to add to this department? ");
                                        int count = int.Parse(Console.ReadLine());

                                        for (int i = 0; i < count; i++)
                                        {
                                            Console.WriteLine($"--- Employee {i + 1} ---");
                                            Console.Write("First Name: ");
                                            string fName = Console.ReadLine();

                                            Console.Write("Last Name: ");
                                            string lName = Console.ReadLine();

                                            var emp = new Employee
                                            {
                                                F_Name = fName,
                                                L_Name = lName,
                                                Department = department 
                                            };

                                            department.Employees.Add(emp);
                                        }

                                        context.Departments.Add(department);
                                        context.SaveChanges();

                                        Console.WriteLine("Department and employees saved successfully!");
                                    }
                                    break;

                                case 0:
                                    Dept_Loop = false;
                                    Console.WriteLine("Exiting Department operations.");
                                    Console.WriteLine();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice, please try again.");
                                    Console.WriteLine();
                                    break;
                            }
                        }
                        break;
                    case 3:
                        bool Project_Loop = true;
                        while (Project_Loop)
                        {
                            Console.WriteLine("Enter the num of the operation you want : ");
                            Console.WriteLine("1. Add Project");
                            Console.WriteLine("2. Display Projects");
                            Console.WriteLine("3. Update Project");
                            Console.WriteLine("4. Delete Project");
                            Console.WriteLine("5. Add Employee to project");
                            Console.WriteLine("0. Exit");
                            int P_Choice = int.Parse(Console.ReadLine());
                            switch (P_Choice)
                            {
                                case 1:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Adding a new Project...");
                                        Console.WriteLine("Enter Project ID:");
                                        int id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter Project Name:");
                                        string name = Console.ReadLine();
                                        var project = new Project
                                        {
                                            Id = id,
                                            Name = name
                                        };
                                        context.Projects.Add(project);
                                        context.SaveChanges();
                                        Console.WriteLine("Project added successfully.");
                                    }
                                    break;
                                case 2:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Displaying all Projects...");

                                        var projects = context.Projects
                                                              .Include(p => p.Employee_Projects)
                                                              .ThenInclude(ep => ep.Employee)
                                                              .ToList();

                                        if (projects.Count == 0)
                                        {
                                            Console.WriteLine("No projects found.");
                                            break;
                                        }

                                        foreach (var proj in projects)
                                        {
                                            Console.WriteLine($"ID: {proj.Id}, Name: {proj.Name}");
                                            Console.WriteLine("Employees assigned to this project:");

                                            if (proj.Employee_Projects.Count == 0)
                                            {
                                                Console.WriteLine("  No employees assigned.");
                                            }
                                            else
                                            {
                                                foreach (var empProj in proj.Employee_Projects)
                                                {
                                                    Console.WriteLine($"  Employee Name: {empProj.Employee.F_Name} {empProj.Employee.L_Name}");
                                                }
                                            }
                                           
                                        }
                                    }
                                    break;

                                case 3:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Enter the id for project you want to update : ");
                                        int id = int.Parse(Console.ReadLine());
                                        var project = context.Projects.Find(id);
                                        if (project == null)
                                        {
                                            Console.WriteLine("Project not found.");
                                            break;
                                        }
                                        Console.WriteLine("Enter new Project Name:");
                                        string newName = Console.ReadLine();
                                        project.Name = newName;
                                        context.SaveChanges();
                                        Console.WriteLine("Project updated successfully.");
                                    }
                                    break;
                                case 4:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Enter the id for project you want to delete : ");
                                        int id = int.Parse(Console.ReadLine());
                                        var project = context.Projects.Find(id);
                                        if (project == null)
                                        {
                                            Console.WriteLine("Project not found.");
                                            break;
                                        }
                                        context.Projects.Remove(project);
                                        context.SaveChanges();
                                        Console.WriteLine("Project deleted successfully.");
                                    }
                                    break;
                                case 5:
                                    using (var context = new AppDbContext())
                                    {
                                        Console.WriteLine("Enter Employee Id:");
                                        int empId = int.Parse(Console.ReadLine());
                                        var employee = context.Employees.Find(empId);
                                        if (employee == null)
                                        {
                                            Console.WriteLine("Employee not found.");
                                            break;
                                        }

                                        Console.WriteLine("Enter Project Id:");
                                        int projectId = int.Parse(Console.ReadLine());
                                        var project = context.Projects.Find(projectId);
                                        if (project == null)
                                        {
                                            Console.WriteLine("Project not found.");
                                            break;
                                        }

                                        var existingRelation = context.Employee_Projects
                                            .FirstOrDefault(ep => ep.Employee_Id == empId && ep.Project_Id == projectId);
                                        if (existingRelation != null)
                                        {
                                            Console.WriteLine("Employee is already assigned to this project.");
                                            break;
                                        }

                                        var empProject = new Employee_Project
                                        {
                                            Employee_Id = empId,
                                            Project_Id = projectId
                                        };

                                        context.Employee_Projects.Add(empProject);
                                        context.SaveChanges();

                                        Console.WriteLine($"Employee {employee.F_Name} {employee.L_Name} assigned to Project {project.Name} successfully.");
                                    }
                                    break;

                                case 0:
                                    Project_Loop = false;
                                    Console.WriteLine("Exiting Project operations.");
                                    Console.WriteLine();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice, please try again.");
                                    Console.WriteLine();
                                    break;
                            }
                        }
                        break;
                    case 0:
                        looping = false;
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
