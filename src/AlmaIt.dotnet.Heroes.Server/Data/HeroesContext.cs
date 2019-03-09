namespace AlmaIt.Dotnet.Heroes.Server.Data
{
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.EntityFrameworkCore;

    /// <summary>Class represent Heroes database.</summary>
    public class HeroesContext : DbContext
    {
        /// <summary>Gets or sets the whole dataset of comic books.</summary>
        public DbSet<ComicBook> ComicBooks { get; set; }

        /// <summary>Gets or sets the whole dataset of associated comic books and tags.</summary>
        public DbSet<ComicBookTags> ComicBookTags { get; set; }

        /// <summary>Gets or sets the whole dataset of comic series.</summary>
        public DbSet<ComicSeries> ComicSeries { get; set; }

        /// <summary>Gets or sets the whole dataset of associated comic series and tags.</summary>
        public DbSet<ComicSeriesTags> ComicSerieTags { get; set; }

        /// <summary>Gets or sets the whole dataset of available Tags.</summary>
        public DbSet<ObjectTag> ObjectTag { get; set; }

        /// <summary>Override This method is called for each instance of the context that is created.</summary>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other extensions) typically define extension methods on this object that
        ///     allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=./Data/Heroes.db");

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from the entity types exposed in
        ///     <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached and re-used for subsequent instances of your
        ///     derived context.
        /// </summary>
        /// <remarks>
        ///     If a model is explicitly set on the options for this context (via
        ///     <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />) then this method will not be run.
        /// </remarks>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context. Databases (and other extensions) typically define extension methods on this object that
        ///     allow you to configure aspects of the model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComicBookTags>().HasKey(x => new { x.ComicBookId, x.TagId });
            modelBuilder.Entity<ComicSeriesTags>().HasKey(x => new { x.ComicSerieId, x.TagId });
        }
    }
}
