using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class HistoryModelDTO : IModelDTO
    {
        public string? Id { get; set; }
        public string User_Id { get; set; }
        public List<string> Orders_Id { get; set; }
    }
}
