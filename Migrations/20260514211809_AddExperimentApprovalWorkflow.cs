using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class AddExperimentApprovalWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                table: "Experiment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Experiment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Experiment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorComment",
                table: "Experiment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_ApprovedById",
                table: "Experiment",
                column: "ApprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_AspNetUsers_ApprovedById",
                table: "Experiment",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_AspNetUsers_ApprovedById",
                table: "Experiment");

            migrationBuilder.DropIndex(
                name: "IX_Experiment_ApprovedById",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "SupervisorComment",
                table: "Experiment");
        }
    }
}
