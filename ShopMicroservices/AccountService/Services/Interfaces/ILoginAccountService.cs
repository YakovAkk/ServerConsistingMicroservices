using AccountData.Models;
using AccountService.DTOs;

namespace AccountService.Services.Interfaces
{
    public interface ILoginAccountService
    {
        Task<UserModel> LoginAsync(UserLoginDTO item);
        Task<bool> isExistUser(UserLoginDTO item);
    }
}
