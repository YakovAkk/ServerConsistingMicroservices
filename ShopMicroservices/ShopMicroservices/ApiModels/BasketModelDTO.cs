using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class BasketModelDTO : IModelDTO
    {
        public string Id { get; set; }
        public LegoModelDTO Lego { get; set; }
        public UserModelDTO User { get; set; }
        public uint Amount { get; set; }
        public DateTime DateDeal { get; set; }
    }
}
