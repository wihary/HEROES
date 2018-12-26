namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface
{
    using AlmaIt.dotnet.Heroes.Shared.Models;

    /// <summary>
    ///     Interface defining how to interact with <see cref="ComicBook"/> data object
    /// </summary>
    public interface IComicBookAccessLayer : IBaseAccessLayer<ComicBook, int>
    {

    }
}