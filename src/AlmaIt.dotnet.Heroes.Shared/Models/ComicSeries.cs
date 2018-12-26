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
        [NotMapped]
        public List<ComicBook> AssociatedComnicBooksExtended { get; set; } = new List<ComicBook>();

        /// <summary>
        /// Object latest update date
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Indicates when was the last issue released
        /// </summary>
        public DateTime LastReleaseDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Indicates when next release is planned
        /// </summary>
        public DateTime NextReleaseDate { get; set; } = DateTime.Now;

        /// <summary>
        /// True if a comic serie is finished (no more issue are planned)
        /// </summary>
        public bool IsSerieCompleted { get; set; }

    }
}