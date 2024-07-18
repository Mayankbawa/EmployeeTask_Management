using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface ITeamRepo
    {
        Task<bool> AddTeam(TeamRequestVM model);

        Task<bool> DeleteTeam(int teamId);

        Task<bool> UpdateTeam(TeamRequestVM model);

        Task<List<TeamVM>> GetAllTeams();

        Task<TeamVM> GetTeam(int teamId);
    }
}
