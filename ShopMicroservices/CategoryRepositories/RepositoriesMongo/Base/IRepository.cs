﻿using CategoryData.Data.Models.Base;

namespace CategoryRepositories.RepositoriesMongo.Base
{
    public interface IRepository<T> where T : IModel
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(string id);
        Task<T> AddAsync(T item);
        Task DeleteAsync(string id);
        Task<T> UpdateAsync(T item);

    }
}
