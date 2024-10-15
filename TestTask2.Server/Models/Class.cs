namespace TestTask2.Server.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Account> Accounts { get; set; } = [];
        public int ReportFileId { get; set; }
        public ReportFile? ReportFile { get; set; }

    }
}
