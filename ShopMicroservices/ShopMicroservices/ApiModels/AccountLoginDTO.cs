using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class AccountLoginDTO : IModelDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
