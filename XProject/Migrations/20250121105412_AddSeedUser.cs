using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "Password", "Role" },
                values: new object[] { new Guid("98b6f092-42b6-409a-9999-1ba51cd51aad"), new DateTime(2025, 1, 21, 10, 54, 11, 672, DateTimeKind.Utc).AddTicks(6123), "julugbek023@gmail.com", "Ulug'bek", true, "Jumaboyev", "Qwerty1904", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98b6f092-42b6-409a-9999-1ba51cd51aad"));
        }
    }
}
