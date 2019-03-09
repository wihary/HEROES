﻿namespace AlmaIt.Dotnet.Heroes.Server.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class AddObjectTags : Migration
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
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.CreateTable(
                name: "ObjectTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Argb = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectTag", x => x.Id);
                });

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
            => migrationBuilder.DropTable(name: "ObjectTag");
    }
}
