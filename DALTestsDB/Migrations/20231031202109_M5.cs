using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK_UserTest_Test_TestId",
                table: "UserTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTest_User_UserId",
                table: "UserTest");

            migrationBuilder.DropIndex(
                name: "IX_UserTest_TestId",
                table: "UserTest");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "UserTest");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTest",
                newName: "TestAssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTest_UserId",
                table: "UserTest",
                newName: "IX_UserTest_TestAssignedUserId");

            migrationBuilder.CreateTable(
                name: "TestAssigned",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAssigned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAssigned_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAssignedUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestAssignedId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAssignedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAssignedUser_TestAssigned_TestAssignedId",
                        column: x => x.TestAssignedId,
                        principalTable: "TestAssigned",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAssignedUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAssigned_TestId",
                table: "TestAssigned",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAssignedUser_TestAssignedId",
                table: "TestAssignedUser",
                column: "TestAssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAssignedUser_UserId",
                table: "TestAssignedUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTest_TestAssignedUser_TestAssignedUserId",
                table: "UserTest",
                column: "TestAssignedUserId",
                principalTable: "TestAssignedUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTest_TestAssignedUser_TestAssignedUserId",
                table: "UserTest");

            migrationBuilder.DropTable(
                name: "TestAssignedUser");

            migrationBuilder.DropTable(
                name: "TestAssigned");

            migrationBuilder.RenameColumn(
                name: "TestAssignedUserId",
                table: "UserTest",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTest_TestAssignedUserId",
                table: "UserTest",
                newName: "IX_UserTest_UserId");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "UserTest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserTest_TestId",
                table: "UserTest",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTest_Test_TestId",
                table: "UserTest",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTest_User_UserId",
                table: "UserTest",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
