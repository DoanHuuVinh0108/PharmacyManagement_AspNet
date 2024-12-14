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
        public double Price { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryId { get; set; }

        [ForeignKey("MedicineId,BatchNumber,CategoryId")]
        public Medicine Medicine { get; set; } = null!;
    }
}
