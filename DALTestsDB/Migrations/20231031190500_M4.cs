using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_UserTest_UserTestId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "MaxPoints",
                table: "Test");

            migrationBuilder.RenameColumn(
                name: "Passed",
                table: "UserTest",
                newName: "IsPassed");

            migrationBuilder.RenameColumn(
                name: "MinPoints",
                table: "Test",
                newName: "PassingPercent");

            migrationBuilder.AddColumn<bool>(
                name: "IsMissed",
                table: "UserTest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Result",
                table: "UserTest",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Test",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Test",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTestId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    IsMissed = table.Column<bool>(type: "bit", nullable: false),
                    TaskGrade = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTask_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTask_UserTest_UserTestId",
                        column: x => x.UserTestId,
                        principalTable: "UserTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_TaskId",
                table: "UserTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_UserTestId",
                table: "UserTask",
                column: "UserTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_UserTask_UserTestId",
                table: "UserAnswer",
                column: "UserTestId",
                principalTable: "UserTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_UserTask_UserTestId",
                table: "UserAnswer");

            migrationBuilder.DropTable(
                name: "UserTask");

            migrationBuilder.DropColumn(
                name: "IsMissed",
                table: "UserTest");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "UserTest");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Test");

            migrationBuilder.RenameColumn(
                name: "IsPassed",
                table: "UserTest",
                newName: "Passed");

            migrationBuilder.RenameColumn(
                name: "PassingPercent",
                table: "Test",
                newName: "MinPoints");

            migrationBuilder.AddColumn<double>(
                name: "MaxPoints",
                table: "Test",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_UserTest_UserTestId",
                table: "UserAnswer",
                column: "UserTestId",
                principalTable: "UserTest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
