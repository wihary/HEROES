namespace AlmaIt.Dotnet.Heroes.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AlmaIt.Dotnet.Heroes.Shared.Enumeration;

    /// <summary>
    ///     Represent a comic book or issue.
    /// </summary>
    public class ComicBook : IDataObject<int>
    {
        /// <summary>
        ///     Gets or sets the primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the primary key of associated comics serie.
        /// </summary>
        public int? ComicSerieId { get; set; }

        /// <summary>
        ///     Gets or sets the instance of the related comic serie.
        /// </summary>
        public ComicSeries ComicSerie { get; set; }

        /// <summary>
        ///     Gets or sets the comic book Title.
        /// </summary>
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the comic book number.
        /// </summary>
        [Required]
        [DisplayName("Issue number")]
        public int IssueNumber { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the comic book parution date.
        /// </summary>
        [Required]
        public DateTime ParutionDate { get; set; } = DateTime.Now;

        /// <summary>
        ///     Gets or sets the object latest update date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        /// <summary>
        ///     Gets or sets the status of the comic book.
        /// </summary>
        public ComicBookStatus Status { get; set; } = ComicBookStatus.None;

        /// <summary>
        ///     Gets or sets the comic cover URL
        /// </summary>
        public string CoverUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the comic cover Picture, base64 representation of the picture
        /// </summary>
        public string CoverPicture { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets list of all associated Tag object.
        ///     TODO: [JsonIgnore] == we are not yet ready for this, should come with business layer separation.
        /// </summary>
        public ICollection<ComicBookTags> RelatedTags { get; set; }

        /// <summary>
        ///     Gets the tags list.
        /// </summary>
        [NotMapped]
        public List<ObjectTag> Tags => this.RelatedTags == null ? new List<ObjectTag>() : this.RelatedTags.Select(t => t.Tag).ToList();
    }
}
