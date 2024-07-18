using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;
using TaskManagementSystem.Utilities;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentRepo documentRepo;

        public DocumentController(IDocumentRepo _documentRepo)
        {
            documentRepo = _documentRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormCollection model)
        {
            var response = new ResponseVM();

            bool data = await documentRepo.AddDocument(model);

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
        public async Task<IActionResult> Update(IFormCollection model)
        {
            var response = new ResponseVM();

            bool data = await documentRepo.UpdateDocument(model);

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

            List<DocumnetVM> data = await documentRepo.GetAllDocuments();

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
        public async Task<IActionResult> Delete(int documentId)
        {
            var response = new ResponseVM();

            bool data = await documentRepo.DeleteDocument(documentId);

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

        [HttpGet("GetDocumentsByTaskId")]
        public async Task<IActionResult> GetDocumentsByTaskId(int taskId)
        {
            var response = new ResponseVM();

            List<DocumnetVM> data = await documentRepo.GetAllDocumentsByTaskId(taskId);

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
