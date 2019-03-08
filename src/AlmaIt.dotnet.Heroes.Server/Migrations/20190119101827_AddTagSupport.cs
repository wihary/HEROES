using Microsoft.EntityFrameworkCore.Migrations;

namespace AlmaIt.Dotnet.Heroes.Server.Migrations
{
    public partial class AddTagSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComicBookTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ComicBookId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicBookTags", x => new { x.ComicBookId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ComicBookTags_ComicBooks_ComicBookId",
                        column: x => x.ComicBookId,
                        principalTable: "ComicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicBookTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "ObjectTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComicSerieTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ComicSerieId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicSerieTags", x => new { x.ComicSerieId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ComicSerieTags_ComicSeries_ComicSerieId",
                        column: x => x.ComicSerieId,
                        principalTable: "ComicSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicSerieTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "ObjectTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicBookTags_TagId",
                table: "ComicBookTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicSerieTags_TagId",
                table: "ComicSerieTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicBookTags");

            migrationBuilder.DropTable(
                name: "ComicSerieTags");
        }
    }
}
