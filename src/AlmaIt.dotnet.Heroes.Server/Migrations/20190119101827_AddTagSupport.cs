namespace AlmaIt.Dotnet.Heroes.Server.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class AddTagSupport : Migration
    {
        /// <summary>
        ///     <para>
        ///         Builds the operations that will migrate the database 'up'.
        ///     </para>
        ///     <para>
        ///         That is, builds the operations that will take the database from the state left in by the
        ///         previous migration so that it is up-to-date with regard to this migration.
        ///     </para>
        ///     <para>
        ///         This method must be overridden in each class the inherits from <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" />.
        ///     </para>
        /// </summary>
        /// <param name="migrationBuilder"> The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will build the operations. </param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComicBookTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ComicBookId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
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
                    TagId = table.Column<int>(nullable: false),
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

        /// <summary>
        ///     <para>
        ///         Builds the operations that will migrate the database 'down'.
        ///     </para>
        ///     <para>
        ///         That is, builds the operations that will take the database from the state left in by
        ///         this migration so that it returns to the state that it was in before this migration was applied.
        ///     </para>
        ///     <para>
        ///         This method must be overridden in each class the inherits from <see cref="T:Microsoft.EntityFrameworkCore.Migrations.Migration" /> if
        ///         both 'up' and 'down' migrations are to be supported. If it is not overridden, then calling it
        ///         will throw and it will not be possible to migrate in the 'down' direction.
        ///     </para>
        /// </summary>
        /// <param name="migrationBuilder"> The <see cref="T:Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder" /> that will build the operations. </param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicBookTags");

            migrationBuilder.DropTable(
                name: "ComicSerieTags");
        }
    }
}
