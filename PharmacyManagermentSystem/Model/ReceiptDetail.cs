using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(
        nameof(ReceiptId), 
        nameof(CategoryId), 
        nameof(BatchNumber),
        nameof(MedicineId)
        )]
    public class ReceiptDetail
    {
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }=null!;
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        [ForeignKey("CategoryId, MedicineId,BatchNumber")]
        public Medicine Medicine { get; set; }=null!;
        public int Quantity { get; set; }
        public int Price { get; set; }


    }
}
