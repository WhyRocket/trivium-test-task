using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dictionaries.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { 1, "G-001", "Гвозди" },
                    { 2, "SH-20", "Шурупы" },
                    { 3, "SA-19", "Саморезы" },
                    { 4, "B-100", "Болты" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
