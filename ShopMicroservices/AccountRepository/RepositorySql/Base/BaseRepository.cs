using AccountData.Database;

namespace AccountRepository.RepositorySql.Base
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected readonly AppDBContent _db;
        public BaseRepository(AppDBContent db)
        {
            _db = db;
        }

        public abstract Task<T> CreateAsync(T item);
        public abstract Task<T> DeleteAsync(string id);
        public abstract Task<T> FindUserByEmailAsync(string usersEmail);
        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<T> GetUserById(string id);
        public abstract Task<bool> isDataBaseHasUser(T item);
        public abstract Task<T> LoginAsync(T item);
        public abstract Task LogoutAsync();
        public abstract Task<T> UpdateAsync(T item);

    }
}
