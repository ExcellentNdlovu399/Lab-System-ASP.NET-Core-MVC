using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class AddExperimentChemicalUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExperimentChemical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperimentId = table.Column<int>(type: "int", nullable: false),
                    ChemicalId = table.Column<int>(type: "int", nullable: false),
                    QuantityUsed = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentChemical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentChemical_Chemicals_ChemicalId",
                        column: x => x.ChemicalId,
                        principalTable: "Chemicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperimentChemical_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentChemical_ChemicalId",
                table: "ExperimentChemical",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentChemical_ExperimentId",
                table: "ExperimentChemical",
                column: "ExperimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperimentChemical");
        }
    }
}
