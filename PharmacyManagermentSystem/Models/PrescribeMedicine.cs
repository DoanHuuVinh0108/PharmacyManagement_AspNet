using Microsoft.EntityFrameworkCore;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(MedicineId), nameof(PrecsriptionId))]
    public class PrescribeMedicine
    {

        public string MedicineName { get; set; }
        public string MedicineId { get; set; }
        public int Quantity { get; set; }
        public string PrecsriptionId { get; set; }
        public Prescription Prescription { get; set; } = null!;
    }
}
