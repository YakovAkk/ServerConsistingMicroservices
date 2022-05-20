using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : ControllerBase
    {
        private readonly IHttpWorker httpWorker;

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var client = new HttpClient();
            var httpResponse = await .GetAsync("https://localhost:7264/api/Category");

            if (httpResponse.IsSuccessStatusCode)
            {
                
            }

            return BadRequest(httpResponse);

        }
    }
}
