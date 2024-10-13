using PharmacyManagermentSystem.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Response
{
    public class ImageCategoryResponse
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string Url { get; set; }
    }
}
