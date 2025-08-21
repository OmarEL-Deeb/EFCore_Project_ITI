namespace EFCore_Project_ITI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Employee_Project> Employee_Projects { get; set; } = new List<Employee_Project>();

    }
}
