using Microsoft.EntityFrameworkCore.Migrations;

namespace BEQLDT.Data.Migrations
{
    public partial class updatefile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64File",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64File",
                table: "Files");
        }
    }
}
