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
            migrationBuilder.DropForeignKey(
                name: "FK_textAnswer_Answer_Id",
                table: "textAnswer");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_textAnswer",
                table: "textAnswer");

            migrationBuilder.RenameTable(
                name: "textAnswer",
                newName: "TextAnswer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextAnswer",
                table: "TextAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextAnswer_Answer_Id",
                table: "TextAnswer",
                column: "Id",
                principalTable: "Answer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextAnswer_Answer_Id",
                table: "TextAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextAnswer",
                table: "TextAnswer");

            migrationBuilder.RenameTable(
                name: "TextAnswer",
                newName: "textAnswer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_textAnswer",
                table: "textAnswer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GroupUser_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UsersId",
                table: "GroupUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_textAnswer_Answer_Id",
                table: "textAnswer",
                column: "Id",
                principalTable: "Answer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
