namespace PharmacyManagermentSystem.Response
{
    public class ReceiptDetailResponse
    {
        public int ReceiptId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
    }
}
