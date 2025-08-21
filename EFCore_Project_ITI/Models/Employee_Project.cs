using EFCore_Project_ITI.Models;
namespace EFCore_Project_ITI.Models
{
    public class Employee_Project
    {   
        public int Employee_Id { get; set; }
        public int Project_Id { get; set; }

        public Employee Employee { get; set; } = null!;
        public Project Project { get; set; } = null!;

    }

}
