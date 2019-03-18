namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer
{
    using System.Collections.Generic;
    using System.Linq;

    using AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    using Microsoft.EntityFrameworkCore;

    /// <summary>Class allowing access to <see cref="ComicSeries" /> data object.</summary>
    internal class ComicSeriesAccessLayer : BaseAccessLayer<HeroesContext, ComicSeries, int>, IComicSeriesAccessLayer
    {
        /// <summary>Initializes a new instance of the <see cref="ComicSeriesAccessLayer" /> class.</summary>
        /// <param name="context">Data base context.</param>
        public ComicSeriesAccessLayer(HeroesContext context)
            : base(context)
        {
        }

        /// <summary>Async Method that return all data object existing in Db.</summary>
        /// <returns>List of <see cref="ComicSeries" />.</returns>
        public override IAsyncEnumerable<ComicSeries> GetAllAsync() => this.ModelSet.ToAsyncEnumerable();

        /// <summary>Async Method that return all data object existing in Db.</summary>
        /// <returns>List of <see cref="ComicSeries" />.</returns>
        public IAsyncEnumerable<ComicSeries> GetAllDetailsAsync() => this.ModelSet.Include(serie => serie.AssociatedComnicBooksExtended).ToAsyncEnumerable();
    }
}
