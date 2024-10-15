using Microsoft.AspNetCore.Mvc;
using TestTask2.Server.Models;
using TestTask2.Server.Services.Interfaces;

namespace TestTask2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetAllReports()
        {
            var files = await _reportService.GetReportsAsync();
            return Ok(files);
        }

        [HttpGet("reports/{reportFileId}")]
        public async Task<IActionResult> GetReportById(int reportFileId)
        {
            var reportData = await _reportService.GetReportDataAsync(reportFileId);
            return Ok(reportData);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile inputfile)
        {
            var result = await _reportService.UploadReport(inputfile);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Incorrect File structure");
        }
    }
}
