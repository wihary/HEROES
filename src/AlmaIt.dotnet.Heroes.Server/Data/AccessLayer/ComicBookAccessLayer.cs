namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Class allowing access to <see cref="ComicBook"/> data object.
    /// </summary>
    internal class ComicBookAccessLayer : BaseAccessLayer<HeroesContext, ComicBook, int>, IComicBookAccessLayer
    {
        /// <summary>
        ///     Initialize new instance of data access layer <see cref="ComicBookAccessLayer" />.
        /// </summary>
        /// <param name="context">Data base context.</param>
        public ComicBookAccessLayer(HeroesContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Async Method that return all data object existing in Db
        /// </summary>
        /// <returns>List of <see cref="TModel"/></returns>
        public override IAsyncEnumerable<ComicBook> GetAllAsync() => this.ModelSet.Include(book => book.ComicSerie).ToAsyncEnumerable();
    }
}