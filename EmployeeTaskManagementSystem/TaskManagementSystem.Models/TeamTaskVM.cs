namespace TaskManagementSystem.Models
{
    public class TeamTaskVM
    {
        List<TeamTaskVM> Teams { get; set; }
    }

    public class Teams
    {
        public string Name { get; set; }

        public int TeamId { get; set; }

        public List<TemmatesVM> TeamMembers { get; set; }

    }

    public class TemmatesVM
    {
        public int TeamId { get; set; }

        public int TeamMemberId { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public List<TaskVM> Tasks { get; set; }
    }
}
