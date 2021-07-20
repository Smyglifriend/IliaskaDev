using Microsoft.EntityFrameworkCore.Migrations;
using SpaceWeb.EfStuff.Model;

namespace IliaskaWebSite.Migrations
{
    public partial class FieldJobType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: JobType.OfficeWorker);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Users");
        }
    }
}
