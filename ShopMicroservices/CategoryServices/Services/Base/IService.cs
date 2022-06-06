namespace CategoryServices.Services.Base
{
    public interface IService<T>
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> DeleteAsync(string id);
        Task<T> GetByIDAsync(string id);
        Task<List<T>> GetAllAsync();
    }
}
