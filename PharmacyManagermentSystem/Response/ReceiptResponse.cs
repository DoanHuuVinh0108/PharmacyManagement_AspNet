namespace PharmacyManagermentSystem.Response
{
    public class ReceiptResponse
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int SupplierId { get; set; }
        public int PharmacyId { get; set; }
        public string EmployeeId { get; set; }
    }
}
