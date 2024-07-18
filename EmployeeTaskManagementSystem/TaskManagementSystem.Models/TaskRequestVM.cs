namespace TaskManagementSystem.Models
{
    public class TaskRequestVM
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string status { get; set; }
        public int EmployeeId { get; set; }
    }
}
