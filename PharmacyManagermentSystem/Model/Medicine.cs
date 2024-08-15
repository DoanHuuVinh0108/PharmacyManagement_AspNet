namespace PharmacyManagermentSystem.Model
{
    public class Medicine
    {
        public string Id { get; set; }
        public string BatchNumber { get; set; }
        public DateOnly ManufacturingDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public string Quantity { get; set; }

    }
}
