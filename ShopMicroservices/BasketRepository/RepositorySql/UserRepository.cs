using BasketData.Data.DatabaseSql;
using BasketData.Data.Models;
using BasketRepository.RepositorySql.Base;
using Microsoft.EntityFrameworkCore;

namespace BasketRepository.RepositorySql
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContent _db;
        public UserRepository(AppDBContent db)
        {
            _db = db;
        }
        public async Task<UserModel> FindByEmailAsync(string email)
        {
            var result = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(result == null)
            {
                return new UserModel()
                {
                    MessageThatWrong = "Database doesn't contain the user"
                };
            }

            return result;
        }
    }
}
