namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>Represent a comic serie.</summary>
    public class ComicSeries : IDataObject<int>
    {
        /// <summary>Gets or sets the list of all associated comic book object (within the local db).</summary>
        public List<ComicBook> AssociatedComnicBooksExtended { get; set; } = new List<ComicBook>();

        /// <summary>Gets or sets the primary key.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets a value indicating whether true if a comic serie is finished (no more issue are planned).</summary>
        public bool IsSerieCompleted { get; set; }

        /// <summary>Gets the issue count.</summary>
        [NotMapped]
        [DisplayName("Nb issues")]
        public int IssuesCount => this.AssociatedComnicBooksExtended.Count;

        /// <summary>Gets or sets the date indicates when was the last issue released.</summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastReleaseDate { get; set; } = DateTime.Now;

        /// <summary>Gets or sets the name of comic serie.</summary>
        [Required]
        [DisplayName("Serie name")]
        public string Name { get; set; }

        /// <summary>Gets or sets the date indicates when next release is planned.</summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NextReleaseDate { get; set; } = DateTime.Now;

        /// <summary>Gets or sets the date indicates the latest update of object.</summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
