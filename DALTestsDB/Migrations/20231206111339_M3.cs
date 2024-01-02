using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Task");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "ChooseFromListTask");

            migrationBuilder.UpdateData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "ChooseFromListTask");

            migrationBuilder.UpdateData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "MatchTask");
        }
    }
}
