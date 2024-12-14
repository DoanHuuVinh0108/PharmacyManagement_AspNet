namespace PharmacyManagermentSystem.Request
{
    public class CreateShiftRequest
    {
        public IList<DateOnly> Dates { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public int PharmacyId { get; set; }
    }
    public class UpdateShiftRequest
    {
        public DateOnly Date { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public int PharmacyId { get; set; }
    }
    public class DeleteShiftRequest
    {
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }
    }
}
