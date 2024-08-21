using Microsoft.AspNetCore.Identity;

namespace PharmacyManagermentSystem.Model
{
    public class User : IdentityUser
    {
        public int? PharmacyId { get; set; }
        public Pharmacy? Pharmacy { get; set; }
        public ICollection<Salary> Salaries { get; set; } = new HashSet<Salary>();
        public ICollection<UserShift> UserShifts { get; set; } = new HashSet<UserShift>();
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Receipt> Receipts { get; set; } = new HashSet<Receipt>();
        public ICollection<ReturnSupplier> ReturnSuppliers { get; set; } = new HashSet<ReturnSupplier>();
        public ICollection<DestructiveMedicine> DestructiveMedicines { get; set; } = new HashSet<DestructiveMedicine>();
    }
}
