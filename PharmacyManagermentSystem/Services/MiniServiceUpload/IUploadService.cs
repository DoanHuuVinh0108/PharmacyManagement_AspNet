using CloudinaryDotNet.Actions;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceUpload
{
    public interface IUploadService
    {
        Task<UploadImageResponse> UploadImage(IFormFile file);
        Task<DeletionResult> DeleteImage(string publicId);
    }
}
