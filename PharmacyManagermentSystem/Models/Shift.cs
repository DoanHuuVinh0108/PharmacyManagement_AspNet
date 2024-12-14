using Microsoft.EntityFrameworkCore;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(Date), nameof(PharmacyId))]
    public class Shift
    {
        public DateOnly Date { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
        public ICollection<UserShift> UserShifts { get; set; } = new HashSet<UserShift>();
    }
}
