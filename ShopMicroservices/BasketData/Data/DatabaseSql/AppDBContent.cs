using BasketData.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasketData.Data.DatabaseSql
{
    public class AppDBContent : IdentityDbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
