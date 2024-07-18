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
            try
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
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TeamTaskReportVM>> GetWeeklyOrMonthLyReport(int reportType)
        {
            List<TeamTaskReportVM> data = new List<TeamTaskReportVM>();
            try
            {
                var endDate = reportType == 1 ? DateTime.Now.AddDays(7) : DateTime.Now.AddMonths(1);

                data = await (from team in context.Teams
                              select new TeamTaskReportVM
                              {
                                  TeamId = team.TeamId,
                                  TeamName = team.Name,
                                  NewTasks = context.Tasks.Count(t => t.status == "New" &&
                                                         context.Teammembers.Any(tm => tm.TeamId == team.TeamId && tm.employeeId == t.EmployeeId) &&
                                                         t.DueDate <= endDate),
                                  ActiveTasks = context.Tasks.Count(t => t.status == "Active" &&
                                                         context.Teammembers.Any(tm => tm.TeamId == team.TeamId && tm.employeeId == t.EmployeeId) &&
                                                         t.DueDate <= endDate),
                                  ClosedTasks = context.Tasks.Count(t => t.status == "Closed" &&
                                                         context.Teammembers.Any(tm => tm.TeamId == team.TeamId && tm.employeeId == t.EmployeeId) &&
                                                         t.DueDate <= endDate),
                                  TotalTasks = context.Tasks.Count(t => context.Teammembers.Any(tm => tm.TeamId == team.TeamId && tm.employeeId == t.EmployeeId) && t.DueDate <= endDate),


                              }).ToListAsync();

                return data;

            }
            catch (Exception ex)
            {

                return data;
            }
        }

    }
}
