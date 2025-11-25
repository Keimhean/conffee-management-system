using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KeimheanCafePOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CashierName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeGiven = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "ImageUrl", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(5990), "https://images.unsplash.com/photo-1645445644664-8f44112f334c?w=400", true, "Espresso", 2.50m, null },
                    { 2, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6560), "https://images.unsplash.com/photo-1708430651927-20e2e1f1e8f7?w=400", true, "Cappuccino", 3.50m, null },
                    { 3, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6560), "https://images.unsplash.com/photo-1582152747136-af63c112fce5?w=400", true, "Latte", 4.00m, null },
                    { 4, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6560), "https://images.unsplash.com/photo-1669872484166-e11b9638b50e?w=400", true, "Americano", 2.80m, null },
                    { 5, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1618576230663-9714aecfb99a?w=400", true, "Mocha", 4.50m, null },
                    { 6, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1683649197410-c58e48ce4d40?w=400", true, "Iced Latte", 4.20m, null },
                    { 7, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1592663527359-cf6642f54cff?w=400", true, "Iced Mocha", 4.80m, null },
                    { 8, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1681026859292-58c3b2041bfd?w=400", true, "Iced Americano", 3.20m, null },
                    { 9, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?w=400", true, "Iced Caramel Macchiato", 5.20m, null },
                    { 10, "Coffee", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1561641377-f7456d23aa9b?w=400", true, "Cold Brew", 4.20m, null },
                    { 11, "Tea", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1729259586570-639957809271?w=400", true, "Green Tea", 2.50m, null },
                    { 12, "Tea", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1498604636225-6b87a314baa0?w=400", true, "Earl Grey", 2.50m, null },
                    { 13, "Tea", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1578899952107-9c390f1af1b7?w=400", true, "Chai Latte", 3.80m, null },
                    { 14, "Tea", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1515823064-d6e0c04616a7?w=400", true, "Matcha Latte", 4.50m, null },
                    { 15, "Pastry", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1712723247648-64a03ba7c333?w=400", true, "Croissant", 3.00m, null },
                    { 16, "Pastry", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=400", true, "Blueberry Muffin", 3.50m, null },
                    { 17, "Pastry", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1700448293876-07dca826c161?w=400", true, "Chocolate Cake", 5.00m, null },
                    { 18, "Pastry", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1716579866950-54abe7d4286f?w=400", true, "Cheesecake", 5.50m, null },
                    { 19, "Snack", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1657418830273-40c19cfff4d7?w=400", true, "Chocolate Chip Cookie", 2.00m, null },
                    { 20, "Snack", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6570), "https://images.unsplash.com/photo-1606313564200-e75d5e30476c?w=400", true, "Brownie", 3.50m, null },
                    { 21, "Snack", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6580), "https://images.unsplash.com/photo-1732393157398-daaf2efd2f6c?w=400", true, "Macarons", 4.50m, null },
                    { 22, "Snack", new DateTime(2025, 11, 23, 5, 55, 2, 445, DateTimeKind.Utc).AddTicks(6580), "https://images.unsplash.com/photo-1597419765826-5b03fa018c18?w=400", true, "Donut", 2.80m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category",
                table: "Products",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionDate",
                table: "Transactions",
                column: "TransactionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransactionItems");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
