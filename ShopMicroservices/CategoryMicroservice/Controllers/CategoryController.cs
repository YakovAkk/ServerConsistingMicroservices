using Bus.MassTransit.Contracts;
using CategoryData.Data.Models;
using CategoryServices.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CategoryMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel categoryModel)
        {
            var result = await _categoryService.AddAsync(categoryModel);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }

            return Ok(result);

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string Id)
        {
            var result = await _categoryService.DeleteAsync(Id);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }
            return Ok(result);

        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryModel categoryModel)
        {
            var result = await _categoryService.UpdateAsync(categoryModel);

            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllAsync();

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


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdCategory([FromRoute] string Id)
        {
            var result = await _categoryService.GetByIDAsync(Id);
            if (result.MessageWhatWrong != null)
            {
                return BadRequest(result.MessageWhatWrong);
            }
            return Ok(result);
        }

    }
}
