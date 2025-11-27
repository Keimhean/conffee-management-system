using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeimheanCafePOS.Infrastructure.src.KeimheanCafePOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductImagePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1594631252845-29fc4cc8cde9?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1587080266227-677cc2a4e76e?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=500&q=80", "Khmer Iced Tea" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1499636136210-6f4ee915583e?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                column: "ImageUrl",
                value: "https://www.pngall.com/wp-content/uploads/5/Chocolate-Cake-PNG-Free-Download.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45,
                column: "ImageUrl",
                value: "https://pngimg.com/uploads/cake/cake_PNG13097.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                column: "ImageUrl",
                value: "/icons/photo/PeanutButterCookie.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50,
                column: "ImageUrl",
                value: "/icons/photo/Biscotti.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51,
                column: "ImageUrl",
                value: "/icons/photo/GranolaBar.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1566316308710-e92d63ac0ddd?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1597318281699-3a108c3d1381?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1563822249366-51b66a2e6734?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://images.unsplash.com/photo-1591255423374-c49cf6cd0db5?w=500&q=80", "Thai Iced Tea" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1580743233886-32e4e8f49448?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1593002871058-f8499c6e5d71?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1586985289565-819ab7c8d03b?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1590080876876-6e7a1bb75d3f?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1590772134464-51bb8d4c63ad?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1583484908420-29cd4282e6c2?w=500&q=80");
        }
    }
}
