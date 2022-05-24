using MassTransit;
using MicrocerviceContract.Queue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopMicroservices.Controllers.Base;
using ShopMicroservices.httpClient.Base;
using ShopMicroservices.MassTransit;
using ShopMicroservices.Models;
using ShopMicroservices.MyBus;
using ShopMicroservices.RabbitMq;

namespace ShopMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : MyControllerBase<CategoryModelDTO>
    {
        private readonly IBus _bus;

        public CategoryController(IHttpWorker httpWorker, IBus bus) : base(httpWorker)
        {
            _bus = bus;
        }

        [HttpPost("RabbitMQ")]
        public async Task<IActionResult> RabbitMQ(CategoryModelDTO model){

            //Uri uri = new Uri(RabbitMqConsts.RabbitMqUri + '/');
            var endPoint = await _bus.GetSendEndpoint(_bus.Address);
            await endPoint.Send(model);
            return Ok();
        }

        [HttpPost]
        public override async Task<IActionResult> Create(CategoryModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);

            var httpResponse = await _httpWorker.PostAsync(_urlStorage.CategoryApiUrl, data);

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
