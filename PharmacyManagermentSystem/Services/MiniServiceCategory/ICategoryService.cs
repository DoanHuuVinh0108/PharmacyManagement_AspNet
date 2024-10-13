using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceCategory
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateCategory(CreateCategoryRequest request);
        Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest request);
        Task<bool> DeleteCategory(string id);
        Task<List<Category>> GetAll();
    }
}
