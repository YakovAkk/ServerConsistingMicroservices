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
             UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             AppDBContent db) : base(db)
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
                UserName = item.Email,
                Email = item.Email
            };

            var result = await _userManager.CreateAsync(user, item.Password);

            if (result.Succeeded)
            {
                var User = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                var userAddDb = new UserModel()
                {
                    Id = User.Id,
                    Name = User.Email,
                    NickName = User.UserName,
                    Email = User.Email,
                    Password = item.Password,
                    RememberMe = false,
                    MessageThatWrong = ""
                };

                try
                {
                    _db.Users.Add(userAddDb);
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    
                }
                

                return userAddDb;
            }
            else
            {
                var User = new UserModel();
                User.MessageThatWrong = "Error when creating user \n";

                foreach (var res in result.Errors)
                {
                    User.MessageThatWrong += res.Description;
                    User.MessageThatWrong += "\n";
                }

                return User;
            }
        }

        public override async Task<UserModel> DeleteAsync(string id)
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

        public override async Task<UserModel> GetUserById(string id)
        {
            if (id == null)
            {
                var User = new UserModel();
                User.MessageThatWrong = "Id was empty";
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

            var result = await _db.Users.FirstOrDefaultAsync(u => u.Id == item.Id);

            return (result) == null ? false : true;
        }

        public override async Task<UserModel> LoginAsync(UserModel item)
        {
            //var result = await _signInManager.PasswordSignInAsync(item.Email,
            //         item.Password, item.RememberMe, false);

            var result = await FindUserByEmailAsync(item.Email);

            if (result != null)
            {
                return result;
            }
            else
            {

                var User = new UserModel();
                User.MessageThatWrong = "The user doesn't contained in database";
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
                
                return item;
            }
            else
            {
                var User = new UserModel();
                User.MessageThatWrong = "Error when creating user \n";

                foreach (var res in result.Errors)
                {
                    User.MessageThatWrong += res.Description;
                    User.MessageThatWrong += "\n";
                }

                return User;
            }
        }
    }
}
