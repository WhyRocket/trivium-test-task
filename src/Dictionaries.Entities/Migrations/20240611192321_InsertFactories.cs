using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dictionaries.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertFactories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "factory",
                columns: new[] { "id", "name", "region", "year" },
                values: new object[,]
                {
                    { 1, "Завод 1", "Пермский край", 1968 },
                    { 2, "Завод 2", "Татарстан", 2001 },
                    { 3, "Завод 3", "Пермский край", 1998 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "factory",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "factory",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "factory",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
