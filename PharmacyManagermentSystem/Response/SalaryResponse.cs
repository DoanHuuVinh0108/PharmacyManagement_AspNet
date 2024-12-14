namespace PharmacyManagermentSystem.Response
{
    public class SalaryResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double BasicSalary { get; set; }
        public double Bonus { get; set; }
        public int DayWorked { get; set; }
        public int DayOff { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
    public class GetSalaryResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double BasicSalary { get; set; }
        public double Bonus { get; set; }
        public int DayWorked { get; set; }
        public int DayOff { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }

    }
}
