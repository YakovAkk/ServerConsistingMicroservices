using AccountBus.MassTransit.Contracts;
using AccountData.Models;
using AccountService.DTOs;
using AccountService.Services.Interfaces;
using MassTransit;

namespace AccountService.Services
{
    public class LoginAccountService : ILoginAccountService
    {
        private readonly IRequestClient<AccountContractIsExistUser> _clientIsExistUser;
        private readonly IRequestClient<AccountContractLogin> _clientLogin;
        public LoginAccountService(
            IRequestClient<AccountContractIsExistUser> clientIsExistUser, 
            IRequestClient<AccountContractLogin> clientLogin)
        {
            _clientIsExistUser = clientIsExistUser;
            _clientLogin = clientLogin; 
        }
        public async Task<bool> isExistUser(UserLoginDTO item)
        {
            var user = new AccountContractIsExistUser()
            {
                Email = item.Email
            };

            var response = await _clientIsExistUser.GetResponse<AccountContractIsExistUser>(user);

            if (response == null)
            {
                return false;
            }

            return response.Message.IsExistUser;
        }
        public async Task<UserModel> LoginAsync(UserLoginDTO item)
        {
            var user = new AccountContractLogin()
            {
                Email = item.Email
            };

            var response = await _clientLogin.GetResponse<AccountContractLogin>(user);

            if (response == null)
            {
                return new UserModel()
                {
                    MessageThatWrong = "Response is null"
                };
            }

            return new UserModel()
            {
                Id = response.Message.Id,
                Name = response.Message.Name,
                NickName = response.Message.NickName,
                Email = response.Message.Email,
                DataRegistration = response.Message.DataRegistration,
                RememberMe = response.Message.RememberMe,
                MessageThatWrong = response.Message.MessageThatWrong
            };


        }
    }
}
