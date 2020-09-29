using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectWEB.Migrations
{
    public partial class itemimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ItemImage",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemImage",
                table: "Item");
        }
    }
}
