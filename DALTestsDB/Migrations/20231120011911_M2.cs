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
            migrationBuilder.CreateTable(
                name: "ImageBody",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageBody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageBody_Body_Id",
                        column: x => x.Id,
                        principalTable: "Body",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "BodyId", "Description", "Point", "TestId" },
                values: new object[] { 2, 2, "ChooseFromListTask", 10.0, 1 });

            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "Id", "IsCorrect", "TaskId", "Text" },
                values: new object[,]
                {
                    { 5, false, 2, "1" },
                    { 6, false, 2, "2" },
                    { 7, false, 2, "3" },
                    { 8, true, 2, "4" }
                });

            migrationBuilder.InsertData(
                table: "Body",
                columns: new[] { "Id", "TaskId", "Text" },
                values: new object[] { 2, 2, "" });

            migrationBuilder.InsertData(
                table: "ChooseFromListTask",
                column: "Id",
                value: 2);

            migrationBuilder.InsertData(
                table: "ImageBody",
                columns: new[] { "Id", "ImageLength", "ImagePath" },
                values: new object[] { 2, 3993, "/Messenger-icon.png" });

            migrationBuilder.InsertData(
                table: "TextAnswer",
                column: "Id",
                values: new object[]
                {
                    5,
                    6,
                    7,
                    8
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageBody");

            migrationBuilder.DeleteData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChooseFromListTask",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TextAnswer",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
