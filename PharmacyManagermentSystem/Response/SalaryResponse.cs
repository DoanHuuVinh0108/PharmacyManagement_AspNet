namespace PharmacyManagermentSystem.Response
{
    public class SalaryResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int BasicSalary { get; set; }
        public int Bonus { get; set; }
        public int DayWorked { get; set; }
        public int DayOff { get; set; }
        public string EmployeeId { get; set; }
    }
}
