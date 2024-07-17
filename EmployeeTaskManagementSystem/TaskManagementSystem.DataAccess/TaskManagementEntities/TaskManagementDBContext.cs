using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.DataAccess.TaskManagementEntities
{
    public partial class TaskManagementDBContext : DbContext
    {
        public TaskManagementDBContext()
        {
        }
        public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
