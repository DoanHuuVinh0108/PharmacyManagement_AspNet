namespace PharmacyManagermentSystem.Request
{
    public class CreateImageCategoryRequest
    {
        public IFormFile File { get; set; }
        public string CategoryId { get; set; }
        
    }
}
