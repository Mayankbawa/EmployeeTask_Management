namespace TaskManagementSystem.Models
{
    public class NoteVM
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int TaskId { get; set; }
    }
}
