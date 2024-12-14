namespace PharmacyManagermentSystem.Response
{
    public class CategoryResponse
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }
        public double Price { get; set; }
        public string ActiveIngredient { get; set; }
        public string Classification { get; set; }
        public string Concentration { get; set; }
        public string Packaging { get; set; }
        public string Assistantherb { get; set; }
        public string ShelfLife { get; set; }
        public string Manufacturer { get; set; }
        public string CountryOfManufacture { get; set; }
        public int Quantity { get; set; }
    }

    public class FindResponse
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }
        public double Price { get; set; }
    }
    
}
