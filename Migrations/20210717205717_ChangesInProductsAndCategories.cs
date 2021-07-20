using Microsoft.EntityFrameworkCore.Migrations;

namespace IliaskaWebSite.Migrations
{
    public partial class ChangesInProductsAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clothes",
                table: "ProductCategory");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Product",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Clothes",
                table: "ProductCategory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
