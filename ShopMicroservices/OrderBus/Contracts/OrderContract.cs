namespace OrderBus.Contracts
{
    public class OrderContract
    {
        public string Id { get; set; }
        public string User_Id { get; set; }
        public List<string> BasketIds { get; set; }
        public bool IsOrderCompleted { get; set; }
        public string? MessageWhatWrong { get; set; }

        public OrderContract()
        {
            BasketIds = new List<string>();
        }
    }
}
