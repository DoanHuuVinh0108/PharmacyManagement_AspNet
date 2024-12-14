namespace PharmacyManagermentSystem.Response
{
    public class OrderDetailResponse
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
    }
}
