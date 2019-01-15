namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;

    /// <summary>
    /// Represent a tag which can be set on any object
    /// </summary>
    /// <typeparam name="int">Unique identifier (primary key)</typeparam>
    public class ObjectTag : IDataObject<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Argb
        {
            get => this.Color.ToArgb();
            set => this.Color = Color.FromArgb(value);
        }

        [NotMapped]
        public Color Color { get; set; }
    }
}