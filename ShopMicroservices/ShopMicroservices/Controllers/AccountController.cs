using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMicroservices.ApiModels;
using ShopMicroservices.Controllers.Base;
using ShopMicroservices.Enum;
using ShopMicroservices.httpClient.Base;
using ShopMicroservices.HttpWorker;

namespace ShopMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : MyControllerBase<AccountRegistrationModelDTO>
    {
        public override IHttpWorker _httpWorker { get ; set ; }

        public AccountController()
        {
            _httpWorker = new MyHttpWorker(UrlEnum.AccountApiUrl);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] AccountLoginDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.PostAsync(data, "Logout");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.PostAsync(data, "Login");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpPost("Register")]
        public override async Task<IActionResult> Create([FromBody] AccountRegistrationModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.PostAsync(data, "Register");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpDelete("{Id}")]
        public override async Task<IActionResult> Delete([FromRoute] string Id)
        {
            var httpResponse = await _httpWorker.DeleteAsync($"{Id}");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpGet("all")]
        public override async Task<IActionResult> GetAll()
        {
            var httpResponse = await _httpWorker.GetAsync("all");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpGet("{Id}")]
        public override async Task<IActionResult> GetById([FromRoute] string Id)
        {
            var httpResponse = await _httpWorker.GetAsync($"{Id}");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpPut]
        public override async Task<IActionResult> Update([FromBody] AccountRegistrationModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.UpdateAsync(data);

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }
    }
}
