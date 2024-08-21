using Microsoft.EntityFrameworkCore;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(TenThuoc), nameof(PrecsriptionId))]
    public class PrescribeMedicine
    {
        public string TenThuoc { get; set; }
        public string SoLuong { get; set; }
        public string PrecsriptionId { get; set; }
        public Prescription Prescription { get; set; } = null!;
    }
}
