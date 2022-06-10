using OrderData.Model.Base;

namespace OrderData.Model
{
    public class OrderModel : IModel
    {
        public string Id { get; set; }
        public string User_Id { get; set; }
        public List<string> BasketIds { get; set; }
        public bool IsOrderCompleted { get; set; }
        public string? MessageWhatWrong { get; set; }

        public OrderModel()
        {
            IsOrderCompleted = false;
        }
    }
}
