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
            migrationBuilder.DropIndex(
                name: "IX_MatchAnswer_PartnerId",
                table: "MatchAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "MatchAnswer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MatchAnswer_PartnerId",
                table: "MatchAnswer",
                column: "PartnerId",
                unique: true,
                filter: "[PartnerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchAnswer_PartnerId",
                table: "MatchAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "MatchAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchAnswer_PartnerId",
                table: "MatchAnswer",
                column: "PartnerId");
        }
    }
}
