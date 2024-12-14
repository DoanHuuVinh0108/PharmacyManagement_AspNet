namespace PharmacyManagermentSystem.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateOnly Date { get; set; }
        public string? PrescriptionId { get; set; }
        public double TotalPrice { get; set; }
    }
    public class  OrderDetailByIdResponse
    {
        public OrderResponse order { get; set; }
        public IList<OrderDetailResponse> orderDetails { get; set; }
    }
}
