using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;
using TaskManagementSystem.Utilities;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskRepo taskRepo;

        public TaskController(ITaskRepo _taskRepo)
        {
            taskRepo = _taskRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskRequestVM model)
        {
            var response = new ResponseVM();

            bool data = await taskRepo.AddTask(model);

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
        public async Task<IActionResult> Update(TaskRequestVM model)
        {
            var response = new ResponseVM();

            bool data = await taskRepo.UpdateTask(model);

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

            List<TaskVM> data = await taskRepo.GetAllTasks();

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


        [HttpGet("GetAllTaskById")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ResponseVM();

            TaskVM data = await taskRepo.GetTaskById(id);

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


        [HttpGet("GetAllTasksByEmployeeId")]
        public async Task<IActionResult> GetAllTasksByEmployeeId(int employeeId)
        {
            var response = new ResponseVM();

            List<TaskVM> data = await taskRepo.GetTaskByEmployeeId(employeeId);

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
        public async Task<IActionResult> Delete(int taskId)
        {
            var response = new ResponseVM();

            bool data = await taskRepo.DeleteTask(taskId);

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


        [HttpGet("GetAllTaskByTeamId")]
        public async Task<IActionResult> GetAllTaskByTeamId(int teamId)
        {
            var response = new ResponseVM();

            List<TaskVM> data = await taskRepo.GetAllTeammembersTasks(teamId);

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


        [HttpGet("GetAllTeamsTasks")]
        public async Task<IActionResult> GetAllTaskByTeamId()
        {
            var response = new ResponseVM();

            List<Teams> data = await taskRepo.GetAllTeamsTasks();

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
