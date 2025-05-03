using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRedundantCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Casts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Casts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 1,
                column: "MovieId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 2,
                column: "MovieId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 3,
                column: "MovieId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 4,
                column: "MovieId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 5,
                column: "MovieId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 6,
                column: "MovieId",
                value: 0);
        }
    }
}
