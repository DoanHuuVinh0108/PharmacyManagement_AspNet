using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(Id), nameof(BatchNumber), nameof(CategoryId))]
    public class Medicine
    {
        public string Id { get; set; }
        public string BatchNumber { get; set; }
        public DateOnly ManufacturingDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string CategoryId { get; set; }
        public string MedicineName { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new HashSet<ReceiptDetail>();
        public ICollection<DestructiveMedicine> DestructiveMedicines { get; set; } = new HashSet<DestructiveMedicine>();
        public ICollection<ReturnSupplier> ReturnSuppliers { get; set; } = new HashSet<ReturnSupplier>();
        public ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }
}
