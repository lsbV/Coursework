using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "Id", "IsCorrect", "TaskId", "Text" },
                values: new object[,]
                {
                    { 11, true, 3, "Apartment" },
                    { 12, true, 3, "Flat" }
                });

            migrationBuilder.InsertData(
                table: "MatchAnswer",
                columns: new[] { "Id", "PartnerId", "Side" },
                values: new object[,]
                {
                    { 12, null, 1 },
                    { 11, 12, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MatchAnswer",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MatchAnswer",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
