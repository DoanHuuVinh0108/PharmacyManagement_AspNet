using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(
        nameof(CategoryId), 
        nameof(BatchNumber),
        nameof(MedicineId)
        )
    ]
    public class DestructiveMedicine
    {
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        [ForeignKey("MedicineId,BatchNumber,CategoryId")]
        public Medicine Medicine { get; set; } = null!;
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } = null!;
    }
}
