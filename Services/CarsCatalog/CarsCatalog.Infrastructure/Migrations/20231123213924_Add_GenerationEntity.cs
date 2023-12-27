using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_GenerationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenerationEntity_Models_ModelId",
                table: "GenerationEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenerationEntity",
                table: "GenerationEntity");

            migrationBuilder.RenameTable(
                name: "GenerationEntity",
                newName: "Generations");

            migrationBuilder.RenameIndex(
                name: "IX_GenerationEntity_ModelId",
                table: "Generations",
                newName: "IX_Generations_ModelId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Generations",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generations",
                table: "Generations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_Name_ModelId",
                table: "Generations",
                columns: new[] { "Name", "ModelId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generations_Models_ModelId",
                table: "Generations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Generations",
                table: "Generations");

            migrationBuilder.DropIndex(
                name: "IX_Generations_Name_ModelId",
                table: "Generations");

            migrationBuilder.RenameTable(
                name: "Generations",
                newName: "GenerationEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Generations_ModelId",
                table: "GenerationEntity",
                newName: "IX_GenerationEntity_ModelId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GenerationEntity",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenerationEntity",
                table: "GenerationEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenerationEntity_Models_ModelId",
                table: "GenerationEntity",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
