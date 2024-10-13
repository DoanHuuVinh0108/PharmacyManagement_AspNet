namespace PharmacyManagermentSystem.Request
{
    public class CreateOrderRequest
    {
        public int PharmacyId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string? PrescriptionId { get; set; } = null;
        public string? Status { get; set; } = null;
    }
    public class UpdateOrderRequest
    {
        public string Status { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public int PharmacyId { get; set; }
        public string? PrescriptionId { get; set; } = null;
    }

}
