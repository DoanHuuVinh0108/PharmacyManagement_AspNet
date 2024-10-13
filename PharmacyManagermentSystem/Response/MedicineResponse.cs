namespace PharmacyManagermentSystem.Response
{
    public class MedicineResponse
    {
        public string Id { get; set; }
        public string BatchNumber { get; set; }
        public DateOnly ManufacturingDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string CategoryId { get; set; }
        public int PharmacyId { get; set; }
    }
}
