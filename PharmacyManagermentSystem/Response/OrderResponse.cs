namespace PharmacyManagermentSystem.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int PharmacyId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string? PrescriptionId { get; set; }
    }
}
