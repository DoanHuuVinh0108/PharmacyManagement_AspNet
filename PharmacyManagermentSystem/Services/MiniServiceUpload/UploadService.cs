using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using PharmacyManagermentSystem.Properties;
using PharmacyManagermentSystem.Response;
namespace PharmacyManagermentSystem.Services.MiniServiceUpload
{
    public class UploadService : IUploadService
    {
        private readonly Cloudinary _cloudinary;
        public UploadService(Cloudinary cloudinary)
        {
            _cloudinary = CloudinaryConfig.GetCloudinaryInstance();
        }
        public async Task<UploadImageResponse> UploadImage(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                    };
                    uploadResult =await _cloudinary.UploadAsync(uploadParams);
                }
            }
            UploadImageResponse response = new UploadImageResponse
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString()
            };
            return response;
        }
        public async Task<DeletionResult> DeleteImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deletionParams);
        }

    }
}
