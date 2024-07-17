using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.DataAccess.TaskManagementEntities
{
    public partial class TeamMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamMemberId { get; set; }
        public int employeeId { get; set; }
        public Employee Employee { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
