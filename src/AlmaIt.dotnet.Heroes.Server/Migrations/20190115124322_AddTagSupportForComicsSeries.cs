using Microsoft.EntityFrameworkCore.Migrations;

namespace AlmaIt.dotnet.Heroes.Server.Migrations
{
    public partial class AddTagSupportForComicsSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComicBookTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComicBookId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicBookTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComicBookTags_ComicBooks_ComicBookId",
                        column: x => x.ComicBookId,
                        principalTable: "ComicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicBookTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComicSeriesTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComicSerieId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicSeriesTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComicSeriesTags_ComicSeries_ComicSerieId",
                        column: x => x.ComicSerieId,
                        principalTable: "ComicSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicSeriesTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicBookTags_ComicBookId",
                table: "ComicBookTags",
                column: "ComicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicBookTags_TagId",
                table: "ComicBookTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicSeriesTags_ComicSerieId",
                table: "ComicSeriesTags",
                column: "ComicSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicSeriesTags_TagId",
                table: "ComicSeriesTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicBookTags");

            migrationBuilder.DropTable(
                name: "ComicSeriesTags");
        }
    }
}
