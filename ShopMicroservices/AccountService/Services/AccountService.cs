using AccountBus.MassTransit.Contracts;
using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using AccountService.Services.Interfaces;
using MassTransit;

namespace AccountService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRequestClient<AccountContractLogin> _clientDelete;
        private readonly IAccountRepository _accountRepository;
        public AccountService(IRequestClient<AccountContractLogin> clientDelete, IAccountRepository accountRepository)
        {
            _clientDelete = clientDelete;
            _accountRepository = accountRepository;
        }
        public async Task<UserModel> DeleteUserByIdAsync(string id)
        {
            var categoryId = new AccountContractLogin() { Id = id };

            var response = await _clientDelete.GetResponse<AccountContractLogin>(categoryId);

            if (response == null)
            {
                return new UserModel()
                {
                    MessageThatWrong = "response is null"
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
        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _accountRepository.GetAllAsync();
        }
        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            return await _accountRepository.GetUserById(id);
        }
        public async Task LogoutAsync()
        {
            await _accountRepository.LogoutAsync();
        }
    }
}
