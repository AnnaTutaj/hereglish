using Microsoft.EntityFrameworkCore.Migrations;

namespace Hereglish.Migrations
{
    public partial class AddDefinitionToWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Definition",
                table: "Words",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Definition",
                table: "Words");
        }
    }
}
