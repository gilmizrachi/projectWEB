using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectWEB.Migrations
{
    public partial class itemImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pictureUrl",
                table: "Item");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "pictureUrl",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
