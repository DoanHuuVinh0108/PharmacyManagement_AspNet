using System.Security.Policy;

namespace PharmacyManagermentSystem.Request
{
    public class CreateOrderDetailRequest
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
    }
    public class AddOrderDetailRequest
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
    }
    public class UpdateOrderDetailRequest
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public string CategoryId { get; set; }
        public string? NewCategoryId { get; set; } = null;
        public string MedicineId { get; set; }
        public string? NewMedicineId { get; set; } = null;
        public string BatchNumber { get; set; }
        public string? NewBatchNumber { get; set; } = null;
    }
    public class DeleteOrderDetailRequest
    {
        public int OrderId { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
    }
}
