using Microsoft.EntityFrameworkCore.Migrations;

namespace projectWEB.Migrations
{
    public partial class comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "CommentBody",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentTitle",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "registeredUsersid",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_registeredUsersid",
                table: "Reviews",
                column: "registeredUsersid");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_RegisteredUsers_registeredUsersid",
                table: "Reviews",
                column: "registeredUsersid",
                principalTable: "RegisteredUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_RegisteredUsers_registeredUsersid",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_registeredUsersid",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CommentBody",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CommentTitle",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "registeredUsersid",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
