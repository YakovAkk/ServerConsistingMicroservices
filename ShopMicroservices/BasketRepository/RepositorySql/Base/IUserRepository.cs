using BasketData.Data.Models;

namespace BasketRepository.RepositorySql.Base
{
    public interface IUserRepository
    {
       Task<UserModel> FindByEmailAsync(string email);
    }
}
