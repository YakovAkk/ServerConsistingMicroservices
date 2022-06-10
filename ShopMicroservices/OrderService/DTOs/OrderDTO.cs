namespace OrderService.DTOs
{
    public class OrderDTO
    {
        public string User_Id { get; set; }
        public List<string> BasketIds { get; set; }
    }
}
