namespace PharmacyManagermentSystem.Response
{
    public class ReturnSupplierResponse
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
}
