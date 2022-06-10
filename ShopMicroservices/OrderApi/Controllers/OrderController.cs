using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Service.Base;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder([FromBody] OrderDTO orderDTO)
        {
            var result = await _orderService.MakeOrder(orderDTO);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }
            return Ok(result);
        }

    }
}
