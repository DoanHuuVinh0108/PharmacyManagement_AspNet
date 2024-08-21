using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } = null!;
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new HashSet<ReceiptDetail>();
    }
}
