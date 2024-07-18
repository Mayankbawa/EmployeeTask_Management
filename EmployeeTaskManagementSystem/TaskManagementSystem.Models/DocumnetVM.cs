namespace TaskManagementSystem.Models
{
    public class DocumnetVM
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public int TaskId { get; set; }
    }
}
