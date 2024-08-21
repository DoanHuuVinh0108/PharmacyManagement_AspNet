using Microsoft.EntityFrameworkCore;

namespace PharmacyManagermentSystem.Model
{
    [PrimaryKey(nameof(Id))]
    public class Category
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }
        public int Price { get; set; }
        public string ActiveIngredient { get; set; }
        public string Classification { get; set; }
        public string Concentration { get; set; }
        public string Packaging { get; set; }
        public string Standards { get; set; }
        public string ShelfLife { get; set; }
        public string Manufacturer { get; set; }
        public string CountryOfManufacture { get; set; }
        public string ExpirationDate { get; set; }
        public ICollection<ImageCategogy> ImageCategogies { get; set; } = new HashSet<ImageCategogy>();
        public ICollection<Medicine> Medicines { get; set; } = new HashSet<Medicine>();
    }
}
