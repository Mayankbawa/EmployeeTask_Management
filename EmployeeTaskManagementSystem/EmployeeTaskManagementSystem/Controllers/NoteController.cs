using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;
using TaskManagementSystem.Utilities;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private INoteRepo noteRepo;

        public NoteController(INoteRepo _noteRepo)
        {
            noteRepo = _noteRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteVM model)
        {
            var response = new ResponseVM();

            bool data = await noteRepo.AddNotes(model);

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
        public async Task<IActionResult> Update(NoteVM model)
        {
            var response = new ResponseVM();

            bool data = await noteRepo.UpdateNote(model);

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

            List<NoteVM> data = await noteRepo.GetAllNotes();

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
        public async Task<IActionResult> Delete(int noteId)
        {
            var response = new ResponseVM();

            bool data = await noteRepo.DeleteNotes(noteId);

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

        [HttpGet("GetNotesByTaskId")]
        public async Task<IActionResult> GetNotesByTaskId(int taskId)
        {
            var response = new ResponseVM();

            List<NoteVM> data = await noteRepo.GetAllNotesByTaskId(taskId);

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
