using HistoryData.Data.Models.Base;

namespace HistoryRepository.RepositoriesMongo.Base
{
    public interface IRepository<T> where T : IModel
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(string id);
        Task<T> AddAsync(T item);
        Task<T> DeleteAsync(string id);
        Task<T> UpdateAsync(T item);
    }
}
