namespace PharmacyManagermentSystem.Request
{
    public class CreateCategoryRequest
    {
        public string id { get; set; }
        public string tenThuoc { get; set; }
        public int gia { get; set; }
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
        public string id { get; set; }
        public string tenThuoc { get; set; }
        public int gia { get; set; }
        public string hoatChat { get; set; }
        public string phanLoai { get; set; }
        public string nongDo { get; set; }
        public string dongGoi { get; set; }
        public string taDuoc { get; set; }
        public string tuoiTho { get; set; }
        public string congTySx { get; set; }
        public string nuocSx { get; set; }
    }

}
