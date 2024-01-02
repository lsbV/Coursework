using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 9,
                column: "TaskId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 10,
                column: "TaskId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 11,
                column: "TaskId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 12,
                column: "TaskId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 13,
                column: "TaskId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 14,
                column: "TaskId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 15,
                column: "TaskId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 16,
                column: "TaskId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "Select the number on the picture");

            migrationBuilder.UpdateData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 4,
                column: "Text",
                value: "Choose a dog and a cat");

            migrationBuilder.UpdateData(
                table: "ImageBody",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageLength", "ImagePath" },
                values: new object[] { 27648, "/number-4.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 9,
                column: "TaskId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 10,
                column: "TaskId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 11,
                column: "TaskId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 12,
                column: "TaskId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 13,
                column: "TaskId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 14,
                column: "TaskId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 15,
                column: "TaskId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 16,
                column: "TaskId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: "");

            migrationBuilder.UpdateData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 4,
                column: "Text",
                value: "");

            migrationBuilder.UpdateData(
                table: "ImageBody",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageLength", "ImagePath" },
                values: new object[] { 3993, "/Messenger-icon.png" });
        }
    }
}
