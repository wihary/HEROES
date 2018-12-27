namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;

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

        public IEnumerable<ComicBook> GetAll()
        {
            var comicBookList = this.GetAllAsync().ToEnumerable();

            foreach (var comicBook in comicBookList)
            {
                
            }

            return comicBookList;
        }
    }
}