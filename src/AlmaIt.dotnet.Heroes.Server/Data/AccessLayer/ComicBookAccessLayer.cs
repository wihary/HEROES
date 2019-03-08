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
        /// Initializes a new instance of the <see cref="ComicBookAccessLayer"/> class.
        /// </summary>
        /// <param name="context">Data base context.</param>
        public ComicBookAccessLayer(HeroesContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Async Method that return all data object existing in Db.
        /// </summary>
        /// <returns>List of <see cref="ComicBook"/>.</returns>
        public override IAsyncEnumerable<ComicBook> GetAllAsync() => this.ModelSet.Include(book => book.ComicSerie).ToAsyncEnumerable();

        /// <summary>
        ///     Async method that returns all comics info and their associated serie
        ///     Filter ComicSerie object so that not all related comicBook's serie get included.
        /// </summary>
        /// <returns>Return a list of comic book and their series <see cref="IEnumerable{ComicBook}"/>.</returns>
        public async Task<IEnumerable<ComicBook>> GetAllComcisAndSerieInfo()
        {
            var result = await this.GetAllAsync().ToList();
            Parallel.ForEach(result, book => book.ComicSerie.AssociatedComnicBooksExtended.Clear());

            return result;
        }
    }
}
