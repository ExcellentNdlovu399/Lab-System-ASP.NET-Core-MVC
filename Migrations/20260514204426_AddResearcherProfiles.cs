using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class AddResearcherProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "ResearcherProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedDate",
                table: "ResearcherProfile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LabGroup",
                table: "ResearcherProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ResearcherProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "ResearcherProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResearchArea",
                table: "ResearcherProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "ResearcherProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "ResearcherProfile");

            migrationBuilder.DropColumn(
                name: "JoinedDate",
                table: "ResearcherProfile");

            migrationBuilder.DropColumn(
                name: "LabGroup",
                table: "ResearcherProfile");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ResearcherProfile");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "ResearcherProfile");

            migrationBuilder.DropColumn(
                name: "ResearchArea",
                table: "ResearcherProfile");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "ResearcherProfile");
        }
    }
}
