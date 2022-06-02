
using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class AccountRegistrationModelDTO : IModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
