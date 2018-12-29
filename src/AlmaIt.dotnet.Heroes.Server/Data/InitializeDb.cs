namespace AlmaIt.dotnet.Heroes.Server.Data
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     This class meant to regroup all initialization options of the Db Context
    /// </summary>
    public class InitializeDb
    {
        private readonly HeroesContext context;

        /// <summary>
        ///     Initialize instance of <see cref="InitialisationData" />.
        /// </summary>
        /// <param name="context">Contexte de la base de données.</param>
        public InitializeDb(HeroesContext context) => this.context = context;

        /// <summary>
        ///     Méthode that initialize Db.
        /// </summary>
        public void Init() => this.context.Database.Migrate();
    }
}