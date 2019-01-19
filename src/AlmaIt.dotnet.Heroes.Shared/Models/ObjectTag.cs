namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;

    /// <summary>
    /// Represent a tag which can be set on any object
    /// </summary>
    /// <typeparam name="int">Unique identifier (primary key)</typeparam>
    public class ObjectTag : IDataObject<int>
    {
        public ObjectTag()
        {
            this.Argb = Color.AliceBlue.ToArgb();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Argb { get; set; }

        [NotMapped]
        public Color Color { get => Color.FromArgb(this.Argb); }

        /// <summary>
        /// List of all associated Comic Books
        /// </summary>
        public ICollection<ComicBookTags> ComicBookTags { get; set; }

        /// <summary>
        /// List of all associated Comic Series
        /// </summary>
        public ICollection<ComicSeriesTags> ComicSerieTags { get; set; }
    }
}