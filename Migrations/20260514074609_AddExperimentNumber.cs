using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class AddExperimentNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExperimentNumber",
                table: "Experiment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperimentNumber",
                table: "Experiment");
        }
    }
}
