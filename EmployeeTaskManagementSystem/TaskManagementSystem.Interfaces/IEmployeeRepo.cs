using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<bool> AddEmployee(EmployeeVM model);
        Task<bool> UpadeateEmployee(EmployeeVM model);
        Task<bool> DeleteEmployee(int employeeId);
        Task<List<EmployeeVM>> GetEmployees();
        Task<EmployeeVM> GetEmployee(int employeeId);
    }
}
