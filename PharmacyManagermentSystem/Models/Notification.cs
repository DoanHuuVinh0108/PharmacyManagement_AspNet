using PharmacyManagermentSystem.Model;
using System.ComponentModel.DataAnnotations;

namespace PharmacyManagermentSystem.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Isread { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }

    }
}
