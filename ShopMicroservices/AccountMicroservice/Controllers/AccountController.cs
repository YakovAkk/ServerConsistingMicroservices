using AccountData.Models;
using AccountMicroservice.DTO;
using AccountService.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService userService)
        {
            _accountService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO registrationUser)
        {
            var user = new UserModel()
            {
                NickName = registrationUser.NickName,
                Name = registrationUser.Email,
                Email = registrationUser.Email,
                Password = registrationUser.Password
            };

            var isHasUser = await _accountService.isExistUser(user);

            if (isHasUser)
            {
                var message = new
                {
                    result = "The user has already been included to database "
                };
                return BadRequest(message);
            }

            var userModel = await _accountService.RegistrationAsync(user);

            if (userModel.MessageThatWrong != null)
            {
                return BadRequest(userModel.MessageThatWrong);
            }

            return Ok(userModel);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO loginUser)
        {
            if (loginUser == null || string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            {
                var message = new
                {
                    result = "login or email is empty"
                };
                return BadRequest(message);
            }

            var user = new UserModel()
            {
                Name = loginUser.Email,
                Email = loginUser.Email,
                Password = loginUser.Password,
                RememberMe = loginUser.RememberMe
            };

            var loggedInModel = await _accountService.LoginAsync(user);

            if (loggedInModel.MessageThatWrong != null)
            {
                return BadRequest(loggedInModel.MessageThatWrong);
            }

            return Ok(loggedInModel);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> logoutUser([FromBody] UserLoginDTO loginUser)
        {
            await _accountService.LogoutAsync();
            return Ok(loginUser);
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllUser()
        {
            var result = await _accountService.GetAllAsync();

            if (result == null)
            {
                var message = new
                {
                    result = "Database hasn't any category"
                };
                return BadRequest(message);
            }
            return Ok(result);
        }
    }
}
