namespace AlmaIt.Dotnet.Heroes.Shared.Models
{
    public class ComicSeriesTags : IDataObject<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Primary key of associated comic Serie
        /// </summary>
        public int ComicSerieId { get; set; }

        /// <summary>
        /// Instance of the related comic Serie
        /// </summary>
        /// <value></value>
        public ComicSeries ComicSerie { get; set; }

        /// <summary>
        /// Primary key of associated tag
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Instance of the related tag
        /// </summary>
        /// <value></value>
        public ObjectTag Tag { get; set; }
    }
}