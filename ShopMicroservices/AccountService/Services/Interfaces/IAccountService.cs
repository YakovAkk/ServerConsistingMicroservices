using AccountData.Models;

namespace AccountService.Services.Interfaces
{
    public interface IAccountService
    {
        Task LogoutAsync();
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel> GetUserByIdAsync(string id);
        Task<UserModel> DeleteUserByIdAsync(string id);
    }
}
