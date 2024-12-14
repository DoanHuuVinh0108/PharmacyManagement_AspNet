using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagermentSystem.Migrations
{
    /// <inheritdoc />
    public partial class ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiptDetails",
                table: "ReceiptDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiptDetails",
                table: "ReceiptDetails",
                columns: new[] { "ReceiptId", "BatchNumber", "MedicineId", "CategoryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiptDetails",
                table: "ReceiptDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiptDetails",
                table: "ReceiptDetails",
                columns: new[] { "ReceiptId", "BatchNumber", "MedicineId" });
        }
    }
}
