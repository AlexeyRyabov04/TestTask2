namespace TestTask2.Server.Data.Dto
{
    public class AccountResponseDTO
    {
        public string Number { get; set; } = string.Empty;
        public decimal ActiveInBalance { get; set; }
        public decimal PassiveInBalance { get; set; }
        public bool IsRecordResult { get; set; }
        public bool IsGroupResult { get; set; }
        public bool IsClassResult { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ActiveOutBalance { get; set; }
        public decimal PassiveOutBalance { get; set; }
    }
}
