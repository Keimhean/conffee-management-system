using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeimheanCafePOS.Infrastructure.src.KeimheanCafePOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStaffPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "10176e7b7b24d317acfcf8d2064cfd2f24e154f7b5a96603077d5ef813d6a6b6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "9f735e0df9a1ddc702bf0a1a7b83033f9f7153a00c29de82cedadc9957289b05");
        }
    }
}
