namespace TestTask2.Server.Models
{
    public class ReportFile
    {
        public int Id { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string CreationDate { get; set; } = string.Empty;
        public List<Class> Classes { get; set; } = [];
        public Account? ReportAccount { get; set; }
        public int AccountId { get; set; }

    }
}
