using TestTask2.Server.Data.Dto;

namespace TestTask2.Server.Services.Interfaces
{
    public interface IReportService
    {
        public Task<List<ReportItemResponseDTO>> GetReportsAsync();
        public Task<ReportDataResponseDTO?> GetReportDataAsync(int reportId);
        public Task<bool> UploadReport(IFormFile inputfile);
    }
}
