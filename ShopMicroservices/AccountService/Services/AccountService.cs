using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using AccountService.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Services
{
    public class AccountService : BaseService<UserModel>, IAccountService
    {
        public AccountService(IAccountRepository accountRepository) : base(accountRepository)
        {
        }
    }
}
