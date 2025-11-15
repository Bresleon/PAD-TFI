using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class agregandounicidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_vehiculos_NumeroChasis",
                table: "vehiculos",
                column: "NumeroChasis",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehiculos_NumeroMotor",
                table: "vehiculos",
                column: "NumeroMotor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patentes_NumeroPatente",
                table: "patentes",
                column: "NumeroPatente",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vehiculos_NumeroChasis",
                table: "vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_vehiculos_NumeroMotor",
                table: "vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_patentes_NumeroPatente",
                table: "patentes");
        }
    }
}
