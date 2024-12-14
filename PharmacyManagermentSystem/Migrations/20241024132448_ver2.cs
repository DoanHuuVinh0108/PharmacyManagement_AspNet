using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagermentSystem.Migrations
{
    /// <inheritdoc />
    public partial class ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserShifts_Shifts_Date_NameShift_PharmacyId",
                table: "UserShifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserShifts",
                table: "UserShifts");

            migrationBuilder.DropIndex(
                name: "IX_UserShifts_Date_NameShift_PharmacyId",
                table: "UserShifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "NameShift",
                table: "UserShifts");

            migrationBuilder.DropColumn(
                name: "NameShift",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DestructiveMedicines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserShifts",
                table: "UserShifts",
                columns: new[] { "EmployeeId", "Date", "PharmacyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts",
                columns: new[] { "Date", "PharmacyId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserShifts_Date_PharmacyId",
                table: "UserShifts",
                columns: new[] { "Date", "PharmacyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserShifts_Shifts_Date_PharmacyId",
                table: "UserShifts",
                columns: new[] { "Date", "PharmacyId" },
                principalTable: "Shifts",
                principalColumns: new[] { "Date", "PharmacyId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserShifts_Shifts_Date_PharmacyId",
                table: "UserShifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserShifts",
                table: "UserShifts");

            migrationBuilder.DropIndex(
                name: "IX_UserShifts_Date_PharmacyId",
                table: "UserShifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts");

            migrationBuilder.AddColumn<string>(
                name: "NameShift",
                table: "UserShifts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameShift",
                table: "Shifts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DestructiveMedicines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserShifts",
                table: "UserShifts",
                columns: new[] { "EmployeeId", "Date", "NameShift", "PharmacyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts",
                columns: new[] { "Date", "NameShift", "PharmacyId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserShifts_Date_NameShift_PharmacyId",
                table: "UserShifts",
                columns: new[] { "Date", "NameShift", "PharmacyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserShifts_Shifts_Date_NameShift_PharmacyId",
                table: "UserShifts",
                columns: new[] { "Date", "NameShift", "PharmacyId" },
                principalTable: "Shifts",
                principalColumns: new[] { "Date", "NameShift", "PharmacyId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
