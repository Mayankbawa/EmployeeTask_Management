using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DataAccess.TaskManagementEntities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public class ReprotService : IReportRepo
    {
        private readonly TaskManagementDBContext context;

        public ReprotService(TaskManagementDBContext _context)
        {
            context = _context;
        }

        public async Task<List<TaskReportVM>> GetTaskCompletionReport(DateTime startDate, DateTime endDate)
        {
            var report = await context.Tasks
            .Where(t => t.DueDate >= startDate && t.DueDate <= endDate)
            .GroupBy(t => t.Employee)
            .Select(g => new TaskReportVM
            {
                EmployeeId = g.Key.EmployeeId,
                EmployeeName = g.Key.Name,
                TotalTasks = g.Count(),
                CompletedTasks = g.Count(t => t.status == "Closed"),
                PendingTasks = g.Count(t => t.status != "Closed")

            }).ToListAsync();

            return report;
        }
    }
}
