using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlmaIt.Dotnet.Heroes.Server.Migrations
{
    public partial class InitializeDataStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComicSeries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    LastReleaseDate = table.Column<DateTime>(nullable: false),
                    NextReleaseDate = table.Column<DateTime>(nullable: false),
                    IsSerieCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComicBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComicSerieId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    IssueNumber = table.Column<int>(nullable: false),
                    ParutionDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComicBooks_ComicSeries_ComicSerieId",
                        column: x => x.ComicSerieId,
                        principalTable: "ComicSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicBooks_ComicSerieId",
                table: "ComicBooks",
                column: "ComicSerieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicBooks");

            migrationBuilder.DropTable(
                name: "ComicSeries");
        }
    }
}
