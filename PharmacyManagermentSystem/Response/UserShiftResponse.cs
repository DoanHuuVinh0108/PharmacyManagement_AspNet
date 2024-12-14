namespace PharmacyManagermentSystem.Response
{
    public class UserShiftResponse
    {
        public string EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }
    }
    public class UserShiftByDateResponse
    {
        public string EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
