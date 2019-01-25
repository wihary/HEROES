namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AlmaIt.dotnet.Heroes.Shared.Models;

    /// <summary>
    ///     Interface defining how to interact with <see cref="ComicBook"/> data object
    /// </summary>
    public interface IComicBookAccessLayer : IBaseAccessLayer<ComicBook, int>
    {
        Task<IEnumerable<ComicBook>> GetAllComcisAndSerieInfo();
    }
}