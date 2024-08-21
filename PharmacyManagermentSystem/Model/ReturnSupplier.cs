using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(
        nameof(Id), 
        nameof(CategoryId), 
        nameof(MedicineId), 
        nameof(BatchNumber))
    ]
    public class ReturnSupplier
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        [ForeignKey("CategoryId, MedicineId,BatchNumber")]
        public Medicine Medicine { get; set; } = null!;
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } = null!;


    }
}
