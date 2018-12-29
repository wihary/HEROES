namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Class allowing access to <see cref="ComicSeries"/> data object.
    /// </summary>
    internal class ComicSeriesAccessLayer : BaseAccessLayer<HeroesContext, ComicSeries, int>, IComicSeriesAccessLayer
    {
        /// <summary>
        ///     Initialize new instance of data access layer <see cref="ComicSeriesAccessLayer" />.
        /// </summary>
        /// <param name="context">Data base context.</param>
        public ComicSeriesAccessLayer(HeroesContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Async Method that return all data object existing in Db
        /// </summary>
        /// <returns>List of <see cref="TModel"/></returns>
        public override IAsyncEnumerable<ComicSeries> GetAllAsync() => this.ModelSet.Include(serie => serie.AssociatedComnicBooksExtended).ToAsyncEnumerable();
    }
}