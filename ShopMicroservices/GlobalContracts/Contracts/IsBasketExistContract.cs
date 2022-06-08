namespace GlobalContracts.Contracts
{
    public class IsBasketExistContract
    {
        public bool IsBasketyExist { get; set; }
        public string BasketId { get; set; }

        public IsBasketExistContract()
        {
            BasketId = "";
        }
    }
}
