using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryRepositories.RepositoriesMongo.Base
{
    public interface IMongoDB<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(string id);
        Task<T> AddAsync(T item);
        Task DeleteAsync(string id);
        Task<T> UpdateAsync(T item);

    }
}
