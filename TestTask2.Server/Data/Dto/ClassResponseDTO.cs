namespace TestTask2.Server.Data.Dto
{
    public class ClassResponseDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public List<AccountResponseDTO> Accounts { get; set; } = [];
    }
}
