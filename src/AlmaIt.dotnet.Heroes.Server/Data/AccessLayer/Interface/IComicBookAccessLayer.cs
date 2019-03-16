namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AlmaIt.Dotnet.Heroes.Shared.Models;

    /// <summary>Interface defining how to interact with <see cref="ComicBook" /> data object.</summary>
    public interface IComicBookAccessLayer : IBaseAccessLayer<ComicBook, int>
    {
        /// <summary>Async method that returns all comics info and their associated serie Filter ComicSerie object so that not all related comicBook's serie get included.</summary>
        /// <returns>Return a list of comic book and their series <see cref="IEnumerable{ComicBook}" />.</returns>
        Task<IEnumerable<ComicBook>> GetAllComcisAndSerieInfo();
    }
}
