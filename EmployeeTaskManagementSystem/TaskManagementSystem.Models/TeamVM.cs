namespace TaskManagementSystem.Models
{
    public class TeamVM
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public ICollection<TeamMemberVM> TeamMembers { get; set; }
    }
}
