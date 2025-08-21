namespace EFCore_Project_ITI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;

        public ICollection<Employee_Project> Employee_Projects { get; set; } = new List<Employee_Project>();
    }
}
