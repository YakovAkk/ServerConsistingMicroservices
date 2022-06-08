using BasketData.Data.Models;

namespace BasketBus.MassTransit.Contracts
{
    public class BasketContractDelete
    {
        public string Id { get; set; }
        public string Lego_Id { get; set; }
        public string User_Id { get; set; }
        public int Amount { get; set; }
        public DateTime DateDeal { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}
