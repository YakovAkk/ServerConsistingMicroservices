using AccountData.Models;
using AccountService.DTOs;

namespace AccountService.Services.Interfaces
{
    public interface IChangeAccountService
    {
        Task<UserModel> RegistrationAsync(UserRegistrationDTO item);
        Task<UserModel> UpdateAsync(UserRegistrationDTO item);
    }
}
