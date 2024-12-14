using PharmacyManagermentSystem.Request;
namespace PharmacyManagermentSystem.Response
{
    public class ReceiptResponse
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int PharmacyId { get; set; }
        public string NamePharmacy { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
    public class ReceiptDetailResponseById
    {
        public string NamePharmacy { get; set; }
        public string SupplierName { get; set; }
        public string EmployeeName { get; set; }
        public DateOnly Date { get; set; }
        public IList<CategoryByReceipt> Categories { get; set; } = new List<CategoryByReceipt>();
    }

}
