using Microsoft.EntityFrameworkCore.Migrations;

namespace AlmaIt.Dotnet.Heroes.Server.Migrations
{
    public partial class ComicBookCoverPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPicture",
                table: "ComicBooks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "ComicBooks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPicture",
                table: "ComicBooks");

            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "ComicBooks");
        }
    }
}
