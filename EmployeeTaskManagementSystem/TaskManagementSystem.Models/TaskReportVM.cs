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
}
