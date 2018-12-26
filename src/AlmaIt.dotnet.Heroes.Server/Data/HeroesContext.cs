namespace AlmaIt.dotnet.Heroes.Server.Data
{
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.EntityFrameworkCore;

    public class HeroesContext : DbContext
    {
        public DbSet<ComicBook> ComicBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=./Data/Heroes.db");
    }
}