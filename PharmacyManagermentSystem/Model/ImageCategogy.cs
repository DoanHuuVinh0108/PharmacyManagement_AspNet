using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    public class ImageCategogy
    {
        [Key]
        public int Id { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        public string Url { get; set; }
    }
}
