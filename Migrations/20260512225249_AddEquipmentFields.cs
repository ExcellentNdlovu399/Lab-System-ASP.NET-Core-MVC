using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Equipment");
        }
    }
}
