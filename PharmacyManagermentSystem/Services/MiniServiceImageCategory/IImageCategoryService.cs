using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceImageCategory
{
    public interface IImageCategoryService
    {
        Task<ImageCategoryResponse> CreateImageCategory(CreateImageCategoryRequest request);
        Task<bool> DeleteImageCategory(string id);
        Task<List<ImageCategogy>> GetAll();
    }
}
