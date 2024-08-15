using System.Reflection.Metadata;

namespace PharmacyManagermentSystem.Model
{
    public class Salary
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int BasicSalary { get; set; }
        public int Bonus { get; set; }
        public int DayWorked { get; set; }
        public int DayOff { get; set; }
    }
}
