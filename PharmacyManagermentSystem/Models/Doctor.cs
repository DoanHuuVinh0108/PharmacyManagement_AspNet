namespace PharmacyManagermentSystem.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
    }
}
