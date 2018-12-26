namespace AlmaIt.dotnet.Heroes.Server.Data.AccessLayer
{

    using AlmaIt.dotnet.Heroes.Server.Data.AccessLayer.Interface;
    using AlmaIt.dotnet.Heroes.Shared.Models;

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
    }
}