using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Rename_LastModifiedAt_to_UpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("38727386-fa7c-40ab-8e8b-2778631b49d9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b842f69a-7446-4f09-9e4d-ab636b7205e7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8eca5b9-70c1-49d5-9cf5-f1cfca43b55a"));

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "AspNetUsers",
                newName: "UpdatedAt");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "AspNetUsers",
                newName: "LastModifiedAt");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("38727386-fa7c-40ab-8e8b-2778631b49d9"), null, "Admin", "ADMIN" },
                    { new Guid("b842f69a-7446-4f09-9e4d-ab636b7205e7"), null, "Moderator", "MODERATOR" },
                    { new Guid("e8eca5b9-70c1-49d5-9cf5-f1cfca43b55a"), null, "User", "USER" }
                });
        }
    }
}
