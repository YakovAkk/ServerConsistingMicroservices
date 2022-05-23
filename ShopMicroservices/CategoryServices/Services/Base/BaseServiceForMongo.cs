using CategoryData.Data.Models.Base;
using CategoryRepositories.RepositoriesMongo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryServices.Services.Base
{
    public abstract class BaseServiceForMongo<T> : IMongoService<T> where T : IModel
    {
        private readonly MongoDbBase<T> _repository;
        public BaseServiceForMongo(MongoDbBase<T> repository)
        {
            _repository = repository;
        }
        public async Task<T> AddAsync(T item)
        {
            return await _repository.AddAsync(item);
        }
        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<T> GetByIDAsync(string id)
        {
            return await _repository.GetByIDAsync(id);
        }
        public async Task<T> UpdateAsync(T item)
        {
            return await _repository.UpdateAsync(item);
        }
    }
}
