using CategoryData.Data.Models.Base;
using CategoryRepositories.RepositoriesMongo.Base;

namespace CategoryServices.Services.Base
{
    public abstract class BaseService<T> : IService<T> where T : IModel
    {
        private readonly IRepository<T> _repository;
        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public abstract Task<T> AddAsync(T item);
        public abstract Task<T> DeleteAsync(string id);
        public abstract Task<T> UpdateAsync(T item);
        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<T> GetByIDAsync(string id)
        {
            return await _repository.GetByIDAsync(id);
        }
    }
}
