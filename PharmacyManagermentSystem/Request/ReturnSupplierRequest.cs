using PharmacyManagermentSystem.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Request
{
    public class CreateReturnSupplierRequest
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public int SupplierId { get; set; }
        public string EmployeeId { get; set; }

    }
    public class UpdateReturnSupplierRequest
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public int SupplierId { get; set; }
        public string EmployeeId { get; set; }
    }
    public class DeleteReturnSupplierRequest
    {
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }

    }
}
