using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DataAccess.TaskManagementEntities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;


namespace TaskManagementSystem.Services
{
    public class EmployeeService : IEmployeeRepo
    {
        private readonly TaskManagementDBContext context;

        public EmployeeService(TaskManagementDBContext _context)
        {
            context = _context;
        }

        public async Task<bool> AddEmployee(EmployeeVM model)
        {
            bool result = false;
            try
            {
                var employee = new Employee()
                {
                    Email = model.Email,
                    Name = model.Name,
                    ManagerId = model.ManagerId != null ? model.ManagerId : null,
                    employeeDesignation = model.employeeDesignation
                };

                await context.AddAsync(employee);

                result = await context.SaveChangesAsync() > 0;

                return result;

            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            bool status = false;
            try
            {
                var employeeData = await context.Employees.Where(a => a.EmployeeId == employeeId).FirstOrDefaultAsync();

                if (employeeData != null)
                {

                    context.Employees.Remove(employeeData);

                    status = await context.SaveChangesAsync() > 0;
                }

                return status;

            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public async Task<EmployeeVM> GetEmployee(int employeeId)
        {
            var employee = new EmployeeVM();
            try
            {
                employee = await context.Employees.Where(a => a.EmployeeId == employeeId)
                                  .Select(a =>
                                  new EmployeeVM
                                  {
                                      EmployeeId = a.EmployeeId,
                                      Name = a.Name,
                                      employeeDesignation = a.employeeDesignation,
                                      ManagerId = a.ManagerId,
                                      Email = a.Email,
                                      ManagerName = a.ManagerId != null ? context.Employees.Where(e => e.EmployeeId == a.ManagerId).Select(a => a.Name).FirstOrDefault() : ""

                                  }).FirstOrDefaultAsync();

                return employee;
            }
            catch (Exception ex)
            {
                return employee;
            }
        }

        public async Task<List<EmployeeVM>> GetEmployees()
        {
            var employees = new List<EmployeeVM>();

            try
            {
                employees = await context.Employees.Select(a =>
                                  new EmployeeVM
                                  {
                                      EmployeeId = a.EmployeeId,
                                      Name = a.Name,
                                      employeeDesignation = a.employeeDesignation,
                                      ManagerId = a.ManagerId,
                                      Email = a.Email,
                                      ManagerName = a.ManagerId != null ? context.Employees.Where(e => e.EmployeeId == a.ManagerId).Select(a => a.Name).FirstOrDefault() : ""

                                  }).ToListAsync();

                return employees;
            }
            catch (Exception ex)
            {
                return employees;
            }

        }

        public async Task<bool> UpadeateEmployee(EmployeeVM model)
        {
            bool status = false;
            try
            {
                var employeeData = await context.Employees.FirstOrDefaultAsync(a => a.EmployeeId == model.EmployeeId);

                if (employeeData != null)
                {
                    employeeData.Name = model.Name;
                    employeeData.ManagerId = model.ManagerId;
                    employeeData.Email = model.Email;
                    employeeData.employeeDesignation = model.employeeDesignation;
                }

                status = await context.SaveChangesAsync() > 0;

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }

        }
    }
}
