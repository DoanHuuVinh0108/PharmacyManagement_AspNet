using System.Diagnostics.SymbolStore;

namespace PharmacyManagermentSystem.Response
{
    public class ReturnSupplierResponse
    {
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double Price { get; set; }
    }
}
