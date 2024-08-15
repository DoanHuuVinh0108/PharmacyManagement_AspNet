namespace PharmacyManagermentSystem.Model
{
    public class DestructiveMedicine
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
    }
}
