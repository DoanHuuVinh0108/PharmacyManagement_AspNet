namespace PharmacyManagermentSystem.Response
{
    public class PrescriptionResponse
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string ImageId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
    public class PrescriptionByIdResponse
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string ImageId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public IList<PrescribeMedicineResponse> PrescribeMedicines { get; set; }
    }
}
