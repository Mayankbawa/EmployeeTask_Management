using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;
using TaskManagementSystem.Utilities;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepo reportRepo;

        public ReportController(IReportRepo _reportRepo)
        {
            reportRepo = _reportRepo;
        }

        [HttpGet("EmployeeTaskReport")]
        public async Task<IActionResult> GetTaskCompletionReport(DateTime startDate, DateTime endDate)
        {
            var response = new ResponseVM();

            List<TaskReportVM> data = await reportRepo.GetTaskCompletionReport(startDate, endDate);

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

        /// <summary>
        /// Report Type will be 1 -weekly, 2- Monthly
        /// </summary>
        [HttpGet("MonthlyOrWeeklyTaskReport")]
        public async Task<IActionResult> GetTaskCompletionReport(int reportType)
        {
            var response = new ResponseVM();

            List<TeamTaskReportVM> data = await reportRepo.GetWeeklyOrMonthLyReport(reportType);

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
