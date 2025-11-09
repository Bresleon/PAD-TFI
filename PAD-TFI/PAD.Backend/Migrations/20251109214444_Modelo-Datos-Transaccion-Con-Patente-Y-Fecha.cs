using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModeloDatosTransaccionConPatenteYFecha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Fecha",
                table: "transacciones",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "PatenteId",
                table: "transacciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Fecha", "PatenteId" },
                values: new object[] { new DateOnly(2023, 8, 21), 2 });

            migrationBuilder.UpdateData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Fecha", "PatenteId" },
                values: new object[] { new DateOnly(2024, 9, 23), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_transacciones_PatenteId",
                table: "transacciones",
                column: "PatenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_transacciones_patentes_PatenteId",
                table: "transacciones",
                column: "PatenteId",
                principalTable: "patentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transacciones_patentes_PatenteId",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_transacciones_PatenteId",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "PatenteId",
                table: "transacciones");
        }
    }
}
