using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReactionStepFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_AspNetUsers_ResearcherId",
                table: "Experiment");

            migrationBuilder.AddColumn<string>(
                name: "ReactionEquation",
                table: "ReactionStep",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StepOrder",
                table: "ReactionStep",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StepType",
                table: "ReactionStep",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ResearcherId",
                table: "Experiment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_AspNetUsers_ResearcherId",
                table: "Experiment",
                column: "ResearcherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_AspNetUsers_ResearcherId",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "ReactionEquation",
                table: "ReactionStep");

            migrationBuilder.DropColumn(
                name: "StepOrder",
                table: "ReactionStep");

            migrationBuilder.DropColumn(
                name: "StepType",
                table: "ReactionStep");

            migrationBuilder.AlterColumn<string>(
                name: "ResearcherId",
                table: "Experiment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_AspNetUsers_ResearcherId",
                table: "Experiment",
                column: "ResearcherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
