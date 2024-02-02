using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteFilters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_user_info_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Filters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Filters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Filters_UserId",
                table: "Filters",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Filters_UserId",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Filters");
        }
    }
}
