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
}
