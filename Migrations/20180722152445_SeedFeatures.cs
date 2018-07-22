using Microsoft.EntityFrameworkCore.Migrations;

namespace Hereglish.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Ważne')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Chodzi o łącznik')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Wymowa do poćwiczenia')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('Ważne', 'Chodzi o łącznik', 'Wymowa do poćwiczenia')");
        }
    }
}
