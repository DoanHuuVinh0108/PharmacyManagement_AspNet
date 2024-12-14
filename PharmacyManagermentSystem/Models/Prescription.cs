using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey("Id")]
    public class Prescription
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string ImageId { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public User Customer { get; set; } = null!;
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; } = null!;
        public ICollection<PrescribeMedicine> PrescribeMedicines { get; set; } = new HashSet<PrescribeMedicine>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();


    }
}
