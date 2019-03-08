namespace AlmaIt.Dotnet.Heroes.Shared.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;

    /// <summary>Represent a tag which can be set on any object.</summary>
    public class ObjectTag : IDataObject<int>
    {
        /// <summary>Gets or sets the RGB color of tag.</summary>
        public int Argb { get; set; } = Color.AliceBlue.ToArgb();

        /// <summary>Gets the .NET color of tag.</summary>
        [NotMapped]
        public Color Color
            => Color.FromArgb(this.Argb);

        /// <summary>Gets or sets the primary key.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the tag name.</summary>
        public string Name { get; set; }
    }
}
