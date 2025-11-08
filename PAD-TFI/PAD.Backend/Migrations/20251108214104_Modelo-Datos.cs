using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PAD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModeloDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "marcas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Ford" },
                    { 3, "Chevrolet" },
                    { 4, "Volkswagen" }
                });

            migrationBuilder.InsertData(
                table: "modelos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Corolla" },
                    { 2, "Hilux" },
                    { 3, "Focus" },
                    { 4, "Onix" },
                    { 5, "Gol" }
                });

            migrationBuilder.InsertData(
                table: "titulares",
                columns: new[] { "Id", "Apellido", "Dni", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Pérez", "30123456", "juan.perez@email.com", "Juan", "1123456789" },
                    { 2, "Gómez", "28999888", "maria.gomez@email.com", "María", "1133344455" },
                    { 3, "Ramírez", "27111222", "carlos.ramirez@email.com", "Carlos", "1145566778" }
                });

            migrationBuilder.InsertData(
                table: "transacciones",
                columns: new[] { "Id", "Costo", "TitularDestinoId", "TitularOrigenId" },
                values: new object[,]
                {
                    { 1, 12500m, 2, 1 },
                    { 2, 11000m, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "vehiculos",
                columns: new[] { "Id", "Categoria", "FechaFabricacion", "MarcaId", "ModeloId", "NumeroChasis", "NumeroMotor", "Precio" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2018, 5, 10), 1, 1, "CHS-TOY-001", "MOT-TOY-001", 15000m },
                    { 2, 1, new DateOnly(2019, 8, 15), 2, 3, "CHS-FRD-002", "MOT-FRD-002", 13000m },
                    { 3, 1, new DateOnly(2020, 3, 20), 3, 4, "CHS-CHV-003", "MOT-CHV-003", 11000m }
                });

            migrationBuilder.InsertData(
                table: "patentes",
                columns: new[] { "Id", "Ejemplar", "FechaEmision", "NumeroPatente", "TitularId", "VehiculoId" },
                values: new object[,]
                {
                    { 1, 0, new DateOnly(2018, 6, 1), "AB123CD", 1, 1 },
                    { 2, 1, new DateOnly(2019, 9, 1), "AC987XY", 2, 2 },
                    { 3, 0, new DateOnly(2020, 4, 10), "AE555GH", 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "marcas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "patentes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "titulares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "titulares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "titulares",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "vehiculos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "marcas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "marcas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "marcas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "modelos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "vehiculos");
        }
    }
}
