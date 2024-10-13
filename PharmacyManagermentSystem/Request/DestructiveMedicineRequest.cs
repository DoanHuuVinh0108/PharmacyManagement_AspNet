namespace PharmacyManagermentSystem.Request
{
    public class CreateDestructiveMedicineRequest
    {
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string EmployeeId { get; set; }
    }
    public class UpdateDestructiveMedicineRequest
    {
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string EmployeeId { get; set; }
    }
    public class DeleteDestructiveMedicineRequest
    {
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
    }
}
