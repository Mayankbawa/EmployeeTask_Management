using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskRepo
    {
        public Task<bool> AddTask(TaskRequestVM model);

        public Task<bool> UpdateTask(TaskRequestVM model);

        public Task<List<TaskVM>> GetAllTasks();

        public Task<TaskVM> GetTaskById(int id);

        public Task<List<TaskVM>> GetTaskByEmployeeId(int employeeId);

        public Task<bool> DeleteTask(int id);

        public Task<List<TaskVM>> GetAllTeammembersTasks(int teamId);

        public Task<List<Teams>> GetAllTeamsTasks();
    }
}
