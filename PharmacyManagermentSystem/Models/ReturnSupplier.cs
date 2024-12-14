using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey( 
        nameof(CategoryId), 
        nameof(MedicineId), 
        nameof(BatchNumber))
    ]
    public class ReturnSupplier
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public DateOnly Date { get; set; }
        [ForeignKey("MedicineId,BatchNumber,CategoryId")]
        public Medicine Medicine { get; set; } = null!;
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } = null!;


    }
}
