using AccountData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRepository.RepositorySql.Base
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected readonly AppDBContent _db;
        public BaseRepository(AppDBContent appDBContent)
        {
            _db = appDBContent;
        }
        public abstract Task<T> CreateAsync(T item);
        public abstract Task<T> FindUserByEmailAsync(string usersEmail);
        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<bool> isDataBaseHasUser(T item);
        public abstract Task<T> LoginAsync(T item);
        public abstract Task LogoutAsync();
        public abstract Task<T> UpdateAsync(T item);

    }
}
