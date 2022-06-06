using BasketData.Data.Models.Base;
using BasketRepository.RepositoriesMongo.Base;

namespace BasketService.Services.Base
{
    public abstract class BaseService<TR, TP> : IServices<TR, TP> where TR : IModel
    {
        private readonly IRepository<TR> _repository;
        public BaseService(IRepository<TR> repository)
        {
            _repository = repository;
        }
        public abstract Task<TR> AddAsync(TP item);
        public abstract Task<TR> DeleteAsync(string id);
        public abstract Task<TR> UpdateAsync(TP item);
        public async Task<List<TR>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<TR> GetByIDAsync(string id)
        {
            return await _repository.GetByIDAsync(id);
        }
    }
}
