using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnterTextAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterTextAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterTextAnswer_Answer_Id",
                        column: x => x.Id,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnterTextTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterTextTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterTextTask_Task_Id",
                        column: x => x.Id,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleSelectTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleSelectTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleSelectTask_Task_Id",
                        column: x => x.Id,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "Id", "IsCorrect", "TaskId", "Text" },
                values: new object[,]
                {
                    { 9, true, 2, "" },
                    { 10, false, 2, "" },
                    { 11, false, 2, "" },
                    { 12, false, 2, "" },
                    { 13, false, 3, "1" },
                    { 14, false, 3, "2" },
                    { 15, false, 3, "3" },
                    { 16, true, 3, "4" }
                });

            migrationBuilder.UpdateData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "2 + 2 =");

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "BodyId", "Point", "TestId" },
                values: new object[,]
                {
                    { 4, 4, 10.0, 1 },
                    { 5, 5, 10.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Body",
                columns: new[] { "Id", "TaskId", "Text" },
                values: new object[,]
                {
                    { 4, 4, "" },
                    { 5, 5, "3 + 1 =" }
                });

            migrationBuilder.InsertData(
                table: "EnterTextAnswer",
                column: "Id",
                values: new object[]
                {
                    13,
                    14,
                    15,
                    16
                });

            migrationBuilder.InsertData(
                table: "EnterTextTask",
                column: "Id",
                value: 5);

            migrationBuilder.InsertData(
                table: "ImageAnswer",
                columns: new[] { "Id", "ImageLength", "ImagePath" },
                values: new object[,]
                {
                    { 9, 20657, "/bird.png" },
                    { 10, 38024, "/cat.png" },
                    { 11, 38439, "/dog.png" },
                    { 12, 30455, "/clown-fish.png" }
                });

            migrationBuilder.InsertData(
                table: "MultipleSelectTask",
                column: "Id",
                value: 4);

            migrationBuilder.InsertData(
                table: "ImageBody",
                columns: new[] { "Id", "ImageLength", "ImagePath" },
                values: new object[] { 4, 24567, "/forest.png" });

            migrationBuilder.InsertData(
                table: "TextBody",
                column: "Id",
                value: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterTextAnswer");

            migrationBuilder.DropTable(
                name: "EnterTextTask");

            migrationBuilder.DropTable(
                name: "MultipleSelectTask");

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ImageAnswer",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ImageAnswer",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ImageAnswer",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ImageAnswer",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ImageBody",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TextBody",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Body",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
                value: "2+2 =");
        }
    }
}
