namespace AlmaIt.Dotnet.Heroes.Shared.Models
{
    /// <summary>Link class between ComicSeries and tags.</summary>
    public class ComicSeriesTags : IDataObject<int>
    {
        /// <summary>Gets or sets instance of the related comic Serie.</summary>
        public ComicSeries ComicSerie { get; set; }

        /// <summary>Gets or sets primary key of associated comic Serie.</summary>
        public int ComicSerieId { get; set; }

        /// <summary>Gets or sets the primary key.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets instance of the related tag.</summary>
        public ObjectTag Tag { get; set; }

        /// <summary>Gets or sets primary key of associated tag.</summary>
        public int TagId { get; set; }
    }
}
