using LegoData.Data.Models;
using LegoService.DTOs;
using LegoService.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegoMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LegoController : ControllerBase
    {
        private readonly ILegoService _legoService;

        public LegoController(ILegoService legoService)
        {
            _legoService = legoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLego([FromBody] LegoModelDTO legoModel)
        {
            var result = await _legoService.AddAsync(legoModel);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }
            return Ok(result);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteLego([FromRoute] string Id)
        {
            var result = await _legoService.DeleteAsync(Id);

            if (result.MessageWhatWrong != null && result.MessageWhatWrong.Trim() != "")
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLego()
        {
            var result = await _legoService.GetAllAsync();

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

        [HttpPut]
        public async Task<IActionResult> UpdateLego([FromBody] LegoModelDTO legoModel)
        {
            var result = await _legoService.UpdateAsync(legoModel);

            if (result.MessageWhatWrong != null && result.MessageWhatWrong.Trim() != "")
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdLego([FromRoute] string Id)
        {
            var result = await _legoService.GetByIDAsync(Id);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }
    }
}
