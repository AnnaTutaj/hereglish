using Microsoft.EntityFrameworkCore.Migrations;

namespace Hereglish.Migrations
{
    public partial class SeedPartsOfSpeech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PartsOfSpeech (Name) VALUES ('Rzeczownik')");
            migrationBuilder.Sql("INSERT INTO PartsOfSpeech (Name) VALUES ('Czasownik')");
            migrationBuilder.Sql("INSERT INTO PartsOfSpeech (Name) VALUES ('Przymiotnik')");
            migrationBuilder.Sql("INSERT INTO PartsOfSpeech (Name) VALUES ('Przysłówek')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PartsOfSpeech WHERE Name IN ('Rzeczownik', 'Czasownik', 'Przymiotnik', 'Przysłówek')");
        }
    }
}
