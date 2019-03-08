namespace AlmaIt.Dotnet.Heroes.Shared.Models
{
    public class ComicBookTags : IDataObject<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Primary key of associated comicBook
        /// </summary>
        public int ComicBookId { get; set; }

        /// <summary>
        /// Instance of the related comicBook
        /// </summary>
        /// <value></value>
        public ComicBook ComicBook { get; set; }

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