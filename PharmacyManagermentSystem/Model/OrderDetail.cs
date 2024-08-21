using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(
        nameof(CategoryId), 
        nameof(BatchNumber), 
        nameof(MedicineId),
        nameof(OrderId))
    ]
    public class OrderDetail
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string CategoryId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        [ForeignKey("CategoryId, MedicineId,BatchNumber")]
        public Medicine Medicine { get; set; } = null!;
    }
}
