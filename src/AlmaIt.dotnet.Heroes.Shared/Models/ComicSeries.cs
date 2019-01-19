using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    /// <summary>
    /// Represent a comic serie
    /// </summary>
    /// <typeparam name="int">Unique identifier (primary key)</typeparam>
    public class ComicSeries : IDataObject<int>
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of comic serie
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// List of all associated comic book object (within the local db)
        /// </summary>
        public List<ComicBook> AssociatedComnicBooksExtended { get; set; } = new List<ComicBook>();

        /// <summary>
        /// Object latest update date
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Indicates when was the last issue released
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastReleaseDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Indicates when next release is planned
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NextReleaseDate { get; set; } = DateTime.Now;

        /// <summary>
        /// True if a comic serie is finished (no more issue are planned)
        /// </summary>
        public bool IsSerieCompleted { get; set; }

        /// <summary>
        /// List of all associated Tag object
        /// </summary>
        public ICollection<ComicSeriesTags> RelatedTags { get; set; }
    }
}