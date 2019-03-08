namespace AlmaIt.Dotnet.Heroes.Server.Data.AccessLayer.Interface
{
    using AlmaIt.Dotnet.Heroes.Shared.Models;

    /// <summary>
    ///     Interface defining how to interact with <see cref="ComicSeries"/> data object
    /// </summary>
    public interface IComicSeriesAccessLayer : IBaseAccessLayer<ComicSeries, int>
    {
    }
}