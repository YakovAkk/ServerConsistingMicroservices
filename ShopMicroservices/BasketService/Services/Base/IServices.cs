using BasketData.Data.Models.Base;

namespace BasketService.Services.Base
{
    public interface IServices<TReturn, TParametr> where TReturn : IModel
    {
        Task<TReturn> AddAsync(TParametr item);
        Task<TReturn> UpdateAsync(TParametr item);
        Task<TReturn> DeleteAsync(string id);
        Task<TReturn> GetByIDAsync(string id);
        Task<List<TReturn>> GetAllAsync();
    }
}
