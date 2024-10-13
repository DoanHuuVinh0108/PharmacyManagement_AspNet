using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(
        nameof(ReceiptId), 
        nameof(BatchNumber),
        nameof(MedicineId)
        )]
    [Index(nameof(CategoryId), nameof(BatchNumber), nameof(MedicineId),IsUnique =true)]
    public class ReceiptDetail
    {
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }=null!;
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }=null!;
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }
    }
}
