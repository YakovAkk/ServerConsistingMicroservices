using MassTransit;
using MicrocerviceContract.Contracts.CategoryContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMicroservices.Controllers.Base;
using ShopMicroservices.httpClient.Base;

using ShopMicroservices.Models;

namespace ShopMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : MyControllerBase<CategoryModelDTO>
    {

        public CategoryController(IHttpWorker httpWorker ) : base(httpWorker)
        {
        }

        [HttpPost]
        public override async Task<IActionResult> Create(CategoryModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.PostAsync($"{_urlStorage.CategoryApiUrl}" , data);

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete([FromQuery] string Id)
        {
            var httpResponse = await _httpWorker.DeleteAsync($"{_urlStorage.CategoryApiUrl}/{Id}");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpGet("all")]
        public override async Task<IActionResult> GetAll()
        {
            var httpResponse = await _httpWorker.GetAsync($"{_urlStorage.CategoryApiUrl}/all");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById([FromQuery] string Id)
        {
            var httpResponse = await _httpWorker.GetAsync($"{_urlStorage.CategoryApiUrl}/{Id}");

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpPut]
        public override async Task<IActionResult> Update(CategoryModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.UpdateAsync(_urlStorage.CategoryApiUrl, data);

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }
    }
}
