namespace PharmacyManagermentSystem.Model
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Receipt> Receipts { get; set; } = new HashSet<Receipt>();
    }
}
