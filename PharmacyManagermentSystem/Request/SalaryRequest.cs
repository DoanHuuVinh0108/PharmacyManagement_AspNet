namespace PharmacyManagermentSystem.Request
{
    public class CreateSalaryRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double BasicSalary { get; set; }
        public double Bonus { get; set; }
        public int DayWorked { get; set; }
        public int DayOff { get; set; }
        public string EmployeeId { get; set; }
    }
    public class UpdateSalaryRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double BasicSalary { get; set; }
        public double Bonus { get; set; }
        public int DayWorked { get; set; }
        public int DayOff { get; set; }
        public string EmployeeId { get; set; }
    }
    public class DeleteSalaryRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string EmployeeId { get; set; }
    }
}
