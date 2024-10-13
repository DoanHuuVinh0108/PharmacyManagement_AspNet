namespace PharmacyManagermentSystem.Request
{
    public class CreatePrescribeMedicineRequest
    {
        public string TenThuoc { get; set; }
        public int SoLuong { get; set; }
        public string PrecsriptionId { get; set; }
    }
    public class UpdatePrescribeMedicineRequest
    {
        public string TenThuoc { get; set; }
        public int SoLuong { get; set; }
        public string PrecsriptionId { get; set; }
    }
    public class DeletePrescribeMedicineRequest
    {
        public string TenThuoc { get; set; }
        public string PrecsriptionId { get; set; }
    }
}
