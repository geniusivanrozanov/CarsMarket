using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_ModelEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenerationEntity_ModelEntity_ModelId",
                table: "GenerationEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ModelEntity_Brands_BrandId",
                table: "ModelEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModelEntity",
                table: "ModelEntity");

            migrationBuilder.RenameTable(
                name: "ModelEntity",
                newName: "Models");

            migrationBuilder.RenameIndex(
                name: "IX_ModelEntity_BrandId",
                table: "Models",
                newName: "IX_Models_BrandId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Models",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Models_Name_BrandId",
                table: "Models",
                columns: new[] { "Name", "BrandId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GenerationEntity_Models_ModelId",
                table: "GenerationEntity",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenerationEntity_Models_ModelId",
                table: "GenerationEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_Name_BrandId",
                table: "Models");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "ModelEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Models_BrandId",
                table: "ModelEntity",
                newName: "IX_ModelEntity_BrandId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ModelEntity",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModelEntity",
                table: "ModelEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenerationEntity_ModelEntity_ModelId",
                table: "GenerationEntity",
                column: "ModelId",
                principalTable: "ModelEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelEntity_Brands_BrandId",
                table: "ModelEntity",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
