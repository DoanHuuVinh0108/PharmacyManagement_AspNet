using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceCategory
{
    public class CategoryService : ICategoryService
    {
        private readonly MyDbContext _Dbcontext;
        public CategoryService(MyDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Id = request.id,
                MedicineName = request.tenThuoc,
                Price = request.gia,
                ActiveIngredient = request.hoatChat,
                Classification = request.phanLoai,
                Concentration = request.nongDo,
                Packaging = request.dongGoi,
                Assistantherb = request.taDuoc,
                ShelfLife = request.tuoiTho,
                Manufacturer = request.congTySx,
                CountryOfManufacture = request.nuocSx
            };
            await _Dbcontext.Categorys.AddAsync(category);
            await _Dbcontext.SaveChangesAsync();
            return new CategoryResponse
            {
                Id = request.id,
                MedicineName = request.tenThuoc,
                Price = request.gia,
                ActiveIngredient = request.hoatChat,
                Classification = request.phanLoai,
                Concentration = request.nongDo,
                Packaging = request.dongGoi,
                Assistantherb = request.taDuoc,
                ShelfLife = request.tuoiTho,
                Manufacturer = request.congTySx,
                CountryOfManufacture = request.nuocSx
            };
        }
        public async Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest request)
        {
            var category = await _Dbcontext.Categorys.FindAsync(request.id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            category.MedicineName = request.tenThuoc;
            category.Price = request.gia;
            category.ActiveIngredient = request.hoatChat;
            category.Classification = request.phanLoai;
            category.Concentration = request.nongDo;
            category.Packaging = request.dongGoi;
            category.Assistantherb = request.taDuoc;
            category.ShelfLife = request.tuoiTho;
            category.Manufacturer = request.congTySx;
            category.CountryOfManufacture = request.nuocSx;
            await _Dbcontext.SaveChangesAsync();
            return new CategoryResponse
            {
                Id = request.id,
                MedicineName = request.tenThuoc,
                Price = request.gia,
                ActiveIngredient = request.hoatChat,
                Classification = request.phanLoai,
                Concentration = request.nongDo,
                Packaging = request.dongGoi,
                Assistantherb = request.taDuoc,
                ShelfLife = request.tuoiTho,
                Manufacturer = request.congTySx,
                CountryOfManufacture = request.nuocSx
            };
        }
        public async Task<List<Category>> GetAll()
        {
            return await _Dbcontext.Categorys.ToListAsync();
        }
        public async Task<bool> DeleteCategory(string id)
        {
            var category = await _Dbcontext.Categorys.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            _Dbcontext.Categorys.Remove(category);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
