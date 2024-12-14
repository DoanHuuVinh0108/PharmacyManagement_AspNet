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
            var category = await _Dbcontext.Categorys.FindAsync(request.Id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            category.MedicineName = request.MedicineName;
            category.Price = request.Price;
            category.ActiveIngredient = request.ActiveIngredient;
            category.Classification = request.Classification;
            category.Concentration = request.Concentration;
            category.Packaging = request.Packaging;
            category.Assistantherb = request.Assistantherb;
            category.ShelfLife = request.ShelfLife;
            category.Manufacturer = request.Manufacturer;
            category.CountryOfManufacture = request.CountryOfManufacture;
            await _Dbcontext.SaveChangesAsync();
            return new CategoryResponse
            {
                Id = request.Id,
                MedicineName = request.MedicineName,
                Price = request.Price,
                ActiveIngredient = request.ActiveIngredient,
                Classification = request.Classification,
                Concentration = request.Concentration,
                Packaging = request.Packaging,
                Assistantherb = request.Assistantherb,
                ShelfLife = request.ShelfLife,
                Manufacturer = request.Manufacturer,
                CountryOfManufacture = request.CountryOfManufacture
            };
        }
        public async Task<PaginatedList<CategoryResponse>> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            var totalItems = await _Dbcontext.Categorys.CountAsync();

            var categories = await _Dbcontext.Categorys
                                             .Skip((pageIndex - 1) * pageSize)
                                             .Take(pageSize)
                                             .Select(x => new CategoryResponse
                                             {
                                                 Id = x.Id,
                                                 MedicineName = x.MedicineName,
                                                 Price = x.Price,
                                                 ActiveIngredient = x.ActiveIngredient,
                                                 Classification = x.Classification,
                                                 Concentration = x.Concentration,
                                                 Packaging = x.Packaging,
                                                 Assistantherb = x.Assistantherb,
                                                 ShelfLife = x.ShelfLife,
                                                 Manufacturer = x.Manufacturer,
                                                 CountryOfManufacture = x.CountryOfManufacture,
                                                 Quantity = x.Medicines.Count(m=>m.Quantity>0)

                                             }).ToListAsync();

            return new PaginatedList<CategoryResponse>
            {
                Items = categories,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
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
        public async Task<List<FindResponse>> FindByName(string medicineName)
        {
            var category = await _Dbcontext.Categorys
                .Where(x => x.MedicineName.ToLower().Contains(medicineName.ToLower()))
                .Select(x => new FindResponse
                    {
                        Id = x.Id,
                        MedicineName = x.MedicineName,
                        Price = x.Price
                    }).Take(10).ToListAsync();
            return category;
        }
        public async Task<FindResponse> getById(string id)
        {
            var category = await _Dbcontext.Categorys.FindAsync(id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return new FindResponse
            {
                Id = category.Id,
                MedicineName = category.MedicineName,
                Price = category.Price
            };
        }
}
}
