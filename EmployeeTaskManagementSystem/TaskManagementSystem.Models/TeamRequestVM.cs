namespace TaskManagementSystem.Models
{
    public class TeamRequestVM
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public List<TeamMemberRequestVM> TeamMembers { get; set; }
    }
}
