using Microsoft.EntityFrameworkCore.Migrations;

namespace api_imdb.Migrations.ApplicationDb
{
    public partial class RenameUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsersActives");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersActives",
                table: "UsersActives",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersActives",
                table: "UsersActives");

            migrationBuilder.RenameTable(
                name: "UsersActives",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
