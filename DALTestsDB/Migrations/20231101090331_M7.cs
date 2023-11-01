using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALTestsDB.Migrations
{
    /// <inheritdoc />
    public partial class M7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "UserTest");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "TestAssigned");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserTest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserTest");

            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "UserTest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "TestAssigned",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
