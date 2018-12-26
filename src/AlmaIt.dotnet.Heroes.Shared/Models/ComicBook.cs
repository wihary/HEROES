namespace AlmaIt.dotnet.Heroes.Shared.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ComicBook : IDataObject<int>
    {
        public int Id { get; set; }

        public int SerieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int IssueNumber { get; set; }

        [Required]
        public DateTime ParutionDate { get; set; }

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public bool IsInCollection { get; set; } = false;

        public bool IsOrdered { get; set; } = false;
    }
}