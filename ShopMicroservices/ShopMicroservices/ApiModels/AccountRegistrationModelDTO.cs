
using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class AccountRegistrationModelDTO : IModelDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
