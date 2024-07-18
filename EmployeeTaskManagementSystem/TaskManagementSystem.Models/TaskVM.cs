namespace TaskManagementSystem.Models
{
    public class TaskVM
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<DocumnetVM> Documents { get; set; }
        public List<NoteVM> Notes { get; set; }
    }
}
