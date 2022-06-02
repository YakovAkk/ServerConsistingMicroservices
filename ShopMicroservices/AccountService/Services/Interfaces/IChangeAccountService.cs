using AccountData.Models;
using AccountService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Services.Interfaces
{
    public interface IChangeAccountService
    {
        Task<UserModel> RegistrationAsync(UserRegistrationDTO item);
        Task<UserModel> UpdateAsync(UserRegistrationDTO item);
    }
}
