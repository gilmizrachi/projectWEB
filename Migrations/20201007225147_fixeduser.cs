using Microsoft.EntityFrameworkCore.Migrations;

namespace projectWEB.Migrations
{
    public partial class fixeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecord_Category_CategoryID",
                table: "CategoryRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecord",
                table: "CategoryRecord");

            migrationBuilder.DropColumn(
                name: "MemberType",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "CategoryRecord",
                newName: "CategoryRecords");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecord_CategoryID",
                table: "CategoryRecords",
                newName: "IX_CategoryRecords_CategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecords",
                table: "CategoryRecords",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecords_Category_CategoryID",
                table: "CategoryRecords",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecords_Category_CategoryID",
                table: "CategoryRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecords",
                table: "CategoryRecords");

            migrationBuilder.RenameTable(
                name: "CategoryRecords",
                newName: "CategoryRecord");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecords_CategoryID",
                table: "CategoryRecord",
                newName: "IX_CategoryRecord_CategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecord",
                table: "CategoryRecord",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecord_Category_CategoryID",
                table: "CategoryRecord",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
