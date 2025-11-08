using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModeloDatosV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Categoria",
                table: "vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TipoTransaccion",
                table: "transacciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Ejemplar",
                table: "patentes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ejemplar",
                value: "A");

            migrationBuilder.UpdateData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ejemplar",
                value: "B");

            migrationBuilder.UpdateData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ejemplar",
                value: "A");

            migrationBuilder.UpdateData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "TipoTransaccion",
                value: "TRANSFERENCIA");

            migrationBuilder.UpdateData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 2,
                column: "TipoTransaccion",
                value: "TRANSFERENCIA");

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Categoria",
                value: "B");

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Categoria",
                value: "B");

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Categoria",
                value: "B");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTransaccion",
                table: "transacciones");

            migrationBuilder.AlterColumn<int>(
                name: "Categoria",
                table: "vehiculos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Ejemplar",
                table: "patentes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ejemplar",
                value: 0);

            migrationBuilder.UpdateData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ejemplar",
                value: 1);

            migrationBuilder.UpdateData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ejemplar",
                value: 0);

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Categoria",
                value: 1);

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Categoria",
                value: 1);

            migrationBuilder.UpdateData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Categoria",
                value: 1);
        }
    }
}
