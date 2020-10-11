using Microsoft.EntityFrameworkCore.Migrations;

namespace projectWEB.Migrations
{
    public partial class recommendedfixed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlsoTryId",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlsoTry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    registeredUsersid = table.Column<int>(nullable: true),
                    S_Phrase = table.Column<string>(nullable: true),
                    V_ItemNo = table.Column<int>(nullable: false),
                    TransactionId = table.Column<int>(nullable: true),
                    PriceLimits = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlsoTry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlsoTry_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlsoTry_RegisteredUsers_registeredUsersid",
                        column: x => x.registeredUsersid,
                        principalTable: "RegisteredUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_AlsoTryId",
                table: "Item",
                column: "AlsoTryId");

            migrationBuilder.CreateIndex(
                name: "IX_AlsoTry_TransactionId",
                table: "AlsoTry",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_AlsoTry_registeredUsersid",
                table: "AlsoTry",
                column: "registeredUsersid");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AlsoTry_AlsoTryId",
                table: "Item",
                column: "AlsoTryId",
                principalTable: "AlsoTry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_AlsoTry_AlsoTryId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "AlsoTry");

            migrationBuilder.DropIndex(
                name: "IX_Item_AlsoTryId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "AlsoTryId",
                table: "Item");
        }
    }
}
