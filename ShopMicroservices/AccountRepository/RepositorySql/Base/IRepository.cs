namespace AccountRepository.RepositorySql.Base
{
    public interface IRepository<T>
    {
        Task<T> FindUserByEmailAsync(string usersEmail);
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T item);
        Task<T> LoginAsync(T item);
        Task<bool> isDataBaseHasUser(T item);
        Task LogoutAsync();
        Task<T> UpdateAsync(T item);
        Task<T> GetUserById(string id);
        Task<T> DeleteAsync(string id);
    }
}
