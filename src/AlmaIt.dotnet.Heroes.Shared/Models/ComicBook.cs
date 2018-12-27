namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represent a comic book or issue
    /// </summary>
    /// <typeparam name="int">Unique identifier (primary key)</typeparam>
    public class ComicBook : IDataObject<int>
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Primary key of associated comics serie
        /// </summary>
        public int? ComicSerieId { get; set; }

        /// <summary>
        /// Instance of the related comic serie
        /// </summary>
        /// <value></value>
        public ComicSeries ComicSerie { get; set; }

        /// <summary>
        /// Comic book Title
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Comic book number
        /// </summary>
        [Required]
        public int IssueNumber { get; set; } = 0;

        /// <summary>
        /// Comic book parution date
        /// </summary>
        [Required]
        public DateTime ParutionDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Object latest update date
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// State if comic book is in collection
        /// </summary>
        public bool IsInCollection { get; set; } = false;

        /// <summary>
        /// State if a comic book has been ordered and pending delivery
        /// </summary>
        public bool IsOrdered { get; set; } = false;
    }
}