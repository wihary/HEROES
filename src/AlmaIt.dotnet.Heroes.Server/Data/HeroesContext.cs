namespace AlmaIt.dotnet.Heroes.Server.Data
{
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.EntityFrameworkCore;

    public class HeroesContext : DbContext
    {
        /// <summary>
        /// Represent the whole dataset of comic books
        /// </summary>
        public DbSet<ComicBook> ComicBooks { get; set; }

        /// <summary>
        /// Represent the whole dataset of comic series
        /// </summary>
        public DbSet<ComicSeries> ComicSeries { get; set; }

        /// <summary>
        /// Represent the whole dataset of available Tags
        /// </summary>
        public DbSet<ObjectTag> Tags { get; set; }

        /// <summary>
        /// Represent the whole dataset of associated comic books and tags
        /// </summary>
        public DbSet<ComicBookTags> ComicBookTags { get; set; }

        /// <summary>
        /// Represent the whole dataset of associated comic series and tags
        /// </summary>
        public DbSet<ComicSeriesTags> ComicSerieTags { get; set; }

        /// <summary>
        ///     Override
        ///     This method is called for each instance of the context that is created
        /// </summary>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other
        ///     extensions) typically define extension methods on this object that allow you
        ///     to configure the context
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=./Data/Heroes.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComicBookTags>().HasKey(x => new { x.ComicBookId, x.TagId });
            modelBuilder.Entity<ComicBook>()
                .HasMany(comicBook => comicBook.Tags)
                .WithOne(tag => tag.ComicBook)
                .HasForeignKey(key => key.ComicBookId);
            modelBuilder.Entity<ObjectTag>()
                .HasMany(objectTag => objectTag.ComicBookTags)
                .WithOne(tag => tag.Tag)
                .HasForeignKey(key => key.TagId);

            modelBuilder.Entity<ComicSeriesTags>().HasKey(x => new { x.ComicSerieId, x.TagId });
            modelBuilder.Entity<ComicSeries>()
                .HasMany(comicSerie => comicSerie.Tags)
                .WithOne(tag => tag.ComicSerie)
                .HasForeignKey(key => key.ComicSerieId);
            modelBuilder.Entity<ObjectTag>()
                .HasMany(objectTag => objectTag.ComicSerieTags)
                .WithOne(tag => tag.Tag)
                .HasForeignKey(key => key.TagId);
        }
    }
}