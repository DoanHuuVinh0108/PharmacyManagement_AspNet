using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagermentSystem.Migrations
{
    /// <inheritdoc />
    public partial class ver4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescribeMedicines",
                table: "PrescribeMedicines");

            migrationBuilder.RenameColumn(
                name: "SoLuong",
                table: "PrescribeMedicines",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "TenThuoc",
                table: "PrescribeMedicines",
                newName: "MedicineName");

            migrationBuilder.AddColumn<string>(
                name: "MedicineId",
                table: "PrescribeMedicines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescribeMedicines",
                table: "PrescribeMedicines",
                columns: new[] { "MedicineId", "PrecsriptionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescribeMedicines",
                table: "PrescribeMedicines");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "PrescribeMedicines");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "PrescribeMedicines",
                newName: "SoLuong");

            migrationBuilder.RenameColumn(
                name: "MedicineName",
                table: "PrescribeMedicines",
                newName: "TenThuoc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescribeMedicines",
                table: "PrescribeMedicines",
                columns: new[] { "TenThuoc", "PrecsriptionId" });
        }
    }
}
