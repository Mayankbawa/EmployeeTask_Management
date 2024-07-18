namespace TaskManagementSystem.Models
{
    public class TaskReportVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
    }

    public class TeamTaskReportVM
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public int TotalTasks { get; set; }

        public int ActiveTasks { get; set; }

        public int NewTasks { get; set; }

        public int ClosedTasks { get; set; }
    }
}
