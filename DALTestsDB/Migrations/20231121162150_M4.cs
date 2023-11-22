using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MatchAnswer",
                keyColumn: "Id",
                keyValue: 12,
                column: "PartnerId",
                value: 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MatchAnswer",
                keyColumn: "Id",
                keyValue: 12,
                column: "PartnerId",
                value: null);
        }
    }
}
