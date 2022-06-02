using AccountData.Database;
using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountRepository.RepositorySql
{
    public class AccountRepository : BaseRepository<UserModel>, IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountRepository(
             AppDBContent appDBContent,
             UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager) : base(appDBContent)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override async Task<UserModel> CreateAsync(UserModel item)
        {
            if (item == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "Item was null";
                return User;
            }
            var user = new IdentityUser()
            {
                UserName = item.Name,
                Email = item.Email
            };

            var result = await _userManager.CreateAsync(user, item.Password);

            if (result.Succeeded)
            {
                await _db.Users.AddAsync(item);
                _db.SaveChanges();
                return item;
            }
            else
            {
                var User = new UserModel();
                User.MessageThatWrong = "Error when creating user \n";

                foreach (var res in result.Errors)
                {
                    User.MessageThatWrong += res;
                    User.MessageThatWrong += "\n";
                }

                return User;
            }
        }

        public override async Task<UserModel> DeleteAsync(int id)
        {
            if (id == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "Email was empty";
                return User;
            }

            var result = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            try
            {
                _db.Remove(result);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                var User = new UserModel();
                User.MessageThatWrong = "The element hasn't contained in database";
                return User;
            }

            return result;
        }

        public override async Task<UserModel> FindUserByEmailAsync(string usersEmail)
        {
            if (usersEmail == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "Email was empty";
                return User;
            }

            var result = await _db.Users.FirstOrDefaultAsync(u => u.Email == usersEmail);

            if (result == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "The element hasn't contained in database";
                return User;
            }

            return result;
        }

        public override async Task<List<UserModel>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public override async Task<UserModel> GetUserById(int id)
        {
            if (id == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "Email was empty";
                return User;
            }

            var result = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (result == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "The element hasn't contained in database";
                return User;
            }

            return result;
        }

        public override async Task<bool> isDataBaseHasUser(UserModel item)
        {
            if (item == null)
            {
                return false;
            }

            var result = await _db.Users.FirstOrDefaultAsync(u => (u.NickName == item.NickName)
            || (u.Email == item.Email));

            return (result) == null ? false : true;
        }

        public override async Task<UserModel> LoginAsync(UserModel item)
        {
            var result = await _signInManager.PasswordSignInAsync(item.Name,
                        item.Password, item.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == item.Name);

                if (user == null)
                {
                    var User = new UserModel();
                    User.MessageThatWrong = "The user hasn't contained in database";
                    return User;
                }

                return user;
            }
            else
            {

                var User = new UserModel();
                User.MessageThatWrong = "The user hasn't contained in database";
                return User;

            }
        }

        public override async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public override async Task<UserModel> UpdateAsync(UserModel item)
        {

            if (item == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "Item was null";
                return User;
            }

            var user = await _userManager.FindByIdAsync(item.Id.ToString());

            if (user == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "User Doesn't exist";
                return User;
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, item.Password);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _db.Users.Update(item);
                _db.SaveChanges();
                return item;
            }
            else
            {
                var User = new UserModel();
                User.MessageThatWrong = "Error when creating user \n";

                foreach (var res in result.Errors)
                {
                    User.MessageThatWrong += res;
                    User.MessageThatWrong += "\n";
                }

                return User;
            }
        }
    }
}
