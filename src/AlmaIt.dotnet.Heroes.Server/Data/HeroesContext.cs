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
        public DbSet<ComicSeries> ComicSeries{ get; set; }

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
    }
}