using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class BasketModelDTO : IModelDTO
    {
        public string Id { get; set; }
        public string Lego_Id { get; set; }
        public string User_Id { get; set; }
        public uint Amount { get; set; }
        public DateTime DateDeal { get; set; }
    }
}
