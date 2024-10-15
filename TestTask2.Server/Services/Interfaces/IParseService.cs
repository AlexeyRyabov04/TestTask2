using TestTask2.Server.Models;

namespace TestTask2.Server.Services.Interfaces
{
    public interface IParseService
    {
        public ReportFile ParseFile(IFormFile reportFile);
    }
}
