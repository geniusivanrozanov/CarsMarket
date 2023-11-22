using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_default_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5ebc53e8-7d15-48d7-abb1-6102ea4d7ea9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("659d04c4-30dc-4729-9539-877c56dcfb9c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("675a3309-29e3-49d3-9290-8df40f0cb09d"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5ebc53e8-7d15-48d7-abb1-6102ea4d7ea9"), null, "User", "USER" },
                    { new Guid("659d04c4-30dc-4729-9539-877c56dcfb9c"), null, "Admin", "ADMIN" },
                    { new Guid("675a3309-29e3-49d3-9290-8df40f0cb09d"), null, "Moderator", "MODERATOR" }
                });
        }
    }
}
