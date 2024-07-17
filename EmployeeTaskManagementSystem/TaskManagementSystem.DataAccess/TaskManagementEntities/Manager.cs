using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.DataAccess.TaskManagementEntities
{
    public partial class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> TeamMembers { get; set; }
    }
}
