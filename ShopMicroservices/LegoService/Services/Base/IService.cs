namespace LegoService.Services.Base
{
    public interface IService<TReturn, TInput>
    {
        Task<TReturn> AddAsync(TInput item);
        Task<TReturn> UpdateAsync(TInput item);
        Task<TReturn> DeleteAsync(string id);
        Task<TReturn> GetByIDAsync(string id);
        Task<List<TReturn>> GetAllAsync();
    }
}
