using AccountData.Models.Interfaces;
using AccountRepository.RepositorySql.Base;

namespace AccountService.Services.Base
{
    public abstract class BaseService<T> : IService<T> where T : IModel
    {
        private readonly IRepository<T> _accountRepository;
        public BaseService(IRepository<T> accountRepository)
        {
           _accountRepository = accountRepository;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<bool> isExistUser(T item)
        {
            return await _accountRepository.isDataBaseHasUser(item);
        }

        public async Task<T> LoginAsync(T item)
        {
            return await _accountRepository.LoginAsync(item);
        }

        public async Task LogoutAsync()
        {
            await _accountRepository.LogoutAsync();
        }

        public async Task<T> RegistrationAsync(T item)
        {
            return await _accountRepository.CreateAsync(item);
        }

        public async Task<T> UpdateAsync(T item)
        {
            return await _accountRepository.UpdateAsync(item);
        }
    }
}
