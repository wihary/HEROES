namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    /// <summary>
    ///     Interface defining how to interact with <see cref="ComicSeries"/> data object.
    /// </summary>
    public interface IComicSeriesAccessLayer : IBaseAccessLayer<ComicSeries, int>
    {
        /// <summary>Async Method that return all data object existing in Db.</summary>
        /// <returns>List of <see cref="ComicSeries" />.</returns>
        IAsyncEnumerable<ComicSeries> GetAllDetailsAsync() ;
    }
}