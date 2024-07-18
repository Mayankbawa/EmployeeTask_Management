using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;
using TaskManagementSystem.Utilities;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamRepo teamRepo;

        public TeamController(ITeamRepo _teamRepo)
        {
            teamRepo = _teamRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TeamRequestVM model)
        {
            var response = new ResponseVM();

            bool data = await teamRepo.AddTeam(model);

            if (data)
            {
                response.status = (int)HttpStatusCode.OK;
                response.message = MessageHandler.ResponseMsg.Add_Success;
                response.data = data;

                return Ok(response);
            }
            else
            {
                response.status = (int)HttpStatusCode.BadRequest;
                response.message = MessageHandler.ResponseMsg.Error;
                response.data = data;

                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(TeamRequestVM model)
        {
            var response = new ResponseVM();

            bool data = await teamRepo.UpdateTeam(model);

            if (data)
            {
                response.status = (int)HttpStatusCode.OK;
                response.message = MessageHandler.ResponseMsg.Update_Success;
                response.data = data;

                return Ok(response);
            }
            else
            {
                response.status = (int)HttpStatusCode.BadRequest;
                response.message = MessageHandler.ResponseMsg.Error;
                response.data = data;

                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new ResponseVM();

            List<TeamVM> data = await teamRepo.GetAllTeams();

            if (data != null)
            {
                response.status = (int)HttpStatusCode.OK;
                response.message = MessageHandler.ResponseMsg.Get_Success;
                response.data = data;

                return Ok(response);
            }
            else
            {
                response.status = (int)HttpStatusCode.BadRequest;
                response.message = MessageHandler.ResponseMsg.Error;
                response.data = data;

                return BadRequest(response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int teamId)
        {
            var response = new ResponseVM();

            bool data = await teamRepo.DeleteTeam(teamId);

            if (data)
            {
                response.status = (int)HttpStatusCode.OK;
                response.message = MessageHandler.ResponseMsg.Delete_Success;
                response.data = data;

                return Ok(response);
            }
            else
            {
                response.status = (int)HttpStatusCode.BadRequest;
                response.message = MessageHandler.ResponseMsg.Error;
                response.data = data;

                return BadRequest(response);
            }
        }

        [HttpGet("GetTeamsById")]
        public async Task<IActionResult> GetTeamsById(int teamId)
        {
            var response = new ResponseVM();

            TeamVM data = await teamRepo.GetTeam(teamId);

            if (data != null)
            {
                response.status = (int)HttpStatusCode.OK;
                response.message = MessageHandler.ResponseMsg.Get_Success;
                response.data = data;

                return Ok(response);
            }
            else
            {
                response.status = (int)HttpStatusCode.BadRequest;
                response.message = MessageHandler.ResponseMsg.Error;
                response.data = data;

                return BadRequest(response);
            }
        }
    }
}
