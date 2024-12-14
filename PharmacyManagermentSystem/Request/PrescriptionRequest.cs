using PharmacyManagermentSystem.Model;

namespace PharmacyManagermentSystem.Request
{
    public class CreatePrescriptionRequest
    {
        public string Id { get; set; }
        public IFormFile File { get; set; }
        public string CustomerId { get; set; }
        public int DoctorId { get; set; }
        public IList<CreatePrescribeMedicineRequest> Medicines { get; set; } = new List<CreatePrescribeMedicineRequest>();
    }
    public class UpdatePrescriptionRequest
    {
        public IFormFile File { get; set; }
        public string CustomerId { get; set; }
        public int DoctorId { get; set; }
    }
}
