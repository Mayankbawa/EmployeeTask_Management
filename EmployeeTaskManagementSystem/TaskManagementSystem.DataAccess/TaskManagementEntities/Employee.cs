using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.DataAccess.TaskManagementEntities
{
    public partial class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? ManagerId { get; set; }
        public string employeeDesignation { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
