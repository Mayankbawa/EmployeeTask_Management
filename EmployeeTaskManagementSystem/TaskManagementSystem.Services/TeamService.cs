using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DataAccess.TaskManagementEntities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public class TeamService : ITeamRepo
    {
        private readonly TaskManagementDBContext context;

        public TeamService(TaskManagementDBContext _context)
        {
            context = _context;
        }

        public async Task<bool> AddTeam(TeamRequestVM model)
        {
            bool status = false;
            try
            {
                var data = new Team()
                {
                    Name = model.Name,
                };

                await context.AddAsync(data);

                status = await context.SaveChangesAsync() > 0;

                if (status)
                {
                    await AddTeamMembers(model.TeamMembers, data.TeamId);
                }

                return status;
            }
            catch
            {
                return status;
            }
        }

        public async Task<bool> DeleteTeam(int teamId)
        {
            bool status = false;

            try
            {
                await DeleteTeamMembers(teamId);

                var data = await context.Teams.FirstOrDefaultAsync(a => a.TeamId == teamId);

                if (data != null)
                {
                    context.Teams.Remove(data);

                    status = await context.SaveChangesAsync() > 0;
                }

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }

        }

        public async Task<List<TeamVM>> GetAllTeams()
        {
            List<TeamVM> teamsData = new List<TeamVM>();

            try
            {
                teamsData = await context.Teams.Select(a =>
                new TeamVM
                {
                    TeamId = a.TeamId,
                    Name = a.Name,
                    TeamMembers = context.Teammembers.Where(t => t.TeamId == a.TeamId).Select(t => new TeamMemberVM
                    {
                        TeamId = a.TeamId,
                        TeamMemberId = t.TeamMemberId,
                        EmployeeId = t.employeeId,
                        EmployeeName = context.Employees.Where(e => e.EmployeeId == t.employeeId).Select(e => e.Name).FirstOrDefault() ?? ""

                    }).ToList()

                }).ToListAsync();

                return teamsData;
            }
            catch (Exception ex)
            {
                return teamsData;
            }
        }

        public async Task<TeamVM> GetTeam(int teamId)
        {
            TeamVM teamsData = new TeamVM();

            try
            {
                teamsData = await context.Teams.Where(a => a.TeamId == teamId).Select(a =>
                new TeamVM
                {
                    TeamId = a.TeamId,
                    Name = a.Name,
                    TeamMembers = context.Teammembers.Where(t => t.TeamId == a.TeamId).Select(t => new TeamMemberVM
                    {
                        TeamId = a.TeamId,
                        TeamMemberId = t.TeamMemberId,
                        EmployeeId = t.employeeId,
                        EmployeeName = context.Employees.Where(e => e.EmployeeId == t.employeeId).Select(e => e.Name).FirstOrDefault() ?? ""

                    }).ToList()

                }).FirstOrDefaultAsync();

                return teamsData;
            }
            catch (Exception ex)
            {
                return teamsData;
            }
        }

        public async Task<bool> UpdateTeam(TeamRequestVM model)
        {
            bool status = false;
            try
            {
                var data = await context.Teams.FirstOrDefaultAsync(a => a.TeamId == model.TeamId);

                if (data != null)
                {
                    data.Name = model.Name;

                    await UpdateTeamMembers(model.TeamMembers);
                }

                status = await context.SaveChangesAsync() > 0;

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }
        }


        #region 'Private Methods'

        private async Task<bool> AddTeamMembers(List<TeamMemberRequestVM> teamMembers, int teamId)
        {
            bool status = false;
            try
            {
                List<TeamMember> data = teamMembers.Select(a => new TeamMember
                {
                    TeamId = teamId,
                    employeeId = a.employeeId

                }).ToList();

                await context.AddRangeAsync(data);

                status = await context.SaveChangesAsync() > 0;

                return status;
            }
            catch
            {
                return status;
            }
        }

        private async Task<bool> DeleteTeamMembers(int teamId)
        {
            bool status = false;
            try
            {
                var data = await context.Teammembers.Where(a => a.TeamId == teamId).ToListAsync();

                if (data.Count > 0)
                {
                    context.Teammembers.RemoveRange(data);

                    status = await context.SaveChangesAsync() > 0;
                }

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        private async Task<bool> UpdateTeamMembers(List<TeamMemberRequestVM> teamMembers)
        {
            bool status = false;

            List<TeamMember> NeWMember = new List<TeamMember>();
            List<TeamMember> removedMember = new List<TeamMember>();

            try
            {
                var existingTeamMemeber = await context.Teammembers.Where(a => a.TeamId == teamMembers[0].TeamId).ToListAsync();

                foreach (var item in teamMembers)
                {
                    var data = existingTeamMemeber.Where(a => a.TeamId == item.TeamId && a.employeeId == item.employeeId).FirstOrDefault();

                    if (data == null)
                    {
                        var newMem = new TeamMember
                        {
                            TeamId = item.TeamId,
                            employeeId = item.employeeId
                        };

                        NeWMember.Add(newMem);
                    }
                }

                foreach (var item in existingTeamMemeber)
                {
                    var data = teamMembers.Where(a => a.TeamId == item.TeamId && a.employeeId == item.employeeId).FirstOrDefault();

                    if (data == null)
                    {
                        var newMem = new TeamMember
                        {
                            TeamId = item.TeamId,
                            employeeId = item.employeeId
                        };

                        removedMember.Add(newMem);
                    }
                }

                context.Teammembers.RemoveRange(removedMember);
                await context.AddRangeAsync(NeWMember);

                status = await context.SaveChangesAsync() > 0;

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        #endregion
    }
}
