using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Test",
                columns: new[] { "Id", "Author", "CreatedAt", "Description", "InfoForTestTaker", "IsArchived", "PassingPercent", "Title" },
                values: new object[] { 1, "Admin", new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1", "Test1", false, 50.0, "Test1" });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "BodyId", "Description", "Point", "TestId" },
                values: new object[] { 1, 1, "ChooseFromListTask", 10.0, 1 });

            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "Id", "IsCorrect", "TaskId", "Text" },
                values: new object[,]
                {
                    { 1, false, 1, "1" },
                    { 2, false, 1, "2" },
                    { 3, false, 1, "3" },
                    { 4, true, 1, "4" }
                });

            migrationBuilder.InsertData(
                table: "Body",
                columns: new[] { "Id", "TaskId", "Text" },
                values: new object[] { 1, 1, "2+2 =" });

            migrationBuilder.InsertData(
                table: "ChooseFromListTask",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "TextAnswer",
                column: "Id",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4
                });

            migrationBuilder.InsertData(
                table: "TextBody",
                column: "Id",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChooseFromListTask",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TextBody",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Test",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
