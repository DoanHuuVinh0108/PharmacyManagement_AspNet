using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(EmployeeId), nameof(Date), nameof(PharmacyId))]
    public class UserShift
    {
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }=null!;
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }
        [ForeignKey("Date,PharmacyId")]
        public Shift Shift { get; set; }=null!;

    }
}
