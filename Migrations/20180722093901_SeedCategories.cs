using Microsoft.EntityFrameworkCore.Migrations;

namespace Hereglish.Migrations
{
    public partial class SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Filmy')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Youtube')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Gry')");
            migrationBuilder.Sql("INSERT INTO Categories (Name) VALUES ('Ćwiczenia')");

            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Friends', (SELECT ID FROM Categories WHERE Name = 'Filmy'))");
            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Adventure Time', (SELECT ID FROM Categories WHERE Name = 'Filmy'))");
            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Game of Thrones', (SELECT ID FROM Categories WHERE Name = 'Filmy'))");
            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Anime', (SELECT ID FROM Categories WHERE Name = 'Filmy'))");

            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Arlena Witt', (SELECT ID FROM Categories WHERE Name = 'Youtube'))");
            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Winiarska', (SELECT ID FROM Categories WHERE Name = 'Youtube'))");

            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('The Sims', (SELECT ID FROM Categories WHERE Name = 'Gry'))");
            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('League of Legends', (SELECT ID FROM Categories WHERE Name = 'Gry'))");

            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Życie codzienne', (SELECT ID FROM Categories WHERE Name = 'Ćwiczenia'))");
            migrationBuilder.Sql("INSERT INTO Subcategories (Name, CategoryID) VALUES ('Praca', (SELECT ID FROM Categories WHERE Name = 'Ćwiczenia'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories WHERE Name IN ('Filmy', 'Youtube', 'Gry', 'Ćwiczenia')");
        }
    }
}
