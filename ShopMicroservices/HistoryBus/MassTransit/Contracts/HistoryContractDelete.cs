namespace HistoryBus.MassTransit.Contracts
{
    public class HistoryContractDelete
    {
        public string? Id { get; set; }
        public string User_Id { get; set; }
        public List<string> Orders_Id { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}
