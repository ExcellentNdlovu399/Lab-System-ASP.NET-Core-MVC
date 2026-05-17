using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class AddExperimentReportSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calculations",
                table: "Experiment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExperimentalData",
                table: "Experiment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FinalResults",
                table: "Experiment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Procedure",
                table: "Experiment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Theory",
                table: "Experiment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calculations",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "ExperimentalData",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "FinalResults",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "Procedure",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "Theory",
                table: "Experiment");
        }
    }
}
