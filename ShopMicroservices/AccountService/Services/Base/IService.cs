using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Services.Base
{
    public interface IService<T> 
    {
        Task<T> RegistrationAsync(T item);
        Task<T> LoginAsync(T item);
        Task<T> UpdateAsync(T item);
        Task LogoutAsync();
        Task<List<T>> GetAllAsync();
        Task<bool> isExistUser(T item);
    }
}
