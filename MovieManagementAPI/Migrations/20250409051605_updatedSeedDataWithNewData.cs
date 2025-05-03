using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedSeedDataWithNewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Leonardo DiCaprio");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Joseph Gordon-Levitt");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Sam Neill");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Laura Dern");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "John Travolta");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Uma Thurman");

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "Name" },
                values: new object[] { new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christopher Nolan" });

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "Name" },
                values: new object[] { new DateTime(1946, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Steven Spielberg" });

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BirthDate", "Name" },
                values: new object[] { new DateTime(1963, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quentin Tarantino" });

            migrationBuilder.UpdateData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Genre", "ReleaseDate" },
                values: new object[] { "Sci-Fi", new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Genre", "ReleaseDate" },
                values: new object[] { "Adventure", new DateTime(1993, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Genre", "ReleaseDate" },
                values: new object[] { "Crime", new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Inception");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Jurassic Park");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Pulp Fiction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Actor One");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Actor Two");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Actor Three");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Actor Four");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Actor Five");

            migrationBuilder.UpdateData(
                table: "Casts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Actor Six");

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "Name" },
                values: new object[] { new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director One" });

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "Name" },
                values: new object[] { new DateTime(1980, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director Two" });

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BirthDate", "Name" },
                values: new object[] { new DateTime(1990, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director Three" });

            migrationBuilder.UpdateData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Genre", "ReleaseDate" },
                values: new object[] { "Action", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Genre", "ReleaseDate" },
                values: new object[] { "Drama", new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Genre", "ReleaseDate" },
                values: new object[] { "Comedy", new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Movie One");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Movie Two");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Movie Three");
        }
    }
}
