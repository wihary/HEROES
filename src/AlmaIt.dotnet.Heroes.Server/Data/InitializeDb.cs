namespace AlmaIt.Dotnet.Heroes.Server.Data
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>This class meant to regroup all initialization options of the Db Context.</summary>
    public class InitializeDb
    {
        private readonly HeroesContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeDb"/> class.</summary>
        /// <param name="context">Contexte de la base de données.</param>
        public InitializeDb(HeroesContext context) => this.context = context;

        /// <summary>Méthode that initialize Db.</summary>
        public void Init() => this.context.Database.Migrate();
    }
}
