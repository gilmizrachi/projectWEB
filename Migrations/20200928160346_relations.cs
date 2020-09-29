using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectWEB.Migrations
{
    public partial class relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranscationDate = table.Column<DateTime>(nullable: false),
                    SumPrice = table.Column<float>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_RegisteredUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_TransactionId",
                table: "Item",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Transaction_TransactionId",
                table: "Item",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Transaction_TransactionId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Item_TransactionId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Item");
        }
    }
}
