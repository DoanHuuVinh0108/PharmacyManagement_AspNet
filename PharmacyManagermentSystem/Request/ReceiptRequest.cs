using PharmacyManagermentSystem.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Request
{
    public class CreateReceiptRequest
    {
        public DateOnly Date { get; set; }
        public int SupplierId { get; set; }
        public int PharmacyId { get; set; }
        public string EmployeeId { get; set; }
    }
    public class UpdateReceiptRequest
    {
        public DateOnly Date { get; set; }
        public int SupplierId { get; set; }
        public int PharmacyId { get; set; }
        public string EmployeeId { get; set; }
    }
    public class ReceiptRequest
    {
        public int PharmacyId { get; set; }
        public int SupplierId { get; set; }
        public string EmployeeId { get; set; }
        public IList<CategoryByReceipt> Categories { get; set; } = new List<CategoryByReceipt>();
    }

    public class CategoryByReceipt
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public IList<Lot> Lots { get; set; } = new List<Lot>();
    }

    public class Lot
    {
        public string Id { get; set; }
        public DateOnly ManufacturingDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        public string Id { get; set; }
    }

}
