namespace HistoryService.DTOs
{
    public class HistoryModelDTO
    {
        public string? Id { get; set; }
        public string User_Id { get; set; }
        public List<string> Orders_Id { get; set; }
    }
}
