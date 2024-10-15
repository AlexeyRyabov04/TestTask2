namespace TestTask2.Server.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsClassResult { get; set; }
        public bool IsGroupResult { get; set; }
        public bool IsRecordResult { get; set; }
        public decimal ActiveInBalance { get; set; }
        public decimal PassiveInBalance { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ActiveOutBalance { get; set; }
        public decimal PassiveOutBalance { get; set; }
        public int? ClassId { get; set; }
        public Class? Class { get; set; }

    }
}
