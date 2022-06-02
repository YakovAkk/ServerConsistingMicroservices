using AccountData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRepository.RepositorySql.Base
{
    public interface IAccountRepository : IRepository<UserModel>
    {
        
    }
}
