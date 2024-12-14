using PharmacyManagermentSystem.Model;
using System.Drawing;

namespace PharmacyManagermentSystem.Request
{
    public class CreateReceiptDetailRequest
    {
        public int ReceiptId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
    }
    public class UpdateReceiptDetailRequest
    {
        public int ReceiptId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryId { get; set; }
        public string NewCategoryId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
    }
    public class DeleteReceiptDetailRequest
    {
        public int ReceiptId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryId { get; set; }
    }
}
