using GlobalContracts.Models;

namespace GlobalContracts.Contracts
{
    public class AddToHistoryContract
    {
        public string User_Id { get; set; }
        public List<OrderModel> Orders { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}
