using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;
using PharmacyManagermentSystem.Services.MiniServiceUpload;

namespace PharmacyManagermentSystem.Services.MiniServiceImageCategory
{
    public class ImageCategoryService : IImageCategoryService
    {
        private readonly MyDbContext _Dbcontext;
        private readonly IUploadService _uploadService;
        public ImageCategoryService(MyDbContext Dbcontext, IUploadService uploadService)
        {
            _Dbcontext = Dbcontext;
            _uploadService = uploadService;
        }
        public async Task<ImageCategoryResponse> CreateImageCategory(CreateImageCategoryRequest request)
        {
            using( var transaction = _Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    var result = await _uploadService.UploadImage(request.File);
                    if(result == null)
                    {
                        return null;
                    }
                    var imageCategogy = new ImageCategogy()
                    {
                       Id = result.PublicId,
                       Url = result.Url,
                       CategoryId = request.CategoryId
                    };

                    await _Dbcontext.ImageCategogies.AddAsync(imageCategogy);
                    await _Dbcontext.SaveChangesAsync();
                    transaction.Commit();
                    return new ImageCategoryResponse()
                    {
                        Id = imageCategogy.Id,
                        Url = imageCategogy.Url,
                        CategoryId = imageCategogy.CategoryId
                    };
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return null;
                }
            }
            
        }
        public async Task<bool> DeleteImageCategory(string id)
        {
            using( var transaction = _Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    var deleteResult = await _uploadService.DeleteImage(id);
                    if (deleteResult == null || deleteResult.Result != "ok")
                    {
                        return false;
                    }
                    var imageCategory = await _Dbcontext.ImageCategogies.FindAsync(id);
                    if(imageCategory == null)
                    {
                        return false;
                    }
                    _Dbcontext.ImageCategogies.Remove(imageCategory);
                    await _Dbcontext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<List<ImageCategogy>> GetAll()
        {
           return await _Dbcontext.ImageCategogies.ToListAsync();
        }
    }
}
