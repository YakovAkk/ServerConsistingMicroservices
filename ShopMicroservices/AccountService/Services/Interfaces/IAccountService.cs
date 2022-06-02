using AccountData.Models;
using AccountData.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
