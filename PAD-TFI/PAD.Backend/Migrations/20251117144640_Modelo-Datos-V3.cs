using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModeloDatosV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehiculos_marcas_MarcaId",
                table: "vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_vehiculos_MarcaId",
                table: "vehiculos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "vehiculos");

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "modelos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 1,
                column: "MarcaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 2,
                column: "MarcaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 3,
                column: "MarcaId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 4,
                column: "MarcaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 5,
                column: "MarcaId",
                value: 4);

            migrationBuilder.CreateIndex(
                name: "IX_modelos_MarcaId",
                table: "modelos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_modelos_marcas_MarcaId",
                table: "modelos",
                column: "MarcaId",
                principalTable: "marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_modelos_marcas_MarcaId",
                table: "modelos");

            migrationBuilder.DropIndex(
                name: "IX_modelos_MarcaId",
                table: "modelos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "modelos");

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 1,
                column: "MarcaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 2,
                column: "MarcaId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 3,
                column: "MarcaId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_vehiculos_MarcaId",
                table: "vehiculos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehiculos_marcas_MarcaId",
                table: "vehiculos",
                column: "MarcaId",
                principalTable: "marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
