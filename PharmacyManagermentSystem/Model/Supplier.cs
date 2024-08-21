using System.ComponentModel.DataAnnotations;

namespace PharmacyManagermentSystem.Model
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Receipt> Receipts { get; set; } = new HashSet<Receipt>();
    }
}
