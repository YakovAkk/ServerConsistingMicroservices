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
    public class HistoryController : MyControllerBase<HistoryModelDTO>
    {
        public override IHttpWorker _httpWorker { get ; set ; }

        public HistoryController()
        {
            _httpWorker = new MyHttpWorker(UrlEnum.HistoryApiUrl);
        }

        [HttpPost]
        public override async Task<IActionResult> Create(HistoryModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.PostAsync(data);

            if (httpResponse.IsSuccess)
            {
                return Ok(httpResponse.Data);
            }

            return BadRequest(httpResponse);
        }

        [HttpPut]
        public override async Task<IActionResult> Update(HistoryModelDTO model)
        {
            string data = JsonConvert.SerializeObject(model);
            var httpResponse = await _httpWorker.UpdateAsync(data);

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

        
    }
}
