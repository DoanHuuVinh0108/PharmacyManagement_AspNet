namespace PharmacyManagermentSystem.Request
{
    public class CreateCategoryRequest
    {
        public string id { get; set; }
        public string tenThuoc { get; set; }
        public double gia { get; set; }
        public string? hoatChat { get; set; } = null;
        public string? phanLoai { get; set; } = null;
        public string? nongDo { get; set; } = null;
        public string? dongGoi { get; set; } = null;   
        public string? taDuoc { get; set; } = null;
        public string? tuoiTho { get; set; } = null;
        public string? congTySx { get; set; } = null;   
        public string? nuocSx { get; set; } = null;
    }
    public class UpdateCategoryRequest
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
    }

}
