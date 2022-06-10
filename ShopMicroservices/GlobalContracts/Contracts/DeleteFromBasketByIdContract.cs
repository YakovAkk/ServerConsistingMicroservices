using GlobalContracts.Models;

namespace GlobalContracts.Contracts
{
    public class DeleteFromBasketByIdContract
    {
        public List<string> basketIdList { get; set; }
        public List<OrderModel> baskets { get; set; }
        public bool IsEverythingOk { get; set; }
        public string? MessageWhatWrong { get; set; }

        public DeleteFromBasketByIdContract()
        {
            baskets = new List<OrderModel>();
            basketIdList = new List<string>();
        }
    }
}
