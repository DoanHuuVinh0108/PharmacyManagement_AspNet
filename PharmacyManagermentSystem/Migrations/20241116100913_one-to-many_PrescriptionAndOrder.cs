using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagermentSystem.Migrations
{
    /// <inheritdoc />
    public partial class onetomany_PrescriptionAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_PrescriptionId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrescriptionId",
                table: "Orders",
                column: "PrescriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_PrescriptionId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrescriptionId",
                table: "Orders",
                column: "PrescriptionId",
                unique: true);
        }
    }
}
