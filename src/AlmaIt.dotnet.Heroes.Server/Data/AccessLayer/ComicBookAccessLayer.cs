namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
        public override IAsyncEnumerable<ComicBook> GetAllAsync() => this.ModelSet
            .Include(book => book.ComicSerie)
            .Include(book => book.RelatedTags).ThenInclude(tag => tag.Tag)
            .ToAsyncEnumerable();

        /// <summary>
        ///     Async method that returns all comics info and their associated serie
        ///     Filter ComicSerie object so that not all related comicBook's serie get included
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ComicBook>> GetAllComcisAndSerieInfo()
        {
            var result = await this.GetAllAsync().ToList();
            Parallel.ForEach<ComicBook>(result, book => book?.ComicSerie?.AssociatedComnicBooksExtended?.Clear());

            return result;
        }
    }
}