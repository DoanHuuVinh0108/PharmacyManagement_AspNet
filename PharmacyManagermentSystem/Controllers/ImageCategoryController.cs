using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceImageCategory;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class ImageCategoryController : ControllerBase
    {
        private readonly IImageCategoryService _imageCategoryService;
        public ImageCategoryController(IImageCategoryService imageCategoryService)
        {
            _imageCategoryService = imageCategoryService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _imageCategoryService.GetAll();
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateImageCategory([FromForm]CreateImageCategoryRequest request)
        {
            try
            {
                var response = await _imageCategoryService.CreateImageCategory(request);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteImageCategory(string id)
        {
            try
            {
                var response = await _imageCategoryService.DeleteImageCategory(id);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
