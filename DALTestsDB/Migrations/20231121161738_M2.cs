using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MatchAnswer",
                keyColumn: "Id",
                keyValue: 10,
                column: "PartnerId",
                value: 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MatchAnswer",
                keyColumn: "Id",
                keyValue: 10,
                column: "PartnerId",
                value: null);
        }
    }
}
