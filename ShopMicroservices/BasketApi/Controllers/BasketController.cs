using BasketData.Data.Base.Models;
using BasketService.DTOs;
using BasketService.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBasketAsync([FromBody] BasketModelDTO basketModel)
        {
            var result = await _basketService.AddAsync(basketModel);

            if (result.MessageWhatWrong != null && result.MessageWhatWrong.Trim() != "")
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket([FromRoute] string Id)
        {
            await _basketService.DeleteAsync(Id);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBaskets()
        {
            var result = await _basketService.GetAllAsync();

            if (result.Count == 0)
            {
                var message = new
                {
                    result = "Database hasn't any lego in the basket"
                };
                return BadRequest(message);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasket([FromBody] BasketModelDTO basketModel)
        {
            var result = await _basketService.UpdateAsync(basketModel);

            if (result.MessageWhatWrong != null && result.MessageWhatWrong.Trim() != "")
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdBasketAsync([FromQuery] string Id)
        {
            var result = await _basketService.GetByIDAsync(Id);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }
    }
}
