using BasketData.Data.Models;

namespace BasketBus.MassTransit.Contracts
{
    public class BasketContractCreate
    {
        public string? Id { get; set; }
        public LegoModel Lego { get; set; }
        public UserModel User { get; set; }
        public uint Amount { get; set; }
        public DateTime DateDeal { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}
