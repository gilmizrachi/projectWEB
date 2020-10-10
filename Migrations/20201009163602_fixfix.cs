using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectWEB.Migrations
{
    public partial class fixfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecords_Category_CategoryID",
                table: "CategoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Promo_PromoID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Order_OrderID",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Item_ProductID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPicture_Item_ItemID",
                table: "ProductPicture");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPicture_Picture_PictureID",
                table: "ProductPicture");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecification_ProductRecord_ProductRecordID",
                table: "ProductSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Picture_PictureID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ProductRecord");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promo",
                table: "Promo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSpecification",
                table: "ProductSpecification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPicture",
                table: "ProductPicture");

            migrationBuilder.DropIndex(
                name: "IX_ProductPicture_ItemID",
                table: "ProductPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Picture",
                table: "Picture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHistory",
                table: "OrderHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "ProductPicture");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "item_id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "item_quantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Promo",
                newName: "Promos");

            migrationBuilder.RenameTable(
                name: "ProductSpecification",
                newName: "ProductSpecifications");

            migrationBuilder.RenameTable(
                name: "ProductPicture",
                newName: "ProductPictures");

            migrationBuilder.RenameTable(
                name: "Picture",
                newName: "Pictures");

            migrationBuilder.RenameTable(
                name: "OrderHistory",
                newName: "OrderHistories");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_Promo_Code",
                table: "Promos",
                newName: "IX_Promos_Code");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSpecification_ProductRecordID",
                table: "ProductSpecifications",
                newName: "IX_ProductSpecifications_ProductRecordID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPicture_PictureID",
                table: "ProductPictures",
                newName: "IX_ProductPictures_PictureID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHistory_OrderID",
                table: "OrderHistories",
                newName: "IX_OrderHistories_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PromoID",
                table: "Orders",
                newName: "IX_Orders_PromoID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Locations",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "CustomerID",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promos",
                table: "Promos",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSpecifications",
                table: "ProductSpecifications",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPictures",
                table: "ProductPictures",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHistories",
                table: "OrderHistories",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    ParentCategoryID = table.Column<int>(nullable: true),
                    isFeatured = table.Column<bool>(nullable: false),
                    SanitizedName = table.Column<string>(nullable: true),
                    DisplaySeqNo = table.Column<int>(nullable: false),
                    PictureID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ConfigurationType = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "NewsletterSubscriptions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterSubscriptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    EntityID = table.Column<int>(nullable: false),
                    RecordID = table.Column<int>(nullable: false),
                    LanguageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    Discount = table.Column<decimal>(nullable: true),
                    Cost = table.Column<decimal>(nullable: true),
                    isFeatured = table.Column<bool>(nullable: false),
                    ThumbnailPictureID = table.Column<int>(nullable: false),
                    SKU = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Barcode = table.Column<string>(nullable: true),
                    Supplier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRecords",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ProductID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductRecords_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_ProductID",
                table: "ProductPictures",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PictureID",
                table: "Categories",
                column: "PictureID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecords_ProductID",
                table: "ProductRecords",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecords_Categories_CategoryID",
                table: "CategoryRecords",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_Orders_OrderID",
                table: "OrderHistories",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderID",
                table: "OrderItem",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductID",
                table: "OrderItem",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Promos_PromoID",
                table: "Orders",
                column: "PromoID",
                principalTable: "Promos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_Pictures_PictureID",
                table: "ProductPictures",
                column: "PictureID",
                principalTable: "Pictures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_Products_ProductID",
                table: "ProductPictures",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecifications_ProductRecords_ProductRecordID",
                table: "ProductSpecifications",
                column: "ProductRecordID",
                principalTable: "ProductRecords",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pictures_PictureID",
                table: "Users",
                column: "PictureID",
                principalTable: "Pictures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecords_Categories_CategoryID",
                table: "CategoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_Orders_OrderID",
                table: "OrderHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Promos_PromoID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_Pictures_PictureID",
                table: "ProductPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_Products_ProductID",
                table: "ProductPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecifications_ProductRecords_ProductRecordID",
                table: "ProductSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pictures_PictureID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "NewsletterSubscriptions");

            migrationBuilder.DropTable(
                name: "ProductRecords");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promos",
                table: "Promos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSpecifications",
                table: "ProductSpecifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPictures",
                table: "ProductPictures");

            migrationBuilder.DropIndex(
                name: "IX_ProductPictures_ProductID",
                table: "ProductPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHistories",
                table: "OrderHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Promos",
                newName: "Promo");

            migrationBuilder.RenameTable(
                name: "ProductSpecifications",
                newName: "ProductSpecification");

            migrationBuilder.RenameTable(
                name: "ProductPictures",
                newName: "ProductPicture");

            migrationBuilder.RenameTable(
                name: "Pictures",
                newName: "Picture");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderHistories",
                newName: "OrderHistory");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_Promos_Code",
                table: "Promo",
                newName: "IX_Promo_Code");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSpecifications_ProductRecordID",
                table: "ProductSpecification",
                newName: "IX_ProductSpecification_ProductRecordID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPictures_PictureID",
                table: "ProductPicture",
                newName: "IX_ProductPicture_PictureID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PromoID",
                table: "Order",
                newName: "IX_Order_PromoID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHistories_OrderID",
                table: "OrderHistory",
                newName: "IX_OrderHistory_OrderID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Location",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "ProductPicture",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "item_id",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "item_quantity",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promo",
                table: "Promo",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSpecification",
                table: "ProductSpecification",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPicture",
                table: "ProductPicture",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Picture",
                table: "Picture",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHistory",
                table: "OrderHistory",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplaySeqNo = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentCategoryID = table.Column<int>(type: "int", nullable: true),
                    PictureID = table.Column<int>(type: "int", nullable: true),
                    SanitizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isFeatured = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Picture_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Picture",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ItemDevision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailPictureID = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    isFeatured = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRecord",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: true),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductRecord_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPicture_ItemID",
                table: "ProductPicture",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryID",
                table: "Category",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_PictureID",
                table: "Category",
                column: "PictureID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryID",
                table: "Item",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecord_ItemID",
                table: "ProductRecord",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecords_Category_CategoryID",
                table: "CategoryRecords",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Promo_PromoID",
                table: "Order",
                column: "PromoID",
                principalTable: "Promo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Order_OrderID",
                table: "OrderHistory",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderID",
                table: "OrderItem",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Item_ProductID",
                table: "OrderItem",
                column: "ProductID",
                principalTable: "Item",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPicture_Item_ItemID",
                table: "ProductPicture",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPicture_Picture_PictureID",
                table: "ProductPicture",
                column: "PictureID",
                principalTable: "Picture",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecification_ProductRecord_ProductRecordID",
                table: "ProductSpecification",
                column: "ProductRecordID",
                principalTable: "ProductRecord",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Picture_PictureID",
                table: "Users",
                column: "PictureID",
                principalTable: "Picture",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
