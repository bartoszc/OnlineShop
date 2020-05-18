using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Backend.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tCards",
                columns: table => new
                {
                    CardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(maxLength: 25, nullable: false),
                    NameOnCard = table.Column<string>(maxLength: 50, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CVV = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tCards_CardID", x => x.CardID);
                },
                comment: "Contains info about cards.");

            migrationBuilder.CreateTable(
                name: "tCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 25, nullable: false),
                    CategoryDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tCategories_CategoryID", x => x.CategoryID);
                },
                comment: "Contains info about categories of products.");

            migrationBuilder.CreateTable(
                name: "tPromotions",
                columns: table => new
                {
                    PromoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountValue = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tPromotions_PromoID", x => x.PromoID);
                },
                comment: "Contains information about current promotions.");

            migrationBuilder.CreateTable(
                name: "tUsers",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pass = table.Column<string>(maxLength: 12, nullable: false),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false),
                    LastName = table.Column<string>(maxLength: 25, nullable: false),
                    UserType = table.Column<string>(maxLength: 25, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 11, nullable: true),
                    Company = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Address1 = table.Column<string>(unicode: false, maxLength: 120, nullable: false),
                    City = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tUsers_UserID", x => x.UserID);
                },
                comment: "Contains info about single user.");

            migrationBuilder.CreateTable(
                name: "tProducts",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ImageColumn = table.Column<byte[]>(nullable: true),
                    ProductDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tProducts_ProductID", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_tProducts_tCategories",
                        column: x => x.CategoryID,
                        principalTable: "tCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Contains info about products in shop.");

            migrationBuilder.CreateTable(
                name: "tOrders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ShippingAddress = table.Column<string>(maxLength: 100, nullable: false),
                    ShippingMethod = table.Column<string>(maxLength: 25, nullable: false),
                    OrderStatus = table.Column<string>(maxLength: 25, nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tOrders_OrderID", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_tOrders_Users",
                        column: x => x.UserID,
                        principalTable: "tUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Contains info about all orders.");

            migrationBuilder.CreateTable(
                name: "tOrderDetails",
                columns: table => new
                {
                    OrderDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    OrderCost = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PromoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tOrderDetails_OrderDetailsID", x => x.OrderDetailsID);
                    table.ForeignKey(
                        name: "FK_tOrderDetails_Orders",
                        column: x => x.OrderID,
                        principalTable: "tOrders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tOrderDetails_Products",
                        column: x => x.ProductID,
                        principalTable: "tProducts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tOrderDetails_Promotions",
                        column: x => x.PromoID,
                        principalTable: "tPromotions",
                        principalColumn: "PromoID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Contains info about single order.");

            migrationBuilder.CreateIndex(
                name: "AK_tCategories_CategorytName",
                table: "tCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tOrderDetails_OrderID",
                table: "tOrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_tOrderDetails_ProductID",
                table: "tOrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tOrderDetails_PromoID",
                table: "tOrderDetails",
                column: "PromoID");

            migrationBuilder.CreateIndex(
                name: "IX_tOrders_UserID",
                table: "tOrders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tProducts_CategoryID",
                table: "tProducts",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "AK_tProducts_ProductName",
                table: "tProducts",
                column: "ProductName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tCards");

            migrationBuilder.DropTable(
                name: "tOrderDetails");

            migrationBuilder.DropTable(
                name: "tOrders");

            migrationBuilder.DropTable(
                name: "tProducts");

            migrationBuilder.DropTable(
                name: "tPromotions");

            migrationBuilder.DropTable(
                name: "tUsers");

            migrationBuilder.DropTable(
                name: "tCategories");
        }
    }
}
