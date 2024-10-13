namespace PharmacyManagermentSystem.Response
{
    public class UserShiftResponse
    {
        public string EmployeeId { get; set; }
        public string NameShift { get; set; }
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }
    }
}
