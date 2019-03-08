namespace AlmaIt.Dotnet.Heroes.Shared.Models
{
    /// <summary>Link class between ComicBook and tags.</summary>
    public class ComicBookTags : IDataObject<int>
    {
        /// <summary>Gets or sets instance of the related comicBook.</summary>
        public ComicBook ComicBook { get; set; }

        /// <summary>Gets or sets primary key of associated comicBook.</summary>
        public int ComicBookId { get; set; }

        /// <summary>Gets or sets the primary key.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets instance of the related tag.</summary>
        public ObjectTag Tag { get; set; }

        /// <summary>Gets or sets primary key of associated tag.</summary>
        public int TagId { get; set; }
    }
}
