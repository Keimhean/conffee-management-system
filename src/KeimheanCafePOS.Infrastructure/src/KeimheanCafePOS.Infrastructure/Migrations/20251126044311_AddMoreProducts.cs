using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KeimheanCafePOS.Infrastructure.src.KeimheanCafePOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "ImageUrl", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 23, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1572442388796-11668a67e53d?w=500&q=80", true, "Flat White", 3.80m, null },
                    { 24, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1511920170033-f8396924c348?w=500&q=80", true, "Cortado", 3.30m, null },
                    { 25, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1557006021-b85faa2bc5e2?w=500&q=80", true, "Macchiato", 3.00m, null },
                    { 26, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1578374173703-14c49d07ac86?w=500&q=80", true, "Affogato", 5.50m, null },
                    { 27, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1514066558159-fc8c737ef259?w=500&q=80", true, "Irish Coffee", 6.50m, null },
                    { 28, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1562095241-8c6714fd4178?w=500&q=80", true, "Vietnamese Coffee", 4.80m, null },
                    { 29, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=500&q=80", true, "Nitro Cold Brew", 5.00m, null },
                    { 30, "Coffee", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1583511655857-d19b40a7a54e?w=500&q=80", true, "Spanish Latte", 4.50m, null },
                    { 31, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1544787219-7f47ccb76574?w=500&q=80", true, "Jasmine Tea", 3.00m, null },
                    { 32, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1566316308710-e92d63ac0ddd?w=500&q=80", true, "Oolong Tea", 3.20m, null },
                    { 33, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1571934811356-5cc061b6821f?w=500&q=80", true, "White Tea", 3.50m, null },
                    { 34, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1597481499750-3e6b22637e12?w=500&q=80", true, "Rooibos Tea", 3.00m, null },
                    { 35, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1597318281699-3a108c3d1381?w=500&q=80", true, "Chamomile Tea", 3.00m, null },
                    { 36, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1563822249366-51b66a2e6734?w=500&q=80", true, "Peppermint Tea", 2.80m, null },
                    { 37, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1591255423374-c49cf6cd0db5?w=500&q=80", true, "Thai Iced Tea", 4.00m, null },
                    { 38, "Tea", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1525385133512-2f3bdd039054?w=500&q=80", true, "Bubble Tea", 5.50m, null },
                    { 39, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1623334044303-241021148842?w=500&q=80", true, "Pain au Chocolat", 3.20m, null },
                    { 40, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=500&q=80", true, "Cinnamon Roll", 3.80m, null },
                    { 41, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1586985289688-ca3cf47d3e6e?w=500&q=80", true, "Danish Pastry", 3.50m, null },
                    { 42, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1580743233886-32e4e8f49448?w=500&q=80", true, "Scone", 2.80m, null },
                    { 43, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1593002871058-f8499c6e5d71?w=500&q=80", true, "Banana Bread", 3.20m, null },
                    { 44, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1621303837174-89787a7d4729?w=500&q=80", true, "Carrot Cake", 5.20m, null },
                    { 45, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1586985289565-819ab7c8d03b?w=500&q=80", true, "Red Velvet Cake", 5.80m, null },
                    { 46, "Pastry", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1571877227200-a0d98ea607e9?w=500&q=80", true, "Tiramisu", 6.00m, null },
                    { 47, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1558961363-fa8fdf82db35?w=500&q=80", true, "Oatmeal Cookie", 2.20m, null },
                    { 48, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1603532648955-039310d9ed75?w=500&q=80", true, "Sugar Cookie", 2.00m, null },
                    { 49, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1590080876876-6e7a1bb75d3f?w=500&q=80", true, "Peanut Butter Cookie", 2.30m, null },
                    { 50, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1590772134464-51bb8d4c63ad?w=500&q=80", true, "Biscotti", 2.50m, null },
                    { 51, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1583484908420-29cd4282e6c2?w=500&q=80", true, "Granola Bar", 2.80m, null },
                    { 52, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1481391243133-f96216dcb5d2?w=500&q=80", true, "Chocolate Truffle", 3.80m, null },
                    { 53, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1558636508-e0db3814bd1d?w=500&q=80", true, "Pretzel", 2.50m, null },
                    { 54, "Snack", new DateTime(2025, 11, 23, 9, 39, 43, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=500&q=80", true, "Muffin Chocolate", 3.50m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 54);
        }
    }
}
