using PharmacyManagermentSystem.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Response
{
    public class DestructiveMedicineResponse
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public int PharmacyId { get; set; }
        public string CategoryId { get; set; }
        public string MedicineId { get; set; }
        public string BatchNumber { get; set; }
        public string EmployeeId { get; set; }
    }
}
