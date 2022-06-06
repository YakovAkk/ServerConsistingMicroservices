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
    public class LegoController : MyControllerBase<LegoModelDTO>
    {
        public override IHttpWorker _httpWorker { get; set; }
        public LegoController()
        {
            _httpWorker = new MyHttpWorker(UrlEnum.LegoApiUrl);
        }

        [HttpPost]
        public async override Task<IActionResult> Create(LegoModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.PostAsync(data);

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }
        [HttpDelete("{Id}")]
        public async override Task<IActionResult> Delete([FromRoute] string Id)
        {
            var httpResponse = await _httpWorker.DeleteAsync($"{Id}");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }
        [HttpGet("all")]
        public async override Task<IActionResult> GetAll()
        {
            var httpResponse = await _httpWorker.GetAsync("all");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }
        [HttpGet("{Id}")]
        public async override Task<IActionResult> GetById([FromRoute] string Id)
        {
            var httpResponse = await _httpWorker.GetAsync($"{Id}");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }
        [HttpPut]
        public async override Task<IActionResult> Update(LegoModelDTO model)
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
