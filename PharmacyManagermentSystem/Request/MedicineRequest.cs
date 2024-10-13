namespace PharmacyManagermentSystem.Request
{
    public class CreateMedicineRequest
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
    public class UpdateMedicineRequest
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
    public class DeleteMedicineRequest
    {
        public string Id { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryId { get; set; }
    }
}
