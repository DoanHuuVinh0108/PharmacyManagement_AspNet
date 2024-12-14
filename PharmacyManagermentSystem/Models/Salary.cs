using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(Month), nameof(Year), nameof(EmployeeId))]
    public class Salary
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double BasicSalary { get; set; }
        public double Bonus { get; set; }
        public int DayWorked { get; set; }
        public int DayOff { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } = null!; 
    }
}
