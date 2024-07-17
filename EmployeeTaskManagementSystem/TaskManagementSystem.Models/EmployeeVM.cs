namespace TaskManagementSystem.Models
{
    public class EmployeeVM
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string employeeDesignation { get; set; }
        public int? ManagerId { get; set; }
        public string? ManagerName { get; set; }
    }
}
