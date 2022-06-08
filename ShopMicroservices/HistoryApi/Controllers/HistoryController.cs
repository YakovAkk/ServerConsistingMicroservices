using HistoryService.DTOs;
using HistoryService.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HistoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService legoService)
        {
            _historyService = legoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLego([FromBody] HistoryModelDTO historyModel)
        {
            var result = await _historyService.AddAsync(historyModel);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }
            return Ok(result);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteLego([FromRoute] string Id)
        {
            var result = await _historyService.DeleteAsync(Id);

            if (result.MessageWhatWrong != null && result.MessageWhatWrong.Trim() != "")
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLego()
        {
            var result = await _historyService.GetAllAsync();

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
        public async Task<IActionResult> UpdateLego([FromBody] HistoryModelDTO historyModel)
        {
            var result = await _historyService.UpdateAsync(historyModel);

            if (result.MessageWhatWrong != null && result.MessageWhatWrong.Trim() != "")
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdLego([FromRoute] string Id)
        {
            var result = await _historyService.GetByIDAsync(Id);

            if (result.MessageWhatWrong != null && result.MessageWhatWrong.Trim() != "")
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);
        }
    }
}
