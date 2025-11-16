using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAD.Backend.Migrations
{
    /// <inheritdoc />
    public partial class agregandocuil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cuil",
                table: "titulares",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "titulares",
                keyColumn: "Id",
                keyValue: 1,
                column: "Cuil",
                value: null);

            migrationBuilder.UpdateData(
                table: "titulares",
                keyColumn: "Id",
                keyValue: 2,
                column: "Cuil",
                value: null);

            migrationBuilder.UpdateData(
                table: "titulares",
                keyColumn: "Id",
                keyValue: 3,
                column: "Cuil",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cuil",
                table: "titulares");
        }
    }
}
