using PharmacyManagermentSystem.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Request
{
    public class CreateUserShiftRequest
    {
        public string EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }
    }
    public class UpdateUserShiftRequest
    {
        public string EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }
        public string NewEmployeeId { get; set; }
        public string NewNameShift { get; set; }
        public DateOnly NewDate { get; set; }
        public int NewPharmacyId { get; set; }
    }
    public class DeleteUserShiftRequest
    {
        public string EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public int PharmacyId { get; set; }

    }
}
