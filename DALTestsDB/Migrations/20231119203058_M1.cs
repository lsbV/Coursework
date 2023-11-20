using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoForTestTaker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassingPercent = table.Column<double>(type: "float", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_Login", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Point = table.Column<double>(type: "float", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    BodyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAssigned",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeToTake = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
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
                name: "UserGroup",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Body",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Body", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Body_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChooseFromListTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChooseFromListTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChooseFromListTask_Task_Id",
                        column: x => x.Id,
                        principalTable: "Task",
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TextAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextAnswer_Answer_Id",
                        column: x => x.Id,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextBody",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextBody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextBody_Body_Id",
                        column: x => x.Id,
                        principalTable: "Body",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTestResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestAssignedUserId = table.Column<int>(type: "int", nullable: false),
                    PassageDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTestResult_TestAssignedUser_TestAssignedUserId",
                        column: x => x.TestAssignedUserId,
                        principalTable: "TestAssignedUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTaskResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    UserTestResultId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaskResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaskResults_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTaskResults_UserTestResult_UserTestResultId",
                        column: x => x.UserTestResultId,
                        principalTable: "UserTestResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswerResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTaskResultId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswerResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswerResults_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswerResults_UserTaskResults_UserTaskResultId",
                        column: x => x.UserTaskResultId,
                        principalTable: "UserTaskResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "CreatedAt", "Description", "IsArchived", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admins group", false, "Admins" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Users group", false, "Users" }
                });

            migrationBuilder.InsertData(
                table: "Test",
                columns: new[] { "Id", "Author", "CreatedAt", "Description", "InfoForTestTaker", "IsArchived", "PassingPercent", "Title" },
                values: new object[] { 1, "Admin", new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1", "Test1", false, 50.0, "Test1" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Description", "FirstName", "IsArchived", "LastName", "Login", "Password", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", false, "Admin", "admin", "�iv�A���M�߱g��s�K��o*�H�", 0 },
                    { 2, new DateTime(2000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User", false, "User", "user", "��m�c��i��� ui��5Hmڲ��[���", 1 },
                    { 3, new DateTime(2000, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User1", true, "User1", "user1", "\n�bʤ��5g���� x}��C=���xʿΐ", 1 },
                    { 4, new DateTime(2000, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User2", false, "User2", "user2", "`%я䊽E�(��eݘ�!�J��a�Ap9�", 1 }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "BodyId", "Description", "Point", "TestId" },
                values: new object[] { 1, 1, "ChooseFromListTask", 10.0, 1 });

            migrationBuilder.InsertData(
                table: "TestAssigned",
                columns: new[] { "Id", "CreatedAt", "EndAt", "IsArchived", "StartAt", "TestId", "TimeToTake" },
                values: new object[] { 1, new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 1, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 }
                });

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
                table: "TestAssignedUser",
                columns: new[] { "Id", "AppointmentDate", "IsActive", "TestAssignedId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 2 },
                    { 2, new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 3 },
                    { 3, new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 4 },
                    { 4, new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 1 }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Answer_TaskId",
                table: "Answer",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Body_TaskId",
                table: "Body",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_TestId",
                table: "Task",
                column: "TestId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswerResults_AnswerId",
                table: "UserAnswerResults",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswerResults_UserTaskResultId",
                table: "UserAnswerResults",
                column: "UserTaskResultId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskResults_TaskId",
                table: "UserTaskResults",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskResults_UserTestResultId",
                table: "UserTaskResults",
                column: "UserTestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestResult_TestAssignedUserId",
                table: "UserTestResult",
                column: "TestAssignedUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChooseFromListTask");

            migrationBuilder.DropTable(
                name: "TextAnswer");

            migrationBuilder.DropTable(
                name: "TextBody");

            migrationBuilder.DropTable(
                name: "UserAnswerResults");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "Body");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "UserTaskResults");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "UserTestResult");

            migrationBuilder.DropTable(
                name: "TestAssignedUser");

            migrationBuilder.DropTable(
                name: "TestAssigned");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
