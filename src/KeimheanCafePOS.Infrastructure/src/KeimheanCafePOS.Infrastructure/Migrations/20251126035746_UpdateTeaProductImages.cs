using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeimheanCafePOS.Infrastructure.src.KeimheanCafePOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeaProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1564890369478-c89ca6d9cde9?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1515823064-d6e0c04616a7?w=500&q=80");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1597318281699-3a108c3d1381?w=500&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1536013564249-9c83f0f97155?w=500&q=80");
        }
    }
}
