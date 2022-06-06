using AccountBus.MassTransit.Contracts;
using AccountData.Models;
using AccountService.DTOs;
using AccountService.Services.Interfaces;
using MassTransit;

namespace AccountService.Services
{
    public class ChangeAccountService : IChangeAccountService
    {
        private readonly ILoginAccountService _loginAccountService;
        private readonly IRequestClient<AccountContractRegistration> _clientRegistration;
        private readonly IRequestClient<AccountContractUpdate> _clientUpdate;
        public ChangeAccountService( 
            ILoginAccountService loginAccountService,
            IRequestClient<AccountContractRegistration> clientRegistration,
            IRequestClient<AccountContractUpdate> clientUpdate)
        {
            _loginAccountService = loginAccountService;
            _clientRegistration = clientRegistration;
            _clientUpdate = clientUpdate;
        }

        public async Task<UserModel> RegistrationAsync(UserRegistrationDTO item)
        {
            var user = new AccountContractRegistration()
            {
                Id = item.Id,
                Email = item.Email,
                Name = item.Name,
                Password = item.Password
            };

            var response = await _clientRegistration.GetResponse<AccountContractRegistration>(user);

            if(response == null)
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
        public async Task<UserModel> UpdateAsync(UserRegistrationDTO item)
        {

            var user = new AccountContractUpdate()
            {
                Email = item.Email,
                Name = item.Name,
                Password = item.Password
            };

            var response = await _clientUpdate.GetResponse<AccountContractUpdate>(user);

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
