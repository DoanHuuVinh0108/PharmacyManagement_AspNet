using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } =null!;
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public User Customer { get; set; } = null!;
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } = null!;
        public string? PrescriptionId { get; set; }
        public Prescription? Prescription { get; set; }
    }
}
