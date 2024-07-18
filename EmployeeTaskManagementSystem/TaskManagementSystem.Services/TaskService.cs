using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DataAccess.TaskManagementEntities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskRepo
    {
        private readonly TaskManagementDBContext context;
        public TaskService(TaskManagementDBContext _context)
        {
            context = _context;
        }

        public async Task<bool> AddTask(TaskRequestVM model)
        {
            bool result = false;
            try
            {
                var data = new DataAccess.TaskManagementEntities.Task()
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    EmployeeId = model.EmployeeId,
                    status = model.status //New, Active, Closed
                };

                await context.AddAsync(data);

                result = await context.SaveChangesAsync() > 0;

                return result;

            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<bool> DeleteTask(int id)
        {
            bool status = false;

            try
            {
                var data = await context.Tasks.FirstOrDefaultAsync(a => a.TaskId == id);

                if (data != null)
                {
                    context.Tasks.Remove(data);

                    status = await context.SaveChangesAsync() > 0;
                }

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public async Task<List<TaskVM>> GetAllTasks()
        {
            var tasks = new List<TaskVM>();
            try
            {
                tasks = await context.Tasks.Select(a =>
                new TaskVM
                {
                    TaskId = a.TaskId,
                    Title = a.Title,
                    Description = a.Description,
                    DueDate = a.DueDate,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = context.Employees.Where(e => e.EmployeeId == a.EmployeeId).Select(a => a.Name).FirstOrDefault() ?? "",
                    Status = a.status,
                    Documents = context.Documents.Where(d => d.TaskId == a.TaskId).Select(d =>
                    new DocumnetVM
                    {
                        TaskId = a.TaskId,
                        DocumentId = d.DocumentId,
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        UploadedAt = d.UploadedAt

                    }).ToList(),
                    Notes = context.Notes.Where(n => n.TaskId == a.TaskId).Select(n =>
                    new NoteVM
                    {
                        TaskId = a.TaskId,
                        NoteId = n.NoteId,
                        Content = n.Content,
                        CreatedAt = n.CreatedAt,

                    }).ToList()

                }).ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                return tasks;
            }
        }

        public async Task<List<TaskVM>> GetTaskByEmployeeId(int employeeId)
        {
            var tasks = new List<TaskVM>();
            try
            {
                tasks = await context.Tasks.Where(a => a.EmployeeId == employeeId).Select(a =>
                new TaskVM
                {
                    TaskId = a.TaskId,
                    Title = a.Title,
                    Description = a.Description,
                    DueDate = a.DueDate,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = context.Employees.Where(e => e.EmployeeId == a.EmployeeId).Select(a => a.Name).FirstOrDefault() ?? "",
                    Status = a.status,
                    Documents = context.Documents.Where(d => d.TaskId == a.TaskId).Select(d =>
                    new DocumnetVM
                    {
                        TaskId = a.TaskId,
                        DocumentId = d.DocumentId,
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        UploadedAt = d.UploadedAt

                    }).ToList(),
                    Notes = context.Notes.Where(n => n.TaskId == a.TaskId).Select(n =>
                    new NoteVM
                    {
                        TaskId = a.TaskId,
                        NoteId = n.NoteId,
                        Content = n.Content,
                        CreatedAt = n.CreatedAt,

                    }).ToList()

                }).ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                return tasks;
            }
        }

        public async Task<TaskVM> GetTaskById(int id)
        {

            var tasks = new TaskVM();
            try
            {
                tasks = await context.Tasks.Select(a =>
                new TaskVM
                {
                    TaskId = a.TaskId,
                    Title = a.Title,
                    Description = a.Description,
                    DueDate = a.DueDate,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = context.Employees.Where(e => e.EmployeeId == a.EmployeeId).Select(a => a.Name).FirstOrDefault() ?? "",
                    Status = a.status,
                    Documents = context.Documents.Where(d => d.TaskId == a.TaskId).Select(d =>
                    new DocumnetVM
                    {
                        TaskId = a.TaskId,
                        DocumentId = d.DocumentId,
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        UploadedAt = d.UploadedAt

                    }).ToList(),
                    Notes = context.Notes.Where(n => n.TaskId == a.TaskId).Select(n =>
                    new NoteVM
                    {
                        TaskId = a.TaskId,
                        NoteId = n.NoteId,
                        Content = n.Content,
                        CreatedAt = n.CreatedAt,

                    }).ToList()

                }).FirstOrDefaultAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                return tasks;
            }
        }

        public async Task<bool> UpdateTask(TaskRequestVM model)
        {
            bool status = false;
            try
            {
                var taskData = await context.Tasks.FirstOrDefaultAsync(a => a.TaskId == model.TaskId);

                if (taskData != null)
                {
                    taskData.Title = model.Title;
                    taskData.Description = model.Description;
                    taskData.DueDate = model.DueDate;
                    taskData.status = model.status;//New, Active, Closed
                }

                status = await context.SaveChangesAsync() > 0;

                return status;

            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public async Task<List<TaskVM>> GetAllTeammembersTasks(int teamId)
        {
            var tasks = new List<TaskVM>();
            try
            {
                tasks = await (from task in context.Tasks
                               join emp in context.Employees on task.EmployeeId equals emp.EmployeeId
                               join teamMember in context.Teammembers on emp.EmployeeId equals teamMember.employeeId
                               join team in context.Teams on teamMember.TeamId equals team.TeamId
                               where team.TeamId == teamId
                               select new TaskVM
                               {
                                   TaskId = task.TaskId,
                                   Title = task.Title,
                                   Description = task.Description,
                                   DueDate = task.DueDate,
                                   EmployeeId = task.EmployeeId,
                                   EmployeeName = emp.Name,
                                   Status = task.status,
                                   Documents = context.Documents.Where(d => d.TaskId == task.TaskId).Select(d =>
                                   new DocumnetVM
                                   {
                                       TaskId = task.TaskId,
                                       DocumentId = d.DocumentId,
                                       FileName = d.FileName,
                                       FilePath = d.FilePath,
                                       UploadedAt = d.UploadedAt

                                   }).ToList(),
                                   Notes = context.Notes.Where(n => n.TaskId == task.TaskId).Select(n =>
                                   new NoteVM
                                   {
                                       TaskId = task.TaskId,
                                       NoteId = n.NoteId,
                                       Content = n.Content,
                                       CreatedAt = n.CreatedAt,

                                   }).ToList()

                               }).ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                return tasks;
            }
        }

        public async Task<List<Teams>> GetAllTeamsTasks()
        {
            List<Teams> data = new List<Teams>();

            try
            {
                data = await context.Teams.Select(t => new Teams
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                    TeamMembers = context.Teammembers.Where(m => m.TeamId == t.TeamId).Select(m =>
                    new TemmatesVM
                    {
                        TeamId = m.TeamId,
                        TeamMemberId = m.TeamMemberId,
                        employeeId = m.employeeId,
                        employeeName = context.Employees.Where(e => e.EmployeeId == m.employeeId).Select(a => a.Name).FirstOrDefault() ?? "",
                        Tasks = context.Tasks.Where(a => a.EmployeeId == m.employeeId).Select(a =>
                                new TaskVM
                                {
                                    TaskId = a.TaskId,
                                    Title = a.Title,
                                    Description = a.Description,
                                    DueDate = a.DueDate,
                                    EmployeeId = a.EmployeeId,
                                    EmployeeName = context.Employees.Where(e => e.EmployeeId == a.EmployeeId).Select(a => a.Name).FirstOrDefault() ?? "",
                                    Status = a.status,
                                    Documents = context.Documents.Where(d => d.TaskId == a.TaskId).Select(d =>
                                    new DocumnetVM
                                    {
                                        TaskId = a.TaskId,
                                        DocumentId = d.DocumentId,
                                        FileName = d.FileName,
                                        FilePath = d.FilePath,
                                        UploadedAt = d.UploadedAt

                                    }).ToList(),
                                    Notes = context.Notes.Where(n => n.TaskId == a.TaskId).Select(n =>
                                    new NoteVM
                                    {
                                        TaskId = a.TaskId,
                                        NoteId = n.NoteId,
                                        Content = n.Content,
                                        CreatedAt = n.CreatedAt,

                                    }).ToList()

                                }).ToList(),
                    }).ToList()

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
