using AccountService.DTOs;
using AccountService.Services.Interfaces;
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
        private readonly IChangeAccountService _changeAccountService;
        private readonly ILoginAccountService _loginAccountService;
        public AccountController(
            IAccountService accountService,
            IChangeAccountService changeAccountService, 
            ILoginAccountService loginAccountService)
        {
            _accountService = accountService;
            _changeAccountService = changeAccountService;
            _loginAccountService = loginAccountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO registrationUser)
        {
            var result = await _changeAccountService.RegistrationAsync(registrationUser);

            if (result.MessageThatWrong != null && result.MessageThatWrong.Trim() != "")
            {
                return BadRequest(result.MessageThatWrong);
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO loginUser)
        {
            var result = await _loginAccountService.LoginAsync(loginUser);

            if (result.MessageThatWrong != null && result.MessageThatWrong.Trim() != "")
            {
                return BadRequest(result.MessageThatWrong);
            }

            return Ok(result);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> logoutUser([FromBody] UserLoginDTO loginUser)
        {
            await _accountService.LogoutAsync();

            return Ok(loginUser);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRegistrationDTO updateUser)
        {
            var result = await _changeAccountService.UpdateAsync(updateUser);

            if (result.MessageThatWrong != null && result.MessageThatWrong.Trim() != "")
            {
                return BadRequest(result.MessageThatWrong);
            }

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getUserById([FromRoute] string Id)
        {
            var result = await _accountService.GetUserByIdAsync(Id);

            if (result.MessageThatWrong != null && result.MessageThatWrong.Trim() != "")
            {
                return BadRequest(result.MessageThatWrong);
            }

            return Ok(result);
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
        [HttpDelete("{Id}")]
        public async Task<IActionResult> deleteUser([FromRoute] string Id)
        {
            var result = await _accountService.DeleteUserByIdAsync(Id);

            if (result.MessageThatWrong != null && result.MessageThatWrong.Trim() != "")
            {
                return BadRequest(result.MessageThatWrong);
            }

            return Ok(result);
        }
    }
}
