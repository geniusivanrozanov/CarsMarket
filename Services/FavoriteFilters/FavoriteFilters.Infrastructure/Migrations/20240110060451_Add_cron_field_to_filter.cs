using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteFilters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_cron_field_to_filter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cron",
                table: "Filters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cron",
                table: "Filters");
        }
    }
}
