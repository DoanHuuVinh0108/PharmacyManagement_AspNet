using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceCategory;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _categoryService.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCategoryRequest request)
        {
            try
            {
                var response = await _categoryService.CreateCategory(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request)
        {
            try
            {
                var response = await _categoryService.UpdateCategory(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await _categoryService.DeleteCategory(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
