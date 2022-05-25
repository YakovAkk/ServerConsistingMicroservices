using MassTransit;
using MicrocerviceContract.Contracts.CategoryContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMicroservices.Controllers.Base;
using ShopMicroservices.httpClient.Base;
using ShopMicroservices.MassTransit;
using ShopMicroservices.Models;

namespace ShopMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : MyControllerBase<CategoryModelDTO>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        private CategoryConsumer _categoryConsumer;

        public CategoryController(IHttpWorker httpWorker, IPublishEndpoint publishEndpoint) : base(httpWorker)
        {
            _publishEndpoint = publishEndpoint;
            _categoryConsumer = new CategoryConsumer();
           
        }

        [HttpPost]
        public override async Task<IActionResult> Create(CategoryModelDTO model)
        {
            await _publishEndpoint.Publish<CategoryContractCreate>(model);

            CategoryContractCreate myCategory = new CategoryContractCreate();

            _categoryConsumer.CategoryCreateEvent += (category) =>
            {
                myCategory = category;
            };

            if (myCategory == null)
            {
                return BadRequest();
            }
            return Ok(myCategory);
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
