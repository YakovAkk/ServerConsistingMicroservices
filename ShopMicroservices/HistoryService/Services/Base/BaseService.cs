using HistoryData.Data.Models.Base;
using HistoryRepository.RepositoriesMongo.Base;

namespace HistoryService.Services.Base
{
    public abstract class BaseService<TR, TI> : IService<TR, TI> where TR : IModel
    {
        private readonly IRepository<TR> _repository;
        public BaseService(IRepository<TR> repository)
        {
            _repository = repository;
        }
        public abstract Task<TR> AddAsync(TI item);
        public abstract Task<TR> DeleteAsync(string id);
        public abstract Task<TR> UpdateAsync(TI item);
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
