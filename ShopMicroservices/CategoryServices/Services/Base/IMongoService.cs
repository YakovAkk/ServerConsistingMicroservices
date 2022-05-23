using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryServices.Services.Base
{
    public interface IMongoService<T>
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(string id);
        Task<T> GetByIDAsync(string id);
        Task<List<T>> GetAllAsync();
    }
}
