

using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class AccountLoginDTO : IModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
