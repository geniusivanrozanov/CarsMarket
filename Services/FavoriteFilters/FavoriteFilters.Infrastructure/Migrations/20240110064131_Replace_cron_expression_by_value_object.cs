using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteFilters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Replace_cron_expression_by_value_object : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cron",
                table: "Filters");

            migrationBuilder.AddColumn<int>(
                name: "Cron_DayOfMonth",
                table: "Filters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cron_DayOfWeek",
                table: "Filters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cron_Hour",
                table: "Filters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cron_Minute",
                table: "Filters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cron_Month",
                table: "Filters",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cron_DayOfMonth",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "Cron_DayOfWeek",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "Cron_Hour",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "Cron_Minute",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "Cron_Month",
                table: "Filters");

            migrationBuilder.AddColumn<string>(
                name: "Cron",
                table: "Filters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
