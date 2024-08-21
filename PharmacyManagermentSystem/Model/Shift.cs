using Microsoft.EntityFrameworkCore;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(Date), nameof(NameShift),nameof(PharmacyId))]
    public class Shift
    {
        public DateOnly Date { get; set; }
        public string NameShift { get; set; }
        public int Count { get; set; }
        public string Limit { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
        public ICollection<UserShift> UserShifts { get; set; } = new HashSet<UserShift>();
    }
}
