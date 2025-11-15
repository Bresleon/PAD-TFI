using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class transaccionaddexternalreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalReference",
                table: "transacciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExternalReference",
                value: null);

            migrationBuilder.UpdateData(
                table: "transacciones",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExternalReference",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalReference",
                table: "transacciones");
        }
    }
}
