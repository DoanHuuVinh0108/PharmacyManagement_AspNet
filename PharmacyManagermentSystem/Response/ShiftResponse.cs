namespace PharmacyManagermentSystem.Response
{
    public class ShiftResponse
    {
        public DateOnly Date { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public int PharmacyId { get; set; }
    }
}
