namespace PharmacyManagermentSystem.Request
{
    public class CreatePrescribeMedicineRequest
    {
        public string MedicineName { get; set; }
        public string MedicineId { get; set; }
        public int Quantity { get; set; }
        public string PrecsriptionId { get; set; }
    }
    public class UpdatePrescribeMedicineRequest
    {
        public string MedicineName { get; set; }
        public string MedicineId { get; set; }
        public int Quantity { get; set; }
        public string PrecsriptionId { get; set; }
    }
    public class DeletePrescribeMedicineRequest
    {
        public string MedicineId { get; set; }
        public string PrecsriptionId { get; set; }
    }
}
