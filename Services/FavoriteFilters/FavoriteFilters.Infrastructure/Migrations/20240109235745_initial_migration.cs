using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteFilters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: true),
                    GenerationId = table.Column<Guid>(type: "uuid", nullable: true),
                    MinYear = table.Column<int>(type: "integer", nullable: true),
                    MaxYear = table.Column<int>(type: "integer", nullable: true),
                    MinMileage = table.Column<int>(type: "integer", nullable: true),
                    MaxMileage = table.Column<int>(type: "integer", nullable: true),
                    MinPrice = table.Column<double>(type: "double precision", nullable: true),
                    MaxPrice = table.Column<double>(type: "double precision", nullable: true),
                    Currency = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filters");
        }
    }
}
