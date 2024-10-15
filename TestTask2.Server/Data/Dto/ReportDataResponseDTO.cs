using TestTask2.Server.Models;

namespace TestTask2.Server.Data.Dto
{
    public class ReportDataResponseDTO
    {
        public string BankName { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string CreationDate { get; set; } = string.Empty;
        public List<ClassResponseDTO> Classes { get; set; } = [];
        public Account? Account { get; set; }


    }
}
